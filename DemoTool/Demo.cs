//#define LOCAL_DEBUG
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Collections.Concurrent;
using System.Xml;
using System.IO;
using DemoTool.Properties;
using System.Text.RegularExpressions;


//using CoreWinSubFTDIFIFO;

using FTD2XX_NET;

using cThrobber;

using System.Windows.Forms.DataVisualization.Charting;

#region Namespace used for combine report

using iTextSharp.text.pdf.parser;
using iTextSharp.text.pdf;

using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

#endregion

namespace DemoTool {

    public partial  class Demo :Form {

        #region Global Constants

        public const string gkVersion = "$Id: Version 60 2017-9-22 10:25:42Z$";

        public const string gkTOFTEKDebugMode = "TOFTEKDEBUG_MODE";

        public const string gkDefaultLanguageParmName = "Language";

        public string gDefaultLanguage = "English";

        public int gSelectedLanguage = (int)gLanguageVersion.English;

        public bool gAutoFittingEnable = true;

        public string gkProtocolDirectory = "C:\\TOFTEK\\Configuration\\Protocols\\";

        public const string gkRawDataDirectory = "C:\\TOFTEK\\RawData\\";

        public string gkConfigFilesPath = "C:\\TOFTEK\\Configuration\\Config.xml";

        public const string gkXmlConfigNodePath = "/ConfigParamContainer/ParameterList/ConfigParamPair";

        public const string gkXmlConfigAddNodePath = "/ConfigParamContainer/ParameterList";

        //This is used to name the energy spectrum display and close all eneger display windows
        const string gkFormNamePrefix = "Energy Spectrum";

        public const string gkTOFTEKImageRoot = "C:\\TOFTEK\\Configuration\\Images\\";

        public const string gkTOFTEKLogo = "TOFTEK_LOGO.PNG";

        public const string gkTOFTEKTempRoot = "C:\\TOFTEK\\Temp\\";

        public const int gkLightShareEvenChecksum = 272;
        public const int gkLightShareOddChecksum = 276;

        #region Result Related

        public const float gkPeakLowLimit = 0f;
        public const float gkPeakHighLimit = 16384f;

        #endregion

        #region Message Command Definition

        public const int gkMessageTypeCmd = 0xFF;

        public const int gkMessageTypeData = 0xF0;

        public const UInt32 gkMessageDataSize = 2;

        public const int gkCommandSize = 4;

        public const int gkTotalPixelNumbers = 256;

        public const int gkMessageBytesSize = 4;

        public const byte gkCmdHandshaking = 0x00;

        public const byte gkCmdCheckCurrentStatus = 0x01;

        public const byte gkCmdGetThermoInfo = 0x02;

        public const byte gkCmdWriteModeReg = 0x03;

        public const byte gkCmdADConfigAccess = 0x04;

        public const byte gkCmdWritePowerConfig = 0x06;

        public const byte gkCmdWriteACQ = 0x07;

        public const byte gkCmdButtonPushed = 0x08;

        public const byte gkCmdErrorMessage = 0x09;

        public const byte gkCmdResetACQBoard = 0x0A;

        public const byte gkCmdWriteCorrectionTableEnable = 0x0B;

        public const byte gkCmdWriteLightShareADCThresholdReg = 0x0B;

        public const byte gkCmdGetBaseLineQuery = 0x0D;

        public const int gkVrefPowerIndex = 0;
        public const int gkVbiasPowerIndex = 1;
        public const int gkVbiasPowerForceOnIndex = 2;

        public const int gkADCPowerIndex = 0;
        public const int gkADCTestModeIndex = 1;
        public const int gkADCRangeIndex = 2;

        public const int gkRequestAllThermoSensorIndex = 0;
        public const int gkRequestAvgThermoSensorIndex = 1;
        public const int gk1stThermoSensorIndex = 0;
        public const int gk2ndThermoSensorIndex = 1;
        public const int gk3rdThermoSensorIndex = 2;
        public const int gk4thThermoSensorIndex = 3;
        public const int gkAverageThermoSensorIndex = 4;

        #endregion

        #region DAQ Status Definition

        public const int gkDarkBoxStatusBitMask = 0x0001;
        public const int gkADCPLLStatusBitMask = 0x0002;
        public const int gkADCBiasStatusBitMask = 0x0004;
        public const int gkRunningStatusBitMask = 0x0018;

        public const UInt16 gkDarkBoxOpen = 0x0001;
        public const UInt16 gkDarkBoxClosed = 0x0000;
        public const UInt16 gkADCPLLError = 0x0002;
        public const UInt16 gkADCPLLOK = 0x0000;
        public const UInt16 gkADCBiasOn = 0x0004;
        public const UInt16 gkADCBiasOff = 0x0000;

        #endregion

        #region Error Code

        public const int gkecBaseCode = 0x8000;

        public const int gkecNoResponse = gkecBaseCode + 0x0001;
        public const int gkecInvalidResponse = gkecBaseCode + 0x0002;
        public const int gkecParseMsgError = gkecBaseCode + 0x0003;
        public const int gkecSendCommandError = gkecBaseCode + 0x0004;

        #endregion 

        #endregion

        #region Global Variables

        #region Fitting Purpose

        public UInt32 gFittingLowBand = 2550;
        public UInt32 gFittingUpBand = 5500;

        public Fitting gFitting;

        public bool gIsFittingDone = false;

        public bool gIsInFittingProcess = false;

        public bool gIsAutoFitting = true;

        public string gFittingPythonFile = "";

        public string gFittingDataFile = "";

        private EventWaitHandle gFittingStartEvent = new EventWaitHandle(false, EventResetMode.AutoReset);

        public Thread gFittingThread;

        #endregion 

        #region Result Related


        public int gMinimumDisplayEnergyCount = 10;

        public float gPeakLowLimit = gkPeakLowLimit;

        public float gPeakHighLimit = gkPeakHighLimit;

        public string gReportCountMapCopy = "";

        public string gReportNotes = "";

        public double gEnergyArea = 0.0f;

        public int[] gPixelNumToPositionMap = new int[gkTotalPixelNumbers];

        #endregion

        #region Configuration Parameters

        public int gSelectedQualifiedType = 0;

        public string gReportLogo = gkTOFTEKImageRoot + "\\" + gkTOFTEKLogo;

        public string gReportHeaderENG = "TOFTEK Results Report";

        public string gReportHeaderCHA = "无锡通透光电科技有限公司测试报告";

        public string gQualifiedResolutionFileName = "";

        public string gQualifiedCountFileName = "";

        public string gApplicationName = "闪烁晶体性能检测实验室 | Scintillator Performance Measurement Lab";
        public string gLinkButtonName = "通透光电 | TOFTEK";
        public string gLinkWebpage = "http://www.toftek.com";
        public bool gEnablePixelReverse = false;

        public bool gIsIncludeResolutionGreyPic = false;
        public bool gIsIncludeEnergyGreyPic = false;
        public bool gIsIncludeCountGreyPic = false;

        #endregion

        public int gChecksumDataChecksum= 0;

        ConcurrentQueue<cRawData> gChecksumDataQueue = new ConcurrentQueue<cRawData>();

        List<cRawData> gLightShareRawData = new List<cRawData>( );

        public bool gIsLightDivide = true;

        public UInt32 gLightSharePixelNo = 0;

        public bool gSwitchDisplay = false;

        public byte gDisplayTracker = 0;

        public bool gIsIncludeResolutionInReport = false;

        public bool gIsIncludeEnergySpectrumInReport = false;

        public bool gIsIncludeEnergyCountInReport = false;

        public bool gIsAutoAdjustVbias = false;

        public bool gIsUseDifferentRangesForPixels = false;

        public bool gIsRunDebugMode = false;

        public bool gRefreshAllEnergyDiagram = false;

        public float gAverageTemperature = 0f;

        public bool gIsJustGUIStart = true;

        public int gLastErrorCode = 0;

        public bool gIsUSBDevicePluggedIn = false;

        public cErrorCode gErrorOutput;

        public int gTemperaryTriggerType = (int)cScanParameters.eScanTriggerType.Unknown;

        public int gTemperaryTriggerCount = -1;

        public XmlDocument gOriginConfigDocument = new XmlDocument();

        public bool gIsDataCollectionInProgress = false;

        public enum gLanguageVersion {Chinese = 0, English = 1 };

        public int gBinSize = 100;

        public Dictionary<string, cScanParameters> gDictProtocolScanParameters = new Dictionary<string, cScanParameters> ( );

        public cScanParameters gSelectedScanParametersForDataAcq = new cScanParameters();

        //TO BE DELETED
        //public List<List<cCommand>> gEnergyData = new List<List<cCommand>>();

        public Dictionary<string, string> gConfigurationDict = new Dictionary<string, string> ( );

        public Dictionary<string, Dictionary<string, string>> gProtocolsDict = new Dictionary<string, Dictionary<string, string>>();

        public static System.Windows.Forms.Timer gRefreshTimer = new System.Windows.Forms.Timer ( );

        public static System.Windows.Forms.Timer gUSBReConnectTimer = new System.Windows.Forms.Timer ( );

        public bool gIsD2XXDLLLoadOK = false;

        public UInt32 gD2XXDLLVersion = 0;

        public FTDI.FT_DEVICE_INFO_NODE gDeviceNode = new FTDI.FT_DEVICE_INFO_NODE();

        public FTDI gFTDI = new FTDI();

        private EventWaitHandle FTDIDequeueStart = new EventWaitHandle ( false, EventResetMode.AutoReset );
        private EventWaitHandle FTDIInqueueStart = new EventWaitHandle(false, EventResetMode.AutoReset);

        public Thread gFTDIInqueueThread;

        public Thread gFTDIDequeueThread;

        public byte[ ] gCommandTypes = new byte[ ] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x0A, 0x0B, 0x0D };


        //Make it thread safe
        ConcurrentQueue<cRawData> gRawDataQueue = new ConcurrentQueue<cRawData>();

        ConcurrentQueue<cCommand> gCommandQueue = new ConcurrentQueue<cCommand>();

        //TO BE DELETED
        //ConcurrentQueue<cCommand> gSendCommandQueue = new ConcurrentQueue<cCommand>();

        //TO BE DELETED
        //Queue<byte> gInputBytesQueue = new Queue<byte> ( );

        //TO BE DELETED
        //public Dictionary<byte, cPixelInfo> gPixelDataDictionary = new Dictionary<byte, cPixelInfo> ( );

        public UInt16 gFPGAFirmwareVersion = 0;

        int gSubModulePerBankRow = 2;
        int gSubModulePerBankCol = 2;
        int gPixelPerSubModuleRow = 8;
        int gPixelPerSubModuleCol = 8;
        int gPixelPerRow = 16;
        int gPixelPerCol = 16;

        byte[] gEngeryCont = new byte[gkTotalPixelNumbers];

        int[] gRecvDataCount = new int[gkTotalPixelNumbers];

        public ConcurrentQueue<int> gLightSharePixelNoArray = new ConcurrentQueue<int>( );

        public Dictionary<int, int> gNewLinePixel = new Dictionary<int, int>( );

        public int gMaxPixelPerLine = 0;

        public List<cRawData>[] gEventDataInfo = new List<cRawData>[gkTotalPixelNumbers];

        UInt32 gTotalEventCount = 0;

        UInt32 gTriggerEventCount = 0;

        public int gMaxEnergyCount = 65536;

        public int gMatchedPixelCount = 0;

        public bool gCollectionProcessDone = false;

        public int gDataACQState = cDataACQState.gkIDLE;

        public bool gIsDataReadyToSave = false;

        //This array is used to store calibration slope and offset
        public cCalibrationBuffer gCalibrationBuffer = new cCalibrationBuffer(gkTotalPixelNumbers, 2);

        public cCalibrationBuffer gSinglePixelCalibrationBuffer = new cCalibrationBuffer(gkTotalPixelNumbers, 2);

        public bool gIsUnknownDevice = true;

        public bool gIsReplugOK = false;

        public Dictionary<int, cEnergyResolutionQualified> gEnergyResolutionQualifiedLevel = new Dictionary<int, cEnergyResolutionQualified> ( );

        public Dictionary<int, cEnergyCountQualified> gEnergyCountQualifiedLevel = new Dictionary<int, cEnergyCountQualified> ( );

        public Dictionary<int, cPixelPosition> gSinglePxielPositionMap = new Dictionary<int, cPixelPosition>();

        #endregion

        #region Clear Memory 
        /// <summary>
        /// This is used to clear shared queue and list
        /// </summary>
        void ClearMemory( ) {

            //Clear the data event
            foreach( List<cRawData> myList in gEventDataInfo ) {

                myList.Clear( );
            
            }

            cRawData myEmptyQueueTemp = new cRawData( );

            while( !gRawDataQueue.IsEmpty ) {

                gRawDataQueue.TryDequeue( out myEmptyQueueTemp );
            
            }

            cCommand myEmptyCommandQueue = new cCommand( );

            while( !gCommandQueue.IsEmpty ) {

                gCommandQueue.TryDequeue( out myEmptyCommandQueue );
            
            }

        
        }

        #endregion

        #region InitializeParameter

        public void InitializeParameter(){

            gTotalEventCount = 0;

            for (int i = 0; i < gkTotalPixelNumbers; i++) {

                gEngeryCont[i] = 0;

                gRecvDataCount[i] = 0;

                gEventDataInfo[i] = new List<cRawData>();
            
            }

            gMatchedPixelCount = 0;

            cRawData myEmptyQueueTemp = new cRawData();

            while (!gRawDataQueue.IsEmpty) {

                gRawDataQueue.TryDequeue(out myEmptyQueueTemp);
            
            }

            //Clear all fitting data
            for (int i = 0; i < gkTotalPixelNumbers; i++) {

                gEventDataInfo[i].Clear();
            
            }

            //Trigger Count reset
            gTemperaryTriggerCount = 0;

            //Clear fitting
            gIsInFittingProcess = false;

            gIsDataReadyToSave = false;

            int myClearInt = 0;

            while( !gLightSharePixelNoArray.IsEmpty ) {

                gLightSharePixelNoArray.TryDequeue( out myClearInt );

            }

            gNewLinePixel.Clear( );

            gMaxPixelPerLine = 0;

            gLightShareRawData.Clear( );

            gReportNotes = "";

            gEnergyArea = 0.0f;
        
        
        }

        #endregion

        #region InitializeLanguageVersion

        public void InitializeLanguageVersion(int pLanguageVersion) {


            if (pLanguageVersion == (int)gLanguageVersion.Chinese) {
             
                grpboxEventData.Text = "事件数据";
                grpboxAcqOption.Text = "采集选项";
                lblEventCount.Text = "事件数";
                //lblDisplayEnergyLine.Text = "显示能量图";
                lblDisplayEnergyLine.Text = "用以下数据范围自动拟合";
                btnStartDataCollection.Text = "开始";
                btnExport.Text = "导出";
                lblProtocol.Text = "扫描协议";
                grpDataAnalysis.Text = "事件数量分析";
                //lblStatus.Text = "状态";
                lblPRPATriggerType.Text = "扫描结束条件";
                lblPRPATriggerValue.Text = "参数值";
                grpTempStatus.Text = "温度监测";
                btnGenerateReport.Text = "生成报告";
                grpReportNote.Text = "报告备注";
                lblPeakValue.Text = "能量合格值范围";
                lblRangeTo.Text = "至";
                btnMergeReport.Text = "合并报告";
                lblTimePeriod.Text = "计时器";
                lblLightSharePixelCount.Text = "晶体数量";
                lblQuilifiedType.Text = "合格判断条件";
                lblOrderNo.Text = "订单#";
                lblArrayNo.Text = "阵列#";
                

            } else if (pLanguageVersion == (int)gLanguageVersion.English) {

                grpboxEventData.Text = "Event Data";
                grpboxAcqOption.Text = "Acquisition Options";
                lblEventCount.Text = "Event Count";
                //lblDisplayEnergyLine.Text = "Display Energy Graph";
                lblDisplayEnergyLine.Text = "Auto Fitting With Data Range";
                btnStartDataCollection.Text = "Start";
                btnExport.Text = "Export";
                lblProtocol.Text = "Protocol";
                grpDataAnalysis.Text = "Data Analysis";
                //lblStatus.Text = "Status";
                lblPRPATriggerType.Text = "Trigger Type";
                lblPRPATriggerValue.Text = "Trigger Value";
                grpTempStatus.Text = "Temperature";
                btnGenerateReport.Text = "Generate Report";
                grpReportNote.Text = "ReportNotes";
                lblPeakValue.Text = "Energy Count Qualified Range";
                lblRangeTo.Text = "To";
                lblLightSharePixelCount.Text = "Pixel Count";
                lblQuilifiedType.Text = "Qualified Type";
                lblOrderNo.Text = "Order#";
                lblArrayNo.Text = "Array#";

            }

            
        
        
        }

        #endregion

        #region InitializeGUIParameter

        public void InitializeGUIParameter() {

            if (gSelectedLanguage == (int)gLanguageVersion.Chinese) {

                InitializeLanguageVersion(gSelectedLanguage);

            } else if (gSelectedLanguage == (int)gLanguageVersion.English) {

                InitializeLanguageVersion(gSelectedLanguage);
            
            }

            //txtTriggerEvent.Text = gTriggerEventCount.ToString();

            btnBoxStatus.Image = Resources.LockWhiteBlack;

            //Todo: USB PLUG
            #region USB Device Check
            //btnStartDataCollection.Visible = false;

            gIsJustGUIStart = false;

            gIsUSBDevicePluggedIn = true;

            #endregion

            btnDontTouch.Visible = false;

            vprgBar1stSensor.Minimum = 0;
            vprgBar1stSensor.Maximum = 100;
            vprgBar1stSensor.Value = 10;
            vprgBar1stSensor.Visible = false;
            lbl1stSensor.Visible = false;
            vprgBar2rdSensor.Visible = false;
            lbl2ndSensor.Visible = false;
            vprgBar3rdSensor.Visible = false;
            lbl3rdSensor.Visible = false;
            vprgBar4thSensor.Visible = false;
            lbl4thSensor.Visible = false;
            btnBoxStatus.Visible = false;
            //lblStatus.Visible = false;

            txtPeakLowLimit.Text = gkPeakLowLimit.ToString("0.0#");
            txtPeakHighLimit.Text = gkPeakHighLimit.ToString("0.0#");

            txtFittingLowBand.Text = gFittingLowBand.ToString( "" );
            txtFittingUpBand.Text = gFittingUpBand.ToString( "" );

            btnGenerateReport.Visible = false;

            grpReportNote.Visible = false;

            grpReportNote.Visible = false;

            picProgress.Visible = false;

            lblLoading.Visible = false;

            cbQualifiedType.SelectedIndex = gSelectedQualifiedType;

            lblLinkLable.Text = gLinkButtonName;

            this.Text = gApplicationName;

            //button1.Visible = false;
            
        }

        #endregion

        #region Construct

        public Demo( ) {

            FTDI.FT_STATUS myStatus = FTDI.FT_STATUS.FT_OTHER_ERROR;

            int myErrorCode = 0;

            InitializeComponent ( );
            
            if( Environment.GetEnvironmentVariable( gkTOFTEKDebugMode ) != null ) {

                if( Environment.GetEnvironmentVariable( gkTOFTEKDebugMode, EnvironmentVariableTarget.Machine ).ToLower( ) == "yes" ) {

                    gIsRunDebugMode = true;

                } else {

                    gIsRunDebugMode = false;

                }

            }

            if( !gIsRunDebugMode ) {

                tbMainControl.TabPages.RemoveAt( 1 );

            }

            if( !Directory.Exists( gkTOFTEKTempRoot ) ) {

                Directory.CreateDirectory( gkTOFTEKTempRoot );
            
            }

            //Load Configuration parameters
            LoadConfigurationParameter();

            #region Deal with protocols

            //Use to load all available protocol under the default protocol directory
            FindAllProtocolFiles(gkProtocolDirectory);

            RefreshProtocolGUIList();

            #endregion

            //Initialze data queue and other DAQ parameters
            InitializeParameter();

            //Based on configuration settings, arrage GUI layout 
            //This is also set the language 
            InitializeGUIParameter();

            gFitting = new Fitting(this);

            gErrorOutput = new cErrorCode(gSelectedLanguage);

            if (System.IO.File.Exists(cProgramDirectory.gkCalibrationFolder + cProgramDirectory.gkCalibrationFileName)) {

                LoadCalibrationFile(cProgramDirectory.gkCalibrationFolder + cProgramDirectory.gkCalibrationFileName);

            } else {

                gErrorOutput.OutPutErrorMessage(cErrorCode.gkecFileSubError_MissSystemFileCalibration, "");
            }

            #region Load Single Pixel File

            if (System.IO.File.Exists(cProgramDirectory.gkCalibrationFolder + cProgramDirectory.gkSinglePixelCalibrationFile)) {

                LoadSinglePixelCalibrationFile(cProgramDirectory.gkCalibrationFolder + cProgramDirectory.gkSinglePixelCalibrationFile);

            } else {

                gErrorOutput.OutPutErrorMessage(cErrorCode.gkecFileSubError_MissSystemFileCalibration, "");
            }

            #endregion

            if (gIsUseDifferentRangesForPixels) {

                #region Load Qualified Range File

                if (System.IO.File.Exists(cProgramDirectory.gkQualifiedLevelFolder + gQualifiedResolutionFileName)) {

                    LoadQualifiedEnergyResolutionFile(cProgramDirectory.gkQualifiedLevelFolder + gQualifiedResolutionFileName);

                } else {

                    gErrorOutput.OutPutErrorMessage(cErrorCode.gkecFileSubError_MissSystemFileQuailified, "");

                }

                if (System.IO.File.Exists(cProgramDirectory.gkQualifiedLevelFolder + gQualifiedCountFileName)) {

                    LoadQualifiedEnergyCountFile(cProgramDirectory.gkQualifiedLevelFolder + gQualifiedCountFileName);

                } else {

                    gErrorOutput.OutPutErrorMessage(cErrorCode.gkecFileSubError_MissSystemFileQuailified, "");

                }

                #endregion

            }

            #region Load pisition file

            if (System.IO.File.Exists(cProgramDirectory.gkCalibrationFolder + cProgramDirectory.gkBasePixelPositionFile)) {

                LoadPixelPositioningFile(cProgramDirectory.gkCalibrationFolder + cProgramDirectory.gkBasePixelPositionFile);

            } else {

                gErrorOutput.OutPutErrorMessage(cErrorCode.gkecFileSubError_MissSystemFileCalibration, "");

            }

            #endregion

            //Start the thread
            gFTDIInqueueThread = new Thread(this.FTDIInQueue);

            gFTDIDequeueThread = new Thread ( this.FTDIDeQueue );

            gFittingThread = new Thread(this.FittingThread);

            myStatus = ReConnectDevice ( );

            gFTDIInqueueThread.Start ( );

            gFTDIDequeueThread.Start();

            gFittingThread.Start();

            Thread.Sleep(1000);

            //If reconnect the device succeed, then acquire the firmware info
            #region Handshake

            if (myStatus == FTDI.FT_STATUS.FT_OK) {

                if (gFTDI != null) {

                    //Give 100ms warm up
                    //Thread.Sleep(1000);

                    myErrorCode = InitDataCollectionBoard();

                    if (myErrorCode != 0) {

                        //Log Communication Error

                    } else {

                        //TODO: USB PLUG
                        /*
                        gIsJustGUIStart = false;

                        gIsUSBDevicePluggedIn = true;
                        
                        btnStartDataCollection.Visible = true;

                        //Don't need to warning again
                        btnUSBDeviceStatus.Visible = false;
                         */

                        //First stop ACQ no matter what
                        StartDataCollection(false);

                    }

                    myErrorCode = GetThermoInfo(gkRequestAvgThermoSensorIndex, out gAverageTemperature);

                    if (myErrorCode == 0) {

                        //MessageBox.Show(myThermoInfo.ToString());
                        vprgBarAveTemp.Value = (int)gAverageTemperature;
                        lblAverage.Text = gAverageTemperature.ToString("F1");

                        if( gAverageTemperature > 40.0f ) {

                            vprgBarAveTemp.Color = Color.Red;

                        } else {

                            vprgBarAveTemp.Color = Color.SpringGreen;
                        
                        }
                    
                    }

                }

            } else {

                //TODO: USB PLUG 
                /*
                gIsUSBDevicePluggedIn = false;
                btnStartDataCollection.Visible = false;
                btnUSBDeviceStatus.Image = Resources.Hardware_Usb_Logo;
                */
            }

            #endregion

            //Start a one second watch dog timer
            //This timer can also be used for time trigger 
            gRefreshTimer.Tick += new EventHandler ( RefreshTimerEventProcess );
            gRefreshTimer.Interval = 1000;

            gUSBReConnectTimer.Tick += new EventHandler ( USBReConnectTimerEventProcess );
            gUSBReConnectTimer.Interval = 2000;

            //gRefreshTimer.Start ( );

        }

        #endregion

        #region StartCollectionWithGUIUpdate

        void StartCollectionWithGUIUpdate(bool pIsStart) {

            if (pIsStart == false) {

                //Stop DAQ
                #region Stop DAQ

                gCollectionProcessDone = true;

                BeginInvoke(((Action)(() => btnStartDataCollection.Image = Resources.CircledPlayBlack1)));

                if (gSelectedLanguage == (int)gLanguageVersion.Chinese) {

                    BeginInvoke(((Action)(() => btnStartDataCollection.Text = "开始")));

                } else {

                    BeginInvoke(((Action)(() => btnStartDataCollection.Text = "Start")));

                }

                StartDataCollection(false);

                #endregion

            } else {

                #region Start DAQ

                BeginInvoke(((Action)(() => btnStartDataCollection.Image = Resources.SimpleCancelBlack)));

                if (gSelectedLanguage == (int)gLanguageVersion.Chinese) {

                    BeginInvoke(((Action)(() => btnStartDataCollection.Text = "停止")));

                } else {

                    BeginInvoke(((Action)(() => btnStartDataCollection.Text = "Stop")));

                }

                StartDataCollection(true);

                #endregion

            }
        
        }

        void StartCollectionWithGUIUpdate(bool pIsStart, bool pIsNotGoodOne) {

            if (pIsStart == false) {

                //Stop DAQ
                #region Stop DAQ

                gCollectionProcessDone = pIsNotGoodOne;

                MessageBox.Show("Error msg received");

                BeginInvoke(((Action)(() => btnStartDataCollection.Image = Resources.CircledPlayBlack1)));

                if (gSelectedLanguage == (int)gLanguageVersion.Chinese) {

                    BeginInvoke(((Action)(() => btnStartDataCollection.Text = "开始")));

                } else {

                    BeginInvoke(((Action)(() => btnStartDataCollection.Text = "Start")));

                }

                StartDataCollection(false);

                #endregion

            } else {

                #region Start DAQ

                BeginInvoke(((Action)(() => btnStartDataCollection.Image = Resources.SimpleCancelBlack)));

                if (gSelectedLanguage == (int)gLanguageVersion.Chinese) {

                    BeginInvoke(((Action)(() => btnStartDataCollection.Text = "停止")));

                } else {

                    BeginInvoke(((Action)(() => btnStartDataCollection.Text = "Stop")));

                }

                StartDataCollection(true);

                #endregion

            }

        }

        #endregion

        #region USBReConnectTimerEventProcess

        private void USBReConnectTimerEventProcess( Object pObject, EventArgs pEventArgs ) {

            if ( gIsUnknownDevice && gIsReplugOK == false ) {

                //The device is unknown device and did not replug
                ReConnectDevice ( );

            } else {

                gUSBReConnectTimer.Stop ( );
            
            }

        }

        #endregion

        #region RefreshTimerEventProcess

        private void RefreshTimerEventProcess( Object pObject, EventArgs pEventArgs ) {

            //gRefreshTimer.Stop();

            //For first version, disable this part
            
            #region Check USB Plugged in
            /*
            if (gIsUSBDevicePluggedIn == false) {
                
                btnUSBDeviceStatus.Visible = !btnUSBDeviceStatus.Visible;
                
                //No use detected yet
                FTDI.FT_STATUS myStatus = FTDI.FT_STATUS.FT_OTHER_ERROR;

                myStatus = gFTDI.OpenByIndex(0);

                //If reconnect the device succeed, then acquire the firmware info
                #region Handshake

                if (myStatus == FTDI.FT_STATUS.FT_OK) {

                    if (gFTDI != null) {

                        InitDataCollectionBoard();

                        gIsUSBDevicePluggedIn = true;
                        //btnUSBDeviceStatus.Image = Resources.Hardware_Usb_Connected;

                        //Don't need to warning again
                        btnUSBDeviceStatus.Visible = false;

                        //Only when USB is plugged and it is able to do DAQ
                        btnStartDataCollection.Visible = true;


                    }
                 


                }

                #endregion

            }
            */
            #endregion
            

            if (gCollectionProcessDone == false) {

                if (gTemperaryTriggerType == (int)cScanParameters.eScanTriggerType.TimeTrigger) {

                    gTemperaryTriggerCount--;

                    txtTimePeriod.Text = gTemperaryTriggerCount.ToString();

                    if (gTemperaryTriggerCount <= 0) {

                        //Stop data acq
                        StartCollectionWithGUIUpdate(false);

                        SentFittingNotice();

                    }

                }

            }

            //gRefreshTimer.Start();

        }

        #endregion

        #region CyclePort

        FTDI.FT_STATUS CyclePort( ) {


            UInt32 myNumDevs = 0;

            FTDI.FT_STATUS myFTDIStatus = FTDI.FT_STATUS.FT_OTHER_ERROR;

            FTDI.FT_DEVICE_INFO_NODE[] myDeviceNodes = new FTDI.FT_DEVICE_INFO_NODE[myNumDevs];

            try {

                #region Cycle Port Test

                gFTDI.CyclePort( );

                gFTDI.Close( );

                //This delay is required because it needed for Windows to
                //correctly install the driver
                Thread.Sleep( 3000 );

                //After cycle port, re-do connection
                #region Create device list

                myFTDIStatus = gFTDI.GetNumberOfDevices( ref myNumDevs );

                if( myFTDIStatus != FTDI.FT_STATUS.FT_OK ) {

                    return myFTDIStatus;

                }

                myDeviceNodes = new FTDI.FT_DEVICE_INFO_NODE[myNumDevs];

                myFTDIStatus = gFTDI.GetDeviceList( myDeviceNodes ); //FTDI.FT_CreateDeviceInfoList ( ref myNumDevs );

                if( myFTDIStatus != FTDI.FT_STATUS.FT_OK ) {

                    return myFTDIStatus;

                } else {

                    gDeviceNode.SerialNumber = myDeviceNodes[0].SerialNumber;
                    gDeviceNode.Description = myDeviceNodes[0].Description;

                    myFTDIStatus = gFTDI.OpenByIndex( 0 );

                    if( myFTDIStatus != FTDI.FT_STATUS.FT_OK ) {

                        return myFTDIStatus;

                    }

                    gFTDI.SetTimeouts( 1, 1 );

                }


                #endregion

                #endregion

            } catch( Exception pException ) {

                myFTDIStatus = FTDI.FT_STATUS.FT_OTHER_ERROR;
            
            }


            return myFTDIStatus; 

        }


        #endregion

        #region ReConnectDevice

        private FTDI.FT_STATUS ReConnectDevice( ) {

            UInt32 myNumDevs = 0;

            FTDI.FT_STATUS myFTDIStatus = FTDI.FT_STATUS.FT_OTHER_ERROR;

            try {

                #region Get the Imported DLL Version

                myFTDIStatus = gFTDI.GetLibraryVersion ( ref gD2XXDLLVersion ); //FTDI.FT_GetLibraryVersion ( ref gD2XXDLLVersion );

                if (myFTDIStatus != FTDI.FT_STATUS.FT_OK) {

                    //Error

                    gIsD2XXDLLLoadOK = false;

                } else {

                    gIsD2XXDLLLoadOK = true;


                }

                #endregion

                #region Create device list

                myFTDIStatus = gFTDI.GetNumberOfDevices ( ref myNumDevs );

                if (myFTDIStatus != FTDI.FT_STATUS.FT_OK) {

                    return myFTDIStatus;

                }

                FTDI.FT_DEVICE_INFO_NODE[] myDeviceNodes = new FTDI.FT_DEVICE_INFO_NODE[myNumDevs];

                myFTDIStatus = gFTDI.GetDeviceList ( myDeviceNodes ); //FTDI.FT_CreateDeviceInfoList ( ref myNumDevs );

                if (myFTDIStatus != FTDI.FT_STATUS.FT_OK) {

                    gIsUnknownDevice = true;

                    return myFTDIStatus;

                } else {

                    gDeviceNode.SerialNumber = myDeviceNodes[0].SerialNumber;
                    gDeviceNode.Description = myDeviceNodes[0].Description;

                    myFTDIStatus = gFTDI.OpenByIndex ( 0 );

                    if (myFTDIStatus != FTDI.FT_STATUS.FT_OK) {

                        return myFTDIStatus;

                    }

                    gFTDI.SetTimeouts ( 1, 1 );

                }

                
                #endregion

                //Cycle Port
                CyclePort( );


            } catch (Exception) {


                myFTDIStatus = FTDI.FT_STATUS.FT_OTHER_ERROR;
            
            }

            return myFTDIStatus;
        
        }

        #endregion

        #region InitDataCollectionBoard

        private int InitDataCollectionBoard() {

            int myStatus = 0;

            UInt16 myDAQStatus = 0;

            myStatus = InitHandshaking(out gFPGAFirmwareVersion);

            if (myStatus != 0) {

                return myStatus;
            
            }

            myStatus = CheckStatus(out myDAQStatus);

            if (myStatus != 0) {

                return myStatus;

            } else { 
                
                
            
            }


            return myStatus;


        }

        #endregion

        #region HandShacking

        private int InitHandshaking(out UInt16 pDAQFirmVersion) {

            int myStatus = 0;

            pDAQFirmVersion = 0xFFFF;

            byte[] myData = new byte[2];

            myData[0] = 0x00;

            myData[1] = 0x00;


            cCommand myCommand = new cCommand();

            #region Send Handshaking message

            myStatus = SendCommand(gkCmdHandshaking, myData, gkMessageDataSize);

            if (myStatus == 0) {


                //Send Command Correctly

                //Then check the return message
                //50ms to get the response
                Thread.Sleep(50);

                myStatus = ReceiveCommand(gkCmdHandshaking, ref myCommand);

                if (myStatus == 0) {

                    if (myCommand.mCommand == gkCmdHandshaking) {

                        //Good
                        pDAQFirmVersion = (UInt16)(((UInt16)myCommand.mData[0] << 8) | ((UInt16)myCommand.mData[1]));


                    } else {


                        return gkecInvalidResponse;


                    }


                } else {

                    return gkecParseMsgError;

                }



            } else {

                return gkecSendCommandError;


            }

            #endregion      
        
            return myStatus;

        }

        #endregion

        #region CheckStatus
        
        private int CheckStatus(out UInt16 pStatus) {

            int myStatus = 0;

            pStatus = 0xFFFF;

            byte[] myData = new byte[2];

            myData[0] = 0x00;

            myData[1] = 0x00;

            cCommand myCommand = new cCommand();

            #region Send Check Status message

            myStatus = SendCommand(gkCmdCheckCurrentStatus, myData, gkMessageDataSize);

            if (myStatus == 0) {


                //Send Command Correctly

                //Then check the return message
                //50ms to get the response
                Thread.Sleep(50);

                myStatus = ReceiveCommand(gkCmdCheckCurrentStatus, ref myCommand);

                if (myStatus == 0) {

                    if (myCommand.mCommand == gkCmdCheckCurrentStatus) {

                        //Good
                        pStatus = (UInt16)(((UInt16)myCommand.mData[0] << 8) | ((UInt16)myCommand.mData[1]));

                    } else {


                        return gkecInvalidResponse;


                    }


                } else {

                    return gkecParseMsgError;

                }



            } else {

                return gkecSendCommandError;


            }

            #endregion 
            
            return myStatus;

        }

        #endregion

        #region GetThermoInfo

        private int GetThermoInfo(byte pIndex, out int pThermoInfo) {

            int myStatus = 0;

            pThermoInfo = 0;

            byte[] myData = new byte[2];

            myData[0] = 0x00;

            myData[1] = 0x01;

            cCommand myCommand = new cCommand();

            #region Send Check Status message

            myStatus = SendCommand(gkCmdGetThermoInfo, myData, gkMessageDataSize);

            if (myStatus == 0) {


                //Send Command Correctly

                //Then check the return message
                //50ms to get the response
                Thread.Sleep(150);

                myStatus = ReceiveCommand(gkCmdGetThermoInfo, ref myCommand);

                if (myStatus == 0) {

                    if (myCommand.mCommand == gkCmdGetThermoInfo) {

                        //Good
                        if (pIndex == gkRequestAvgThermoSensorIndex) {
                         
                            pThermoInfo = (int)myCommand.mData[1];

                        }  

                    } else {


                        return gkecInvalidResponse;


                    }


                } else {

                    return gkecParseMsgError;

                }



            } else {

                return gkecSendCommandError;


            }

            #endregion

            return myStatus;
        
        }



        private int GetThermoInfo( byte pIndex, out float pThermoInfo ) {

            int myStatus = 0;

            pThermoInfo = 0.0f;

            Int16 myReadSignedData = 0;

            byte[] myData = new byte[2];

            myData[0] = 0x00;

            myData[1] = 0x01;

            cCommand myCommand = new cCommand( );

            #region Send Check Status message

            myStatus = SendCommand( gkCmdGetThermoInfo, myData, gkMessageDataSize );

            if( myStatus == 0 ) {


                //Send Command Correctly

                //Then check the return message
                //50ms to get the response
                Thread.Sleep( 150 );

                myStatus = ReceiveCommand( gkCmdGetThermoInfo, ref myCommand );

                if( myStatus == 0 ) {

                    if( myCommand.mCommand == gkCmdGetThermoInfo ) {

                        //Good
                        if( pIndex == gkRequestAvgThermoSensorIndex ) {

                            //pThermoInfo = ( int )myCommand.mData[1];
                            myReadSignedData = (Int16) ( ( Int16 )( ( Int16 )myCommand.mData[0] << 8 ) | ( Int16 )myCommand.mData[1]);

                            pThermoInfo = (float)(myReadSignedData * 1.0 / 256.0);

                        }

                    } else {


                        return gkecInvalidResponse;


                    }


                } else {

                    return gkecParseMsgError;

                }



            } else {

                return gkecSendCommandError;


            }

            #endregion

            return myStatus;

        } 


        #endregion

        #region Read/WriteADCMode

        private int WriteADCModeReg(byte pData) {

            int myStatus = 0;

            byte[] myData = new byte[2];

            myData[0] = 0x01;

            myData[1] = pData;

            cCommand myCommand = new cCommand();

            #region Send Write Mode Register 

            myStatus = SendCommand(gkCmdWriteModeReg, myData, gkMessageDataSize);

            if (myStatus == 0) {


                //Send Command Correctly

                //Then check the return message
                //50ms to get the response
                Thread.Sleep(50);

                myStatus = ReceiveCommand(gkCmdWriteModeReg, ref myCommand);

                if (myStatus == 0) {

                    if (myCommand.mCommand == gkCmdWriteModeReg) {

                        //Good
                        

                    } else {


                        return gkecInvalidResponse;


                    }


                } else {

                    return gkecParseMsgError;

                }



            } else {

                return gkecSendCommandError;


            }

            #endregion

            return myStatus;
        
        
        
        }

        private int ReadADCModeReg(out byte pData) {

            int myStatus = 0;

            pData = 0xFF;

            byte[] myData = new byte[2];

            myData[0] = 0x00;

            myData[1] = 0x00;

            cCommand myCommand = new cCommand();

            #region Send Read Mode Register

            myStatus = SendCommand(gkCmdWriteModeReg, myData, gkMessageDataSize);

            if (myStatus == 0) {


                //Send Command Correctly

                //Then check the return message
                //50ms to get the response
                Thread.Sleep(50);

                myStatus = ReceiveCommand(gkCmdWriteModeReg, ref myCommand);

                if (myStatus == 0) {

                    if (myCommand.mCommand == gkCmdWriteModeReg) {

                        //Good
                        pData = myCommand.mData[1];

                    } else {


                        return gkecInvalidResponse;


                    }


                } else {

                    return gkecParseMsgError;

                }



            } else {

                return gkecSendCommandError;


            }

            #endregion

            return myStatus;
        
        
        }

        #endregion 

        #region WriteLightShareADCThresholdReg

        private int WriteLightShareADCThresholdReg( UInt16 pData ) {

            int myStatus = 0;

            byte[] myData = new byte[2];

            myData[0] = (byte)((pData >> 8) & 0xFF);

            myData[1] = (byte)(pData & 0xFF);

            cCommand myCommand = new cCommand( );

            #region Send Write Mode Register

            myStatus = SendCommand( gkCmdWriteLightShareADCThresholdReg, myData, gkMessageDataSize );

            if( myStatus == 0 ) {


                //Send Command Correctly

                //Then check the return message
                //50ms to get the response
                Thread.Sleep( 50 );

                myStatus = ReceiveCommand( gkCmdWriteModeReg, ref myCommand );

                if( myStatus == 0 ) {

                    if( myCommand.mCommand == gkCmdWriteModeReg ) {

                        //Good


                    } else {


                        return gkecInvalidResponse;


                    }


                } else {

                    return gkecParseMsgError;

                }



            } else {

                return gkecSendCommandError;


            }

            #endregion

            return myStatus;



        }

        #endregion 

        #region Read/WriteADCConfig

        private int ReadADCConfig(byte pIndex, out byte pData) {

            pData = 0xFF;

            int myStatus = 0;

            byte[] myData = new byte[2];

            myData[0] = 0x00;

            myData[1] = 0x00;

            cCommand myCommand = new cCommand();

            myData[0] = (byte)(pIndex | 0x04);


            #region Send Read ADC Config Register

            myStatus = SendCommand(gkCmdADConfigAccess, myData, gkMessageDataSize);

            if (myStatus == 0) {


                //Send Command Correctly

                //Then check the return message
                //50ms to get the response
                Thread.Sleep(50);

                myStatus = ReceiveCommand(gkCmdADConfigAccess, ref myCommand);

                if (myStatus == 0) {

                    if (myCommand.mCommand == gkCmdADConfigAccess) {

                        //Good
                        pData = myCommand.mData[1];

                    } else {


                        return gkecInvalidResponse;


                    }


                } else {

                    return gkecParseMsgError;

                }



            } else {

                return gkecSendCommandError;


            }

            #endregion

            return myStatus;



        }

        private int WriteADCConfig(byte pIndex, byte pData) {

            int myStatus = 0;

            byte[] myData = new byte[2];

            myData[0] = 0x00;

            myData[1] = pData;

            cCommand myCommand = new cCommand();

            //Data0 
            //Bit2: 1 = write, 0 = read
            //Bit1:0 Data---0 = ADC Power, 1 = ADC Test Mode, 2,3 = ADC Range
            //For a byte, first clear bit 7:3, then set bit 2 and leave bit 1:0 untouched
            myData[0] = (byte)(0x04 | (pIndex & 0x03) );

            #region Send Write ADC Config Register

            myStatus = SendCommand(gkCmdADConfigAccess, myData, gkMessageDataSize);

            if (myStatus == 0) {


                //Send Command Correctly

                //Then check the return message
                //50ms to get the response
                Thread.Sleep(50);

                myStatus = ReceiveCommand(gkCmdADConfigAccess, ref myCommand);

                if (myStatus == 0) {

                    if (myCommand.mCommand == gkCmdADConfigAccess) {

                        //Good

                    } else {


                        return gkecInvalidResponse;


                    }


                } else {

                    return gkecParseMsgError;

                }



            } else {

                return gkecSendCommandError;


            }

            #endregion

            return myStatus;

        }

        #endregion

        #region Get/SetPower
        
        private int SetPower(UInt16 pIndex, UInt16 pData) {

            int myStatus = 0;

            byte[] myData = new byte[2];

            myData[0] = 0x00;

            myData[1] = 0x01;

            cCommand myCommand = new cCommand();

            UInt16 myCombineData = (UInt16)(0x8000 | (pIndex << 12) | pData);

            myData[0] = (byte) (myCombineData >> 8);

            myData[1] = (byte)myCombineData;

            #region Send Set Power 

            myStatus = SendCommand(gkCmdWritePowerConfig, myData, gkMessageDataSize);

            if (myStatus == 0) {


                //Send Command Correctly

                //Then check the return message
                //50ms to get the response
                Thread.Sleep(50);

                myStatus = ReceiveCommand(gkCmdWritePowerConfig, ref myCommand);

                if (myStatus == 0) {

                    if (myCommand.mCommand == gkCmdWritePowerConfig) {

                        //Good

                    } else {


                        return gkecInvalidResponse;


                    }


                } else {

                    return gkecParseMsgError;

                }



            } else {

                return gkecSendCommandError;


            }

            #endregion


            return myStatus;
        
        }

        private int GetPower(out UInt16 pData) {

            pData = 0xFFFF;

            int myStatus = 0;

            byte[] myData = new byte[2];

            //CLEAR WRITE BIT
            myData[0] = 0x00;

            myData[1] = 0x00;

            cCommand myCommand = new cCommand();


            #region Send Set Power

            myStatus = SendCommand(gkCmdWritePowerConfig, myData, gkMessageDataSize);

            if (myStatus == 0) {


                //Send Command Correctly

                //Then check the return message
                //50ms to get the response
                Thread.Sleep(50);

                myStatus = ReceiveCommand(gkCmdWritePowerConfig, ref myCommand);

                if (myStatus == 0) {

                    if (myCommand.mCommand == gkCmdWritePowerConfig) {

                        //Good
                        pData = (UInt16) ((UInt16)myCommand.mData[0] << 8 | (UInt16)myCommand.mData[1]); 


                    } else {


                        return gkecInvalidResponse;


                    }


                } else {

                    return gkecParseMsgError;

                }



            } else {

                return gkecSendCommandError;


            }

            #endregion


            return myStatus;

        }

        #endregion

        #region StartDataCollection

        private int StartDataCollection(bool pFlag) {

            int myStatus = 0;

            byte[] myData = new byte[2];

            myData[0] = 0x00;

            myData[1] = 0x00;
            

            cCommand myCommand = new cCommand();


            if(pFlag) {

                //0x01 means enable data collection
                myData[1] = 0x01;

            } else {

                //0x00 means disable data collection
                myData[1] = 0x00;
            
            }

            myStatus = SendCommand(gkCmdWriteACQ, myData, gkMessageDataSize);

            //This message has no reply then does not need to check the response

            if ( pFlag ) {

                //If start the data acquisition 
                //then start the timer
                gRefreshTimer.Start ( );

            } else {

                //If stop data acquisition
                //then stop timers
                gRefreshTimer.Stop ( );
            
            }

            return myStatus;
        
        
        }

        #endregion

        #region Utility Functions

        /*******************************************************************************
        * Module Name: InitializeDataAnalysisBoard
        *
        * Description: This function is used to initialize the GUI dash board.
        * 
        *******************************************************************************/

        private void InitializeDataAnalysisBoard( int pRowNumber, int pClmnNumber, object sender, PaintEventArgs e ) {

            /*int mySize = 0;
            
            dgDataAnalysisBoard.BackgroundColor = Color.Gray;

            dgDataAnalysisBoard.DefaultCellStyle.BackColor = Color.Gray;

            mySize = ( ( dgDataAnalysisBoard.Width >= dgDataAnalysisBoard.Height ) ? ( dgDataAnalysisBoard.Height ) : ( dgDataAnalysisBoard.Width ) );
            
            for ( int i = 0; i < pClmnNumber; i++ ) {

                DataGridViewColumn myClmn = new DataGridViewButtonColumn ( );

                myClmn.DataPropertyName = i.ToString();
                myClmn.Name = i.ToString ( );

                dgDataAnalysisBoard.Columns.Add ( myClmn );
            
            }

            for ( int i = 0; i < pRowNumber; i++ ) {

                dgDataAnalysisBoard.Rows.Add ( );

            }

            foreach ( DataGridViewColumn myColumn in dgDataAnalysisBoard.Columns ) {

                myColumn.Width = mySize / dgDataAnalysisBoard.Columns.Count;

            }

            foreach ( DataGridViewRow myRow in dgDataAnalysisBoard.Rows ) {

                myRow.Height = mySize / dgDataAnalysisBoard.Columns.Count;

            }
            */
            PictureBox myObject = (PictureBox)sender;

            //Based on width to design the drawing area
            myObject.Height = myObject.Width / gSubModulePerBankCol * gSubModulePerBankRow;

            // Lines between pixels.
            for ( int i = 0; i < gSubModulePerBankCol; i++ ) {

                for ( int j = 1; j < gPixelPerSubModuleCol; j++ ) {

                    e.Graphics.DrawLine ( new System.Drawing.Pen ( System.Drawing.Color.Yellow, 2f ), new Point ( myObject.Width / gSubModulePerBankCol / gPixelPerSubModuleCol * ( i * gPixelPerSubModuleCol + j ), 0 ), new Point ( myObject.Width / gSubModulePerBankCol / gPixelPerSubModuleCol * ( i * gPixelPerSubModuleCol + j ), myObject.Height ) );

                }

            }

            for ( int i = 0; i < gSubModulePerBankRow; i++ ) {

                for ( int j = 1; j < gPixelPerSubModuleRow; j++ ) {

                    e.Graphics.DrawLine ( new System.Drawing.Pen ( System.Drawing.Color.Yellow, 2f ), new Point ( 0, myObject.Height / gSubModulePerBankRow / gPixelPerSubModuleRow * ( i * gPixelPerSubModuleRow + j ) ), new Point ( myObject.Width, myObject.Height / gSubModulePerBankRow / gPixelPerSubModuleRow * ( i * gPixelPerSubModuleRow + j ) ) );

                }

            }

            // Outline.
            e.Graphics.DrawLine ( new System.Drawing.Pen ( System.Drawing.Color.Green, 3f ), new Point ( 0, 0 ), new Point ( 0, myObject.Height ) );
            e.Graphics.DrawLine ( new System.Drawing.Pen ( System.Drawing.Color.Green, 3f ), new Point ( myObject.Width - 1, 0 ), new Point ( myObject.Width - 1, myObject.Height ) );
            e.Graphics.DrawLine ( new System.Drawing.Pen ( System.Drawing.Color.Green, 3f ), new Point ( 0, 0 ), new Point ( myObject.Width, 0 ) );
            e.Graphics.DrawLine ( new System.Drawing.Pen ( System.Drawing.Color.Green, 3f ), new Point ( 0, myObject.Height - 1 ), new Point ( myObject.Width, myObject.Height - 1 ) );

            // Lines between submodules.
            for ( int i = 1; i < gSubModulePerBankCol; i++ ) {

                e.Graphics.DrawLine ( new System.Drawing.Pen ( System.Drawing.Color.Green, 3f ), new Point ( myObject.Width / gSubModulePerBankCol * i, 0 ), new Point ( myObject.Width / gSubModulePerBankCol * i, myObject.Height ) );

            }

            for ( int i = 1; i < gSubModulePerBankRow; i++ ) {

                e.Graphics.DrawLine ( new System.Drawing.Pen ( System.Drawing.Color.Green, 3f ), new Point ( 0, myObject.Height / gSubModulePerBankRow * i ), new Point ( myObject.Width, myObject.Height / gSubModulePerBankRow * i ) );

            }


            /////////////////

           
        }

        private void myAppendText( RichTextBox pRtbox, string pText, Color pColor ) {

            pRtbox.SelectionStart = pRtbox.TextLength;
            pRtbox.SelectionLength = 0;

            pRtbox.SelectionColor = pColor;
            //pRtbox.SelectionFont = pFont;
            pRtbox.AppendText ( pText + "\r\n");
            pRtbox.SelectionColor = pRtbox.ForeColor;

        }

        #endregion

        #region Communication Functions

        #region SendCommand

        int SendCommand(byte pCommand, byte[] pData, UInt32 pDataSize) {

            int myStatus = 0;

            cCommand myCommand = new cCommand(gkMessageTypeCmd, pCommand, pData);

            FTDI.FT_STATUS myFTDUStatus = FTDI.FT_STATUS.FT_OK;

            UInt32 myNumBytesWritten = 0;


            if(gFTDI.IsOpen) {

                myFTDUStatus = gFTDI.Write(myCommand.mMessage, (uint)gkCommandSize, ref myNumBytesWritten);


                if(myFTDUStatus == FTDI.FT_STATUS.FT_OK) {


                    if(gkCommandSize != myNumBytesWritten) {


                        //Writtern bytes' number is different from the number of bytes need to be wriiten
                        myStatus = -1;


                    } else {


                        myStatus = 0;

                    }


                } else {


                    myStatus = -2;
                
                }
            
            
            }




            return myStatus;
        
        }

        #endregion

        #region ReceiveCommand

        int ReceiveCommand(byte pCommand, ref cCommand pRecvCommand) {

            int myStatus = 0;

            //Give is a invalid initialized
            pRecvCommand.mCommandType = 0x00;

            try {

                pRecvCommand = gCommandQueue.Last(cCommand => (cCommand.mCommandType == gkMessageTypeCmd) && (cCommand.mCommand == pCommand));

                //gCommandQueue. RemoveAll(cCommand => (cCommand.mCommandType == gkMessageTypeCmd) && (cCommand.mCommand == pCommand));

                if(pRecvCommand.mCommandType != gkMessageTypeCmd) {

                    myStatus = -2;
                
                
                } 


            } catch {

                myStatus = -1;
            
            }


            return myStatus;
     

        }

        #endregion

        #endregion

        #region PictureBoxUpdateImage

        /*********************************************************************** 
        * Module Name: PictureBoxUpdateImage
        *
        * Description: This function will convert SubModule array data into a
        *              picture with same size with the picture box.
        * Calling Arguments: 
        *  Name              Input/Output  Type         Description 
        *  mySubModuleArray  input         byte[]       New picture data.                                       
        *
        * Returns:
        *  new bitmap image.
        *
        ***********************************************************************/

        public Bitmap PictureBoxUpdateImage(byte[] mySubModuleArray) {

            Bitmap bmp = new Bitmap(gPixelPerRow, gPixelPerCol, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
            
            System.Drawing.Imaging.ColorPalette pal = bmp.Palette;

            //create grayscale palette
            for (int i = 1; i < 253; i++) {

                pal.Entries[i] = System.Drawing.Color.FromArgb ( 255, i, i, 255);
            
            }
            pal.Entries[0] = System.Drawing.Color.FromArgb(255, 0, 0, 0);

            pal.Entries[253] = System.Drawing.Color.FromArgb ( 255, 255, 215, 0 );
            pal.Entries[254] = System.Drawing.Color.FromArgb ( 255, 255, 0, 0 );
            pal.Entries[255] = System.Drawing.Color.FromArgb ( 255, 0, 255, 0 );


            //assign to bmp
            bmp.Palette = pal;

            
            //lock it to get the BitmapData Object
            System.Drawing.Imaging.BitmapData bmData = bmp.LockBits(new Rectangle(0, 0, gPixelPerRow, gPixelPerCol), 
                System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);

            //copy the bytes
            System.Runtime.InteropServices.Marshal.Copy(mySubModuleArray, 0, bmData.Scan0, bmData.Stride * bmData.Height);

            //never forget to unlock the bitmap
            bmp.UnlockBits(bmData);

            Bitmap bmp1 = new Bitmap(pcBoxPixel.ClientSize.Width, pcBoxPixel.ClientSize.Height);

            Graphics g = Graphics.FromImage(bmp1);

            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
            
            g.DrawImage(bmp, 0, 0, pcBoxPixel.ClientSize.Width, pcBoxPixel.ClientSize.Height);

            return bmp1;
        }

        #endregion

        #region SetPixelValue

        /*********************************************************************** 
        * Module Name: SetPixelValue
        *
        * Description: This function will update single pixel value in submodule
        *              view.
        * Calling Arguments: 
        *  Name             Input/Output  Type         Description 
        *  pOutputFileName  input         string       File name.
        *  pCHNum           input         uint         Current channel number.
        *
        ***********************************************************************/

        private void SetPixelValue(uint pChannelIndex, ushort pColor, PictureBox pPictureBox, byte[] pArrayImage) {

            short newColor = (short)(pColor);
            
            if (newColor > 256) {
            
                newColor = 256;

            }

            pArrayImage[pChannelIndex] = (byte)newColor;
            
            Bitmap bmp1;

            bmp1 = PictureBoxUpdateImage(pArrayImage);

            pPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            
            pPictureBox.Image = bmp1;

        }

        #endregion

        #region SentFittingNotice

        void SentFittingNotice() {

            //If auto fitting is true
            if (gIsAutoFitting) {
             
                if (!gIsInFittingProcess) {

                    gFittingStartEvent.Set();

                    gIsInFittingProcess = true;

                }

            }
        
        }

        #endregion

        #region Threads

        #region FTDIInQueue

        private void FTDIInQueue() {

            UInt32 myBytesInQueue = 0;

            UInt32 myBytesRead = 0;

            byte mySwitchedAddress = 0;

            FTDI.FT_STATUS myStatus = FTDI.FT_STATUS.FT_OTHER_ERROR;

            byte[] myData = new byte[2];

            int mySleepTime = 50;

            while (true) {

                myStatus = gFTDI.GetRxBytesAvailable(ref  myBytesInQueue);

                if (myStatus == FTDI.FT_STATUS.FT_OK) {

                    while (myBytesInQueue >= 4) {

                        byte[] myQueueBuffer = new byte[4];

                        myStatus = gFTDI.Read(myQueueBuffer, 4, ref myBytesRead);

                        if ((myStatus == FTDI.FT_STATUS.FT_OK) && (myBytesRead >= 4)) {

                            if ((myQueueBuffer[0] != gkMessageTypeCmd) && (myQueueBuffer[0] != gkMessageTypeData)) {

                                //Bad message then quit 
                                //gTotalEventCount = gTotalEventCount;

                            } else if (myQueueBuffer[0] == gkMessageTypeData) {

                                //This is a data message 
                                //Need get the data part and put it in queue

                                myData[0] = myQueueBuffer[2];
                                myData[1] = myQueueBuffer[3];

                                //Temperay version
                                //mySwitchedAddress = (byte)(((myQueueBuffer[1] & 0x77)) | ((myQueueBuffer[1] & 0x80) >> 4) | ((myQueueBuffer[1] & 0x08) << 4));
                                //cRawData myDataPoint = new cRawData(mySwitchedAddress, myData);
                                cRawData myPrecheckDataPoint = new cRawData(myQueueBuffer[1], myData);

                                if ( ((int)gSelectedScanParametersForDataAcq.gSourceType >= (int)cScanParameters.eSourceType.LightShare ) ){

                                    #region Light Share 

                                    gChecksumDataQueue.Enqueue(myPrecheckDataPoint);

                                    gChecksumDataChecksum += myPrecheckDataPoint.mAddress;

                                    if ((gChecksumDataQueue.Count == 4) && ((gChecksumDataChecksum == gkLightShareEvenChecksum) || (gChecksumDataChecksum == gkLightShareOddChecksum))) {

                                        cRawData myCheckFirstData = new cRawData();

                                        if (gChecksumDataQueue.TryDequeue(out myCheckFirstData) ) {
                                            
                                            //This is to avoid 128, 136,0,8 problem
                                            if (myCheckFirstData.mAddress == 0 || myCheckFirstData.mAddress == 1) {

                                                #region First data in four data array is start with 0 or 1

                                                //Because the first data was out for detect start point
                                                gRawDataQueue.Enqueue(myCheckFirstData);

                                                gLightShareRawData.Add( myCheckFirstData );

                                                //Total Event count ++
                                                gTotalEventCount++;

                                                for (int i = 0; i < 3; i++) {

                                                    cRawData myDataPoint = new cRawData();

                                                    gChecksumDataQueue.TryDequeue(out myDataPoint);

                                                    gRawDataQueue.Enqueue(myDataPoint);

                                                    gLightShareRawData.Add( myDataPoint );

                                                    //Total Event count ++
                                                    gTotalEventCount++;

                                                    gChecksumDataChecksum = 0;

                                                }

                                                #endregion

                                            } else {

                                                //Discard the first data
                                                gChecksumDataChecksum -= myCheckFirstData.mAddress;
                                            
                                            }

                                        } 

                                    } else if (gChecksumDataQueue.Count >= 4) {

                                        //Get the bad data from the queue
                                        cRawData myDiscardDataPoint = new cRawData();

                                        gChecksumDataQueue.TryDequeue(out myDiscardDataPoint);

                                        //Get the bad data address from the queue
                                        gChecksumDataChecksum -= myDiscardDataPoint.mAddress;

                                    }

                                    #endregion

                                } else {

                                    //Not light share version
                                    #region Not Light Share 

                                    gRawDataQueue.Enqueue(myPrecheckDataPoint);

                                    //Total Event count ++
                                    gTotalEventCount++;

                                    #endregion

                                }

                            } else if (myQueueBuffer[0] == gkMessageTypeCmd) {

                                #region Command Type

                                myData[0] = myQueueBuffer[2];
                                myData[1] = myQueueBuffer[3];

                                cCommand myCommandResp = new cCommand(myQueueBuffer[0], myQueueBuffer[1], myData);

                                if (myQueueBuffer[1] == gkCmdButtonPushed) {

                                    if (gIsJustGUIStart == false) {

                                        #region Button Message

                                        try {

                                            if (myData[1] == 0) {

                                                //Button push
                                                //Start ACQ
                                                BeginInvoke(((Action)(() => btnStartDataCollection.PerformClick())));

                                            } else if (myData[1] == 1) {

                                                //Button long pressed
                                                //Stop ACQ
                                                StartCollectionWithGUIUpdate(false);

                                            }

                                        } catch {

                                            //TODO: lOG ERROR

                                        }

                                        #endregion

                                    }

                                } else if (myQueueBuffer[1] == gkCmdErrorMessage) {

                                    if (gIsJustGUIStart == false) {

                                        #region Error Message
                                        //This is a error message 
                                        if (gCollectionProcessDone == false && gIsUSBDevicePluggedIn) {

                                            //End stop DAQ
                                            StartCollectionWithGUIUpdate(false);
                                            //StartCollectionWithGUIUpdate(false, false);

                                            //Pop up message box to indicate DAQ is terminated because of the error
                                            if ((myData[1] & gkDarkBoxStatusBitMask) == gkDarkBoxOpen) {

                                                gLastErrorCode = cErrorCode.gkecDataAcq_BoxOpen;
                                                gErrorOutput.OutPutErrorMessage(gLastErrorCode, "");

                                            } else if ((myData[1] & gkADCPLLStatusBitMask) == gkADCPLLError) {

                                                gLastErrorCode = cErrorCode.gkecDataAcq_ADCPLLError;
                                                gErrorOutput.OutPutErrorMessage(gLastErrorCode, "");

                                            }

                                        }

                                        #endregion

                                    }

                                } else {

                                    #region Normal Message

                                    gCommandQueue.Enqueue(myCommandResp);

                                    #endregion

                                }

                                #endregion

                            } else {

                                //Do nothing 
                            
                            }


                        } else {

                            //No correct data
                            myBytesInQueue = 0;
                            break;

                        }

                    }

                }

                Thread.Sleep(mySleepTime);

            }


            //}

        }

        #endregion

        #region FTDIDeQueue

        
        private void FTDIDeQueue() {

            int myDataQueueCnt = 0;

            int myDefaultSleepTimeMs = 20;

            int myCheckTime = 0;

            int myTempBuffer = 0;

            cRawData myDataMessage = new cRawData();

            while (true) {

                FTDIDequeueStart.WaitOne();

                while (true) {

                    myDataQueueCnt = gRawDataQueue.Count;

                    if (myDataQueueCnt >= 10) {

                        if (!gIsInFittingProcess) {

                            #region Grab Data

                            myCheckTime = 0;

                            myDefaultSleepTimeMs = 20;

                            try {

                                BeginInvoke(((Action)(() => myAppendText(rtbLog, gTotalEventCount.ToString(), Color.Green))));

                                BeginInvoke(((Action)(() => txtEventCount.Text = gTotalEventCount.ToString())));

                            } catch (Exception) {

                                //This exception actually don't need do anything
                                //Todo: log this error

                            }

                            if( ( ( int )gSelectedScanParametersForDataAcq.gSourceType >= ( int )cScanParameters.eSourceType.LightShare ) ) {

                                #region Trigger Event match

                                if (gTemperaryTriggerType == (int)cScanParameters.eScanTriggerType.TotalEventCountTrigger) {

                                    #region Total Count Trigger Match

                                    if (gTotalEventCount > gTemperaryTriggerCount) {

                                        //Stop DAQ
                                        StartCollectionWithGUIUpdate(false);

                                        SentFittingNotice();

                                        break;

                                    } else if (gCollectionProcessDone) {

                                        break;

                                    }

                                    #endregion

                                } else if (gTemperaryTriggerType == (int)cScanParameters.eScanTriggerType.SinglePixelEventCountTrigger) {

                                    //TODO: need add percentadge 
                                    #region Single Count Trigger Match

                                    //If check every pixel count reach the trigger count
                                    if (gRecvDataCount[myDataMessage.mAddress] >= gTemperaryTriggerCount) {

                                        gMatchedPixelCount++;

                                    }

                                    if ((1.0 * gMatchedPixelCount / (gSelectedScanParametersForDataAcq.gPixelNumsPerCol * gSelectedScanParametersForDataAcq.gPixelNumsPerRow)) > 0.8) {


                                    }

                                    #endregion

                                }

                                #endregion
                            
                            } else {

                                #region Dequeue Data

                                //Max process amount of data is 1024
                                for( int i = 0; i < myDataQueueCnt; i++ ) {

                                    gRawDataQueue.TryDequeue( out myDataMessage );

                                    gRecvDataCount[myDataMessage.mAddress]++;


                                    //Add this check because don't want to show the leaking light pixel data
                                    if( gRecvDataCount[myDataMessage.mAddress] > gMinimumDisplayEnergyCount ) {

                                        gEngeryCont[myDataMessage.mAddress] = ( byte )( gRecvDataCount[myDataMessage.mAddress] * 252 / gMaxEnergyCount );

                                    }

                                    //Todo: need check satisfied data

                                    //Apply calibation 
                                    //But also check if the protocol implement the correction type

                                    cRawData myDataInfo;

                                    //Apply calibation 
                                    //But also check if the protocol implement the correction type
                                    //if (gSelectedScanParametersForDataAcq.gCorrectionOption == (int)cScanParameters.eCorrectionType.PixelCorrection)
                                    if( true ) {

                                        myTempBuffer = ( int )( myDataMessage.mData * gCalibrationBuffer.mCalibrationBuffer[myDataMessage.mAddress, 0] + gCalibrationBuffer.mCalibrationBuffer[myDataMessage.mAddress, 1] );

                                        myDataInfo = new cRawData( myDataMessage.mAddress, myTempBuffer );

                                    } else {

                                        myDataInfo = new cRawData( myDataMessage.mAddress, myDataMessage.mData );

                                    }

                                    gEventDataInfo[myDataMessage.mAddress].Add( myDataInfo );



                                    //  if ( gSelectedScanParametersForDataAcq.gCorrectionOption == (int)cScanParameters.eCorrectionType.PixelCorrection ) {

                                    //      myDataMessage.mData = (int)( myDataMessage.mData * gCalibrationBuffer.mCalibrationBuffer[myDataMessage.mAddress, 0] + gCalibrationBuffer.mCalibrationBuffer[myDataMessage.mAddress, 1] );
                                    //myDataMessage.mData = myDataMessage.mData * 2;
                                    //   }

                                    //  cRawData myDataInfo = new cRawData(myDataMessage.mAddress, myDataMessage.mData);

                                    //  gEventDataInfo[myDataMessage.mAddress].Add(myDataInfo);

                                    #region Trigger Event match

                                    if( gTemperaryTriggerType == ( int )cScanParameters.eScanTriggerType.TotalEventCountTrigger ) {

                                        #region Total Count Trigger Match

                                        if( gTotalEventCount > gTemperaryTriggerCount ) {

                                            //Stop DAQ
                                            StartCollectionWithGUIUpdate( false );

                                            SentFittingNotice( );

                                            break;

                                        } else if( gCollectionProcessDone ) {

                                            break;

                                        }

                                        #endregion

                                    } else if( gTemperaryTriggerType == ( int )cScanParameters.eScanTriggerType.SinglePixelEventCountTrigger ) {

                                        //TODO: need add percentadge 
                                        #region Single Count Trigger Match

                                        //If check every pixel count reach the trigger count
                                        if( gRecvDataCount[myDataMessage.mAddress] >= gTemperaryTriggerCount ) {

                                            gMatchedPixelCount++;

                                        }

                                        if( ( 1.0 * gMatchedPixelCount / ( gSelectedScanParametersForDataAcq.gPixelNumsPerCol * gSelectedScanParametersForDataAcq.gPixelNumsPerRow ) ) > 0.8 ) {


                                        }

                                        #endregion

                                    }

                                    #endregion

                                }

                                #endregion

                                if( !gCollectionProcessDone ) {

                                    SetPixelValue( 0, ( byte )gEngeryCont[0], pcBoxPixel, gEngeryCont );

                                }
                            
                            }

                            #endregion

                        }

                    } else if (gCollectionProcessDone) {

                        gIsDataReadyToSave = true;

                        break;

                    } else {

                        #region Adjust delay time

                        myCheckTime++;

                        if (myCheckTime >= 10) {

                            myDefaultSleepTimeMs += (int)(0.1F * (float)myCheckTime);

                            myCheckTime = 0;

                        }

                        #endregion

                    }

                    Thread.Sleep(myDefaultSleepTimeMs);

                }

            }

        }

        #endregion

        #region FittingThread

        private void FittingThread() {

            int myRetyTime = 3;

            string myFittingType = "";

            string myCountMapGenerator = "";

            string myHistgramFile = "";

            string myCountMapFile = "";

            string myFittingProcessMessage = "";

            string myExportFileName = "";

            int myPixelPosition = -1;

            int myAddressIndex = 0;

            float myCalibratedSlope = 1.0f;
            float myCalibratedOffset = 0.0f;

            while (true) {

                gFittingStartEvent.WaitOne();

                if (gAutoFittingEnable) {

                    BackGroundWorkDisapear(false);

                    BackGroundWork_RunBackgroundwork("Loading fitting program...");
                    
                    if (gIsInFittingProcess == false) {

                        gIsInFittingProcess = true;
                    
                    }

                    myRetyTime = 3;

                    Fitting.LoadFittingPython( ( int )gSelectedScanParametersForDataAcq.gSourceType, out gFittingPythonFile, out myFittingType, out myCountMapGenerator );

                    //If in local debug mode, use the sample data file so does not need save data file
                    
#if !LOCAL_DEBUG

                    if( ( int )gSelectedScanParametersForDataAcq.gSourceType == ( int )cScanParameters.eSourceType.LightShare196Version ) {
                        gFittingDataFile = gkRawDataDirectory + DateTime.Now.Year.ToString( ) + "_" + DateTime.Now.Month.ToString( ) + "_" +
                                        DateTime.Now.Day.ToString( ) + "_" + DateTime.Now.Hour.ToString( ) + "_" + DateTime.Now.Minute.ToString( ) + "_" +
                                        DateTime.Now.Second.ToString( ) + "_" + myFittingType + ".npz";

                    } else {
                    
                        gFittingDataFile = gkRawDataDirectory + DateTime.Now.Year.ToString( ) + "_" + DateTime.Now.Month.ToString( ) + "_" +
                                        DateTime.Now.Day.ToString( ) + "_" + DateTime.Now.Hour.ToString( ) + "_" + DateTime.Now.Minute.ToString( ) + "_" +
                                        DateTime.Now.Second.ToString( ) + "_" + myFittingType + ".npz";
                    
                    
                    }

#endif

                    BackGroundWork_Complete();

                    if ((gFittingDataFile.Length > 0) && (gFittingPythonFile.Length > 0)) {

                        BackGroundWork_RunBackgroundwork("Saving binary file....");
                        
                        //If in local debug mode, use the sample data file so does not need save data file
#if !LOCAL_DEBUG
                        //If not get all datas and it is not timeout yet
                        while ((gCollectionProcessDone == false) && (myRetyTime > 0 )) {

                            myRetyTime--;

                            Thread.Sleep(1000);
                        
                        }

                        myRetyTime = 3;
                        //Added by YSC, this is to make sure the data count matches the event count
                        while ( (gEventDataInfo.Count() >= gTotalEventCount) && (myRetyTime > 0)) {

                            myRetyTime--;

                            Thread.Sleep(1000);
                        
                        }

                        lock (gEventDataInfo) {

                            SaveFileForFitting(gEventDataInfo, gFittingDataFile);

                        }
#endif
   
                        BackGroundWork_Complete();

                        Thread.Sleep(1000);

                        if( ( ( int )gSelectedScanParametersForDataAcq.gSourceType >= ( int )cScanParameters.eSourceType.LightShare )  ) {

                            #region Light Share

                            BackGroundWork_RunBackgroundwork( "Generating countmap..." );

                            Fitting.LoadFittingPython( ( int )gSelectedScanParametersForDataAcq.gSourceType, out gFittingPythonFile, out myFittingType, out myCountMapGenerator );

                            gFitting.RunCountMap( myCountMapGenerator, gFittingDataFile );

                            while( !gFitting.WaitforComplete( out myFittingProcessMessage ) ) {

                                //BeginInvoke( ( Action )( ( ) => txtPixelAndCount.Text = myFittingProcessMessage ) );
                                //Thread.Sleep( 100 );

                            }

                            gFitting.FillErroCode( );

                            if( gFitting.gErrorCode == cErrorCode.gkecFitting_PixelNoMatch ) {

                                /*
                                //Only when new python code updated, this can be re-openned
                                string myMessage = "";
                                string myTitle = "";

                                if (gSelectedLanguage == (int)Demo.gLanguageVersion.Chinese) {

                                    myMessage = "找到的晶体数量不符，确认继续？";
                                    myTitle = "继续确认";

                                } else if (gSelectedLanguage == (int)Demo.gLanguageVersion.English) {

                                    myMessage =  "The pixel number found is not match, confirm to continue? ";
                                    myTitle = "Confirm Continue";
                    
                                }

                                DialogResult myConfirmDialog = myConfirmDialog = MessageBox.Show( myMessage, myTitle, MessageBoxButtons.YesNo );
                                
                                if( myConfirmDialog == DialogResult.No ) {
                                    
                                    //do something else
                                    BackGroundWork_Complete( );

                                    BackGroundWorkDisapear( true );

                                    continue;

                                }
                                 * */

                                BackGroundWork_Complete( );

                                BackGroundWorkDisapear( true );

                                gErrorOutput.OutPutErrorMessage( gFitting.gErrorCode, "" );

                                continue;


                            }

                            if( gFittingDataFile.EndsWith( ".CSV" ) ) {

                                myCountMapFile = gFittingDataFile.Replace( ".CSV", "_imShow.png" );

                                myHistgramFile = gFittingDataFile.Replace( ".CSV", "_enHist" );

                                //gReportCountMapCopy = gFittingDataFile.Replace( ".CSV", "_imShowReport.png" );
                                gReportCountMapCopy = myCountMapFile;

                                myExportFileName = gFittingDataFile.Replace(".CSV", "_enHist_Calibrated");


                            } else {

                                myCountMapFile = gFittingDataFile.Replace( ".npz", "_imShow.png" );

                                myHistgramFile = gFittingDataFile.Replace( ".npz", "_enHist" );

                                //gReportCountMapCopy = gFittingDataFile.Replace( ".npz", "_imShowReport.png" );
                                gReportCountMapCopy = myCountMapFile;

                                myExportFileName = gFittingDataFile.Replace(".npz", "_enHist_Calibrated");
                            
                            }

                            if( File.Exists( myCountMapFile ) && File.Exists( myHistgramFile ) ) {

                                //Copy for later report use
                                //This was the file beed used, now don't need it anymore
                                /*
                                if( File.Exists( gReportCountMapCopy ) ) {

                                    File.Delete( gReportCountMapCopy );
                                
                                }
                                File.Copy( myCountMapFile, gReportCountMapCopy );
                                */

                                BackGroundWork_RunBackgroundwork( "Process fitting..." );

                                //System.IO.FileStream myFileHandler = new FileStream(myExportFileName, System.IO.FileMode.Create, System.IO.FileAccess.Write);
                                //StreamWriter myStreamWriter = new StreamWriter(myFileHandler, System.Text.Encoding.UTF8);

                                char[] myDelimiters = new char[] { '\r', '\n', ',' };

                                System.IO.FileStream myFileStream = new System.IO.FileStream(myHistgramFile, System.IO.FileMode.Open, System.IO.FileAccess.Read);

                                StreamReader myReader = new StreamReader( myFileStream );

                                string myContent = myReader.ReadToEnd( );

                                string[] mySeperatedData = Regex.Split( myContent, "\n" ); // myContent.Split( "\r\n", StringSplitOptions.RemoveEmptyEntries );

                                int myPixelIndexY = -1;

                                int myPixelIndexX = -1;

                                float myPixelXAxis = 0.0f;
                                float myPixelYAxis = 0.0f;

                                int myLastPixelYPosition = -1;

                                #region Parse All Data

                                for( int i = 0; i < mySeperatedData.Count( ); i++ ) {

                                    string[] myData = mySeperatedData[i].Split( ' ' );

                                    if( myData.Count() >= 2 ) {

                                        int myPixelNo = 0;

                                        float myRawData = 0f;

                                        int myXPosition = ( int )( Convert.ToDouble( myData[0] ) );

                                        int myYPosition = ( int )( Convert.ToDouble( myData[1] ) );

                                        myPixelXAxis = (float)Convert.ToDouble(myData[0]);

                                        myPixelYAxis = (float)Convert.ToDouble(myData[1]);
                                        
                                        if( myLastPixelYPosition != myYPosition ) {

                                            //This is used for later add png report
                                            if( ( !gNewLinePixel.ContainsKey( myPixelIndexY * gPixelPerCol + myPixelIndexX ) ) && ( myPixelIndexX != -1 ) ) {

                                                gNewLinePixel.Add( myPixelIndexY * gPixelPerCol + myPixelIndexX, 0 );

                                            }

                                            if( myPixelIndexX > gMaxPixelPerLine) {

                                                gMaxPixelPerLine = myPixelIndexX;

                                            }

                                            myPixelIndexY++;

                                            myLastPixelYPosition = myYPosition;

                                            myPixelIndexX = 0;

                                        } else {

                                            myPixelIndexX++;
                                        
                                        }
                                        //if (((int)gSelectedScanParametersForDataAcq.gSourceType >= (int)cScanParameters.eSourceType.LightShare196Version)) {
                                        if(gEnablePixelReverse) {
                                            
                                            //Reverse the pixels
                                            myPixelNo = (13 - myPixelIndexY) * gPixelPerCol + myPixelIndexX;

                                        } else {
                                            
                                            myPixelNo = myPixelIndexY * gPixelPerCol + myPixelIndexX;

                                        }
                                        gLightSharePixelNoArray.Enqueue( myPixelNo );

                                        #region Implement Single Pixel Calibration Value

                                        /*
                                        if (gSelectedScanParametersForDataAcq.gSourceType == (int)cScanParameters.eSourceType.LightShareSiglePixel) {

                                            myStreamWriter.Write(myPixelXAxis.ToString("0.00"));
                                            myStreamWriter.Write( " " + myPixelYAxis.ToString("0.00"));

                                        }
                                         */

                                        #endregion
                                        
                                        myPixelPosition = FindPixelIndex(myPixelXAxis, myPixelYAxis);

                                        if (myPixelPosition >= 0) {

                                            gPixelNumToPositionMap[myPixelNo] = myPixelPosition;

                                            myCalibratedSlope = gSinglePixelCalibrationBuffer.mCalibrationBuffer[myPixelPosition, 0];

                                            myCalibratedOffset = gSinglePixelCalibrationBuffer.mCalibrationBuffer[myPixelPosition, 1];

                                        } else {

                                            myCalibratedSlope = 1.0f;
                                            myCalibratedOffset = 0.0f;
                                        
                                        }

                                        #region Parse the data

                                        myAddressIndex = 0;

                                        for (int j = 2; j < myData.Count(); j++) {

                                            if( float.TryParse( myData[j], out myRawData ) ) {

                                                cRawData myRawDataSet = null;

                                                #region Write back with calibrated value 

                                                if (gSelectedScanParametersForDataAcq.gSourceType == (int)cScanParameters.eSourceType.LightShareSiglePixel) {

                                                   
                                                    if (myPixelPosition >= 0) {

                                                        //myStreamWriter.Write(" " + (myRawData * 1 + 0).ToString("0.00"));
                                                        /*
                                                        myRawDataSet = new cRawData((byte)j, 
                                                            (int)(myRawData * gSinglePixelCalibrationBuffer.mCalibrationBuffer[myPixelPosition, 0] +
                                                            gSinglePixelCalibrationBuffer.mCalibrationBuffer[myPixelPosition, 1]));
                                                        */
                                                        if ((myAddressIndex * myCalibratedSlope + myCalibratedOffset) <= 255) {
                                                         
                                                            myRawDataSet = new cRawData((byte)(myAddressIndex * myCalibratedSlope + myCalibratedOffset), (int)(myRawData));
                                                        
                                                        }


                                                    }

                                                } else {

                                                    myRawDataSet = new cRawData((byte)myAddressIndex, (int)myRawData);
                                                
                                                }

                                                #endregion

                                                if (myRawDataSet != null) {
                                                 
                                                    gEventDataInfo[myPixelNo].Add(myRawDataSet);

                                                }

                                                myAddressIndex++;

                                            }

                                        }
                                        #endregion

                                        //myStreamWriter.Write("\n");

                                    }

                                }

                                #endregion

                                myReader.Close();
                                myFileStream.Close();

                                //myStreamWriter.Close();
                                //myFileHandler.Close();

                                /*
                                if (gSelectedScanParametersForDataAcq.gSourceType == (int)cScanParameters.eSourceType.LightShareSiglePixel) {

                                    //File.Replace(myExportFileName, myHistgramFile, myHistgramFile+"Backup");
                                    File.Delete(myHistgramFile);

                                    myHistgramFile = myExportFileName;

                                }
                                 */

                                gFitting.RunPython(gFittingPythonFile, myHistgramFile);

                                //Sperate the RunPython and wait for exit in order to get the status message 
                                while (!gFitting.WaitforComplete(out myFittingProcessMessage)) {

                                    //BeginInvoke( ( Action )( ( ) => label5.Text = myFittingProcessMessage ) );
                                    //Thread.Sleep( 200 );

                                }

                                gFitting.FillFittingResult( );

                            } else {

                                BackGroundWork_Complete( );

                                BackGroundWorkDisapear( true );

                                gErrorOutput.OutPutErrorMessage( cErrorCode.gkecFitting_NoCountmap, "" );

                                continue;


                            }

                            #endregion

                        } else {


                            #region Not Light Share

                            //Non-lightshare just fitting

                            BackGroundWork_RunBackgroundwork( "Process fitting..." );

                            Fitting.LoadFittingPython( ( int )gSelectedScanParametersForDataAcq.gSourceType, out gFittingPythonFile, out myFittingType, out myCountMapGenerator );

                            gFitting.RunPython( gFittingPythonFile, gFittingDataFile );

                            //Sperate the RunPython and wait for exit in order to get the status message 
                            while( !gFitting.WaitforComplete( out myFittingProcessMessage ) ) {

                                //BeginInvoke( ( Action )( ( ) => txtPixelAndCount.Text = myFittingProcessMessage ) ); 

                            }

                            gFitting.FillFittingResult( );

                            #endregion

                        }
                         
                        
                        gIsFittingDone = true;

                        BackGroundWork_Complete();

                        BeginInvoke(((Action)(() => btnGenerateReport.Visible = true )));

                        BeginInvoke(((Action)(() => grpReportNote.Visible = true ))); 

                        BackGroundWorkDisapear(true);

                        if( ( ( int )gSelectedScanParametersForDataAcq.gSourceType >= ( int )cScanParameters.eSourceType.LightShare ) ) {

                            #region Light Share Display

                            BeginInvoke( ( ( Action )( ( ) => picLightDivideDisplay.Visible = true ) ) );

                            if( gFittingDataFile.EndsWith( ".CSV" ) ) {

                                picLightDivideDisplay.ImageLocation = gFittingDataFile.Replace( ".CSV", "_imShow.png" );

                            } else {

                                picLightDivideDisplay.ImageLocation = gFittingDataFile.Replace( ".npz", "_imShow.png" );

                            }
                            picLightDivideDisplay.SizeMode = PictureBoxSizeMode.StretchImage;
                            gDisplayTracker = 0;
                            gSwitchDisplay = true;

                            #endregion

                        } else {

                            #region Not Light Share Display

                            gDisplayTracker = 1;
                            gSwitchDisplay = false;
                            BeginInvoke( ( ( Action )( ( ) => picLightDivideDisplay.Visible = false ) ) );
                            SetResultOnPixelMap( );

                            #endregion

                        }

                        #region Get Count After Fitting

                        int myCountCnt = 0;

                        for( int myPixelNo = 0; myPixelNo < 256; myPixelNo++ ) {

                            myCountCnt = 0;

                            if( ( ( int )gSelectedScanParametersForDataAcq.gSourceType >= ( int )cScanParameters.eSourceType.LightShare ) ) {

                                foreach( cRawData myCount in gEventDataInfo[myPixelNo] ) {

                                    myCountCnt += myCount.mData;

                                }


                            } else {

                                myCountCnt = gEventDataInfo[myPixelNo].Count( );

                            }

                            if( myCountCnt > gMinimumDisplayEnergyCount ) {

                                gFitting.gCountCnt[myPixelNo] = myCountCnt;

                            }

                        }
                        #endregion


                    }


                }

            } //While loop
        
        }

        #endregion

        #endregion

        #region SaveFileForFitting

        int SaveFileForFitting(List<cRawData>[] pEventData, string pExportFileName) {

            int myStatus = 0;

            int myTotalPixelCount = 0;  //pEventData.Count();

            try {

                if( ( ( int )gSelectedScanParametersForDataAcq.gSourceType >= ( int )cScanParameters.eSourceType.LightShare ) ) {

                    #region Light Share

                    if( pExportFileName.ToLower( ).Contains( ".csv" ) ) {

                        #region Save As CSV File

                        string myTimestamp = "";
                        myTimestamp = DateTime.Now.ToString( ); ;
                        myTimestamp = myTimestamp.Replace( ',', ' ' );

                        try {

                            System.IO.FileStream myFileHandler = new FileStream( pExportFileName, System.IO.FileMode.Create, System.IO.FileAccess.Write );
                            StreamWriter myStreamWriter = new StreamWriter( myFileHandler, System.Text.Encoding.UTF8 );

                            #region Add Header Section

                            //Write Header
                            myStreamWriter.WriteLine( "Header0:" + "Address,Energy" );
                            myStreamWriter.WriteLine( "Header1:Timestamp: " + myTimestamp );

                            for( int myLineNumber = 2; myLineNumber < 16; myLineNumber++ ) {

                                myStreamWriter.WriteLine( "Header" + myLineNumber.ToString( ) );

                            }

                            #endregion

                            int myDatCount = gRawDataQueue.Count;

                            cRawData myRawData = new cRawData( );

                            for( int i = 0; i < myDatCount; i++ ) {

                                if( gRawDataQueue.TryDequeue( out myRawData ) ) {

                                    myStreamWriter.WriteLine( myRawData.mAddress.ToString( ) + "," + myRawData.mData.ToString( ) );

                                }

                            }

                            myStreamWriter.Close( );
                            myFileHandler.Close( );



                        } catch( IOException ) {

                            gErrorOutput.OutPutErrorMessage( cErrorCode.gkecFileSubError_FileUsedByOthers, "" );

                        } catch( Exception pException ) {

                            gErrorOutput.OutPutErrorMessage( 0, pException.Message );

                        }

                        #endregion

                    } else if( pExportFileName.ToLower( ).Contains( ".npz" ) ) {

                        #region Save AS NPZ file

                        System.IO.FileStream myBinFileHandler = new FileStream( pExportFileName, System.IO.FileMode.CreateNew, System.IO.FileAccess.Write );
                        BinaryWriter myBinStreamWrite = new BinaryWriter( myBinFileHandler );

                        #region Add Header Section

                        myBinStreamWrite.Write( ( UInt32 )gSelectedScanParametersForDataAcq.gSourceType );

                        for( int myLineNumber = 1; myLineNumber < 16; myLineNumber++ ) {

                            myBinStreamWrite.Write( ( UInt32 )myLineNumber );

                        }

                        #endregion

                        int myDatCount = gRawDataQueue.Count;

                        cRawData myRawData = new cRawData( );

                        for( int i = 0; i < myDatCount; i++ ) {

                            if( gRawDataQueue.TryDequeue( out myRawData ) ) {

                                myBinStreamWrite.Write( ( ushort )myRawData.mAddress );

                                myBinStreamWrite.Write( ( ushort )myRawData.mData );

                            }

                        }

                        myBinFileHandler.Close( );
                        myBinStreamWrite.Close( );

                        #endregion

                    }

                    #endregion

                } else {

                    #region Not Light Share

                    #region Save NPZ file

                    System.IO.FileStream myBinFileHandler = new FileStream( pExportFileName, System.IO.FileMode.CreateNew, System.IO.FileAccess.Write );
                    BinaryWriter myBinStreamWrite = new BinaryWriter( myBinFileHandler );

                    #region Add Header Section

                    myBinStreamWrite.Write( ( UInt32 )gSelectedScanParametersForDataAcq.gSourceType );

                    for( int myLineNumber = 1; myLineNumber < 16; myLineNumber++ ) {

                        myBinStreamWrite.Write( ( UInt32 )myLineNumber );

                    }

                    #endregion

                    myTotalPixelCount = gEventDataInfo.Count( );

                    for( int i = 0; i < myTotalPixelCount; i++ ) {

                        if( gEngeryCont[i] > gMinimumDisplayEnergyCount ) {

                            foreach( cRawData myRawData in gEventDataInfo[i] ) {

                                myBinStreamWrite.Write( ( ushort )myRawData.mAddress );

                                myBinStreamWrite.Write( ( ushort )myRawData.mData );

                            }

                        }


                    }

                    myBinFileHandler.Close( );
                    myBinStreamWrite.Close( );

                    #endregion

                    #endregion               
                
                }


            } catch(Exception pException){

                MessageBox.Show("When saving binary " + pException.Message);

                myStatus = -1;
                
            }

            return myStatus;

        }

        #endregion

        #region SetResultOnPixelMap

        void SetResultOnPixelMap() {

            float myQualityTypeValue = 0.0f;

            float myQualifiedMinValue = 0.0f;
            float myQualifiedMaxValue = 16384.0f;
            cEnergyCountQualified myEnergyCountQualified = new cEnergyCountQualified ( );
            cEnergyResolutionQualified myEnergyResolutionQualified = new cEnergyResolutionQualified ( );

            foreach (KeyValuePair<int, cFittingParameters> myFittingRersult in gFitting.gDictFittingParameters) {

                if (myFittingRersult.Value.mStatus == (int)cFittingParameters.eStatus.OK) {

                    if (gSelectedQualifiedType == 0) {

                        myQualityTypeValue = myFittingRersult.Value.mG2Center;

                    } else if (gSelectedQualifiedType == 1) {

                        myQualityTypeValue = myFittingRersult.Value.mEnergyResolution;
                    
                    }

                    #region Check Qualified Range

                    if (gIsUseDifferentRangesForPixels) {

                        if (gSelectedQualifiedType == 0) {

                            #region Use Energy Peak

                            if (gEnergyCountQualifiedLevel.TryGetValue ( myFittingRersult.Key, out myEnergyCountQualified )) {

                                myQualifiedMinValue = myEnergyCountQualified.mEnergyCountMin;
                                myQualifiedMaxValue = myEnergyCountQualified.mEnergyCountMax;

                            } else {

                                myQualifiedMinValue = gPeakLowLimit;
                                myQualifiedMaxValue = gPeakHighLimit;

                            }

                            #endregion

                        } else if (gSelectedQualifiedType == 1) {

                            #region Use Energy Resolution

                            if (gEnergyResolutionQualifiedLevel.TryGetValue ( myFittingRersult.Key, out myEnergyResolutionQualified )) {

                                myQualifiedMinValue = myEnergyResolutionQualified.mEnergyResolutionMin;
                                myQualifiedMaxValue = myEnergyResolutionQualified.mEnergyResolutionMax;

                            } else {

                                myQualifiedMinValue = gPeakLowLimit;
                                myQualifiedMaxValue = gPeakHighLimit;

                            }

                            #endregion

                        }



                    } else {

                        myQualifiedMinValue = gPeakLowLimit;
                        myQualifiedMaxValue = gPeakHighLimit;

                    }

                    #endregion


                    if ((myQualityTypeValue >= myQualifiedMinValue) && ((myQualityTypeValue <= myQualifiedMaxValue))) {

                        //255 is green
                        gEngeryCont[myFittingRersult.Key] = 255;

                    } else if (myQualityTypeValue < myQualifiedMinValue) {

                        //254 is red
                        if (gSelectedQualifiedType == 0) {
                         
                            gEngeryCont[myFittingRersult.Key] = 254;

                        } else if (gSelectedQualifiedType == 1) {

                            gEngeryCont[myFittingRersult.Key] = 253;
                        
                        }

                    } else if (myQualityTypeValue > myQualifiedMaxValue) {

                        //253 is yellow
                        if (gSelectedQualifiedType == 0) {

                            gEngeryCont[myFittingRersult.Key] = 253;

                        } else {

                            gEngeryCont[myFittingRersult.Key] = 254;

                        }
                    }

                }

            }

            SetPixelValue(0, (byte)gEngeryCont[0], pcBoxPixel, gEngeryCont);

        }

        #endregion

        #region LoadConfigurationParameter

        int LoadConfigurationParameter() {

            int myStatus = 0;

            int myResultValue = 0;

            myStatus = LoadConfigurationFile ( gkConfigFilesPath );

            if (myStatus == 0) {

                #region Get Default Language 

                myStatus = GetStringValue(gkDefaultLanguageParmName, out gDefaultLanguage);

                if (myStatus != 0) {

                    gDefaultLanguage = cFactoryConfigParameter.gkLanguageEnglish.ToString();

                    gSelectedLanguage = (int)gLanguageVersion.English;

                } else {

                    if (gDefaultLanguage == cFactoryConfigParameter.gkLanguageChinese) {

                        gSelectedLanguage = (int)gLanguageVersion.Chinese;

                    } else if (gDefaultLanguage == cFactoryConfigParameter.gkLanguageEnglish) {

                        gSelectedLanguage = (int)gLanguageVersion.English;

                    }


                }

                #endregion

                #region Get Max Energy Count Per Pixel

                myStatus = GetIntValue(cFactoryConfigParameter.gkMaxEnergyCountPerPixel, out gMaxEnergyCount);

                if (myStatus != 0) {

                    gMaxEnergyCount = 65536;

                }

                #endregion

                #region Get Default Bin Size

                myStatus = GetIntValue(cFactoryConfigParameter.gkBinSize, out gBinSize);

                if (myStatus != 0) {

                    gBinSize = 100;

                }

                #endregion

                #region Get Default Trigger Count 

                myStatus = GetIntValue("DefaultTriggerCount", out myResultValue);

                if (myStatus != 0) {

                    gTriggerEventCount = 1000000;

                } else {

                    gTriggerEventCount = (uint)myResultValue;

                }

                #endregion

                #region Get Default If Include Resolution 

                string myTemp = "";

                myStatus = GetStringValue( cFactoryConfigParameter.gkIfIncludeResolutionInReport, out myTemp );

                if( myStatus == 0 ) {

                    if( myTemp != null ) {

                        myTemp = myTemp.ToLower( );

                        if( myTemp == "yes" ) {

                            gIsIncludeResolutionInReport = true;

                        } else if( myTemp == "no" ) {

                            gIsIncludeResolutionInReport = false;

                        }
                    
                    } else {

                        gIsIncludeResolutionInReport = true;

                    }

                } else {

                    gIsIncludeResolutionInReport = true;

                }

                #endregion

                #region Get Default If Include Energy Spectrum

                myStatus = GetStringValue( cFactoryConfigParameter.gkIfIncludeEnergySpectrumInReport, out myTemp );

                if( myStatus == 0 ) {

                    if( myTemp != null ) {

                        myTemp = myTemp.ToLower( );

                        if( myTemp == "yes" ) {

                            gIsIncludeEnergySpectrumInReport = true;

                        } else if( myTemp == "no" ) {

                            gIsIncludeEnergySpectrumInReport = false;

                        }

                    } else {

                        gIsIncludeEnergySpectrumInReport = false;

                    }

                } else {

                    gIsIncludeEnergySpectrumInReport = false;

                }

                #endregion

                #region Get Default If Include Energy Count

                myStatus = GetStringValue( cFactoryConfigParameter.gkIfIncludeCountInReport, out myTemp );

                if( myStatus == 0 ) {

                    if( myTemp != null ) {

                        myTemp = myTemp.ToLower( );

                        if( myTemp == "yes" ) {

                            gIsIncludeEnergyCountInReport = true;

                        } else if( myTemp == "no" ) {

                            gIsIncludeEnergyCountInReport = false;

                        }

                    } else {

                        gIsIncludeEnergyCountInReport = false;

                    }

                } else {

                    gIsIncludeEnergyCountInReport = false;

                }

                #endregion

                #region Get Default If IncludeCountGreyPic

                myStatus = GetStringValue(cFactoryConfigParameter.gkIncludeCountGreyPic, out myTemp);

                if (myStatus == 0) {

                    if (myTemp != null) {

                        myTemp = myTemp.ToLower();

                        if (myTemp == "yes") {

                            gIsIncludeCountGreyPic = true;

                        } else if (myTemp == "no") {

                            gIsIncludeCountGreyPic = false;

                        }

                    } else {

                        gIsIncludeCountGreyPic = false;

                    }

                } else {

                    gIsIncludeCountGreyPic = false;

                }

                #endregion

                #region Get Default If IncludeEnergyGreyPic

                myStatus = GetStringValue(cFactoryConfigParameter.gkIncludeEnergyGreyPic, out myTemp);

                if (myStatus == 0) {

                    if (myTemp != null) {

                        myTemp = myTemp.ToLower();

                        if (myTemp == "yes") {

                            gIsIncludeEnergyGreyPic = true;

                        } else if (myTemp == "no") {

                            gIsIncludeEnergyGreyPic = false;

                        }

                    } else {

                        gIsIncludeEnergyGreyPic = false;

                    }

                } else {

                    gIsIncludeEnergyGreyPic = false;

                }

                #endregion

                #region Get Default If IncludeResolutionGreyPic

                myStatus = GetStringValue(cFactoryConfigParameter.gkIncludeResolutionGreyPic, out myTemp);

                if (myStatus == 0) {

                    if (myTemp != null) {

                        myTemp = myTemp.ToLower();

                        if (myTemp == "yes") {

                            gIsIncludeResolutionGreyPic = true;

                        } else if (myTemp == "no") {

                            gIsIncludeResolutionGreyPic = false;

                        }

                    } else {

                        gIsIncludeResolutionGreyPic = false;

                    }

                } else {

                    gIsIncludeResolutionGreyPic = false;

                }

                #endregion

                #region Get Default If Auto Adjust Vbias

                myStatus = GetStringValue( cFactoryConfigParameter.gkIfAutoAdjustAbias, out myTemp );

                if( myStatus == 0 ) {

                    if( myTemp != null ) {

                        myTemp = myTemp.ToLower( );

                        if( myTemp == "yes" ) {

                            gIsAutoAdjustVbias = true;

                        } else if( myTemp == "no" ) {

                            gIsAutoAdjustVbias = false;

                        }

                    } else {

                        gIsAutoAdjustVbias = false;

                    }

                } else {

                    gIsAutoAdjustVbias = false;

                }

                #endregion

                #region Get Default If Use Different Ranges For Pixels

                myStatus = GetStringValue ( cFactoryConfigParameter.gkUseDifferentRangesForPixels, out myTemp );

                if (myStatus == 0) {

                    if (myTemp != null) {

                        myTemp = myTemp.ToLower ( );

                        if (myTemp == "yes") {

                            gIsUseDifferentRangesForPixels = true;

                        } else if (myTemp == "no") {

                            gIsUseDifferentRangesForPixels = false;

                        }

                    } else {

                        gIsUseDifferentRangesForPixels = false;

                    }

                } else {

                    gIsUseDifferentRangesForPixels = false;

                }

                #endregion

                #region Get Report Logo File

                myStatus = GetStringValue(cFactoryConfigParameter.gkReportLogoPath, out myTemp);

                if (myStatus == 0) {

                    if (myTemp != null) {

                        gReportLogo = myTemp;

                    } else {

                        //Just use default one

                    }

                } else {

                    //Just use default one

                }

                #endregion

                #region Get Report Header ENG

                myStatus = GetStringValue(cFactoryConfigParameter.gkReportHeaderENG, out myTemp);

                if (myStatus == 0) {

                    if (myTemp != null) {

                        gReportHeaderENG = myTemp;

                    } else {

                        //Just use default one

                    }

                } else {

                    //Just use default one

                }

                #endregion

                #region Get Report Header CHA

                myStatus = GetStringValue(cFactoryConfigParameter.gkReportHeaderCHA, out myTemp);

                if (myStatus == 0) {

                    if (myTemp != null) {

                        gReportHeaderCHA = myTemp;

                    } else {

                        //Just use default one

                    }

                } else {

                    //Just use default one

                }

                #endregion

                #region Get Application Name

                myStatus = GetStringValue ( cFactoryConfigParameter.gkApplicationName, out myTemp );

                if (myStatus == 0) {

                    if (myTemp != null) {

                        gApplicationName = myTemp;

                    } else {

                        //Just use default one

                    }

                } else {

                    //Just use default one

                }

                #endregion

                #region Get Link Button Name

                myStatus = GetStringValue ( cFactoryConfigParameter.gkLinkButtonName, out myTemp );

                if (myStatus == 0) {

                    if (myTemp != null) {

                        gLinkButtonName = myTemp;

                    } else {

                        //Just use default one

                    }

                } else {

                    //Just use default one

                }

                #endregion

                #region Get Link Web Page

                myStatus = GetStringValue ( cFactoryConfigParameter.gkLinkWebpage, out myTemp );

                if (myStatus == 0) {

                    if (myTemp != null) {

                        gLinkWebpage = myTemp;

                    } else {

                        //Just use default one

                    }

                } else {

                    //Just use default one

                }

                #endregion

                #region Get Pixel Reverse Setting

                myStatus = GetStringValue(cFactoryConfigParameter.gkEnablePixelReverse, out myTemp);

                if (myStatus == 0) {

                    if (myTemp != null) {

                        myTemp = myTemp.ToUpper();

                        if (myTemp == "YES") {

                            gEnablePixelReverse = true;

                        } else {

                            gEnablePixelReverse = false;

                        }

                    } else {

                        //Just use default one

                    }

                } else {

                    //Just use default one

                }

                #endregion

                #region Get Qualified Type

                myStatus = GetIntValue(cFactoryConfigParameter.gkDefaultQualifiedType, out myResultValue);

                if (myStatus == 0) {

                    gSelectedQualifiedType = myResultValue;

                } else {

                    gSelectedQualifiedType = 0;

                }

                #endregion

                #region Get QualifiedPeakRangeFile

                myStatus = GetStringValue(cFactoryConfigParameter.gkQualifiedPeakRangeFile, out myTemp);

                if (myStatus == 0) {

                    if (myTemp != null) {

                        gQualifiedCountFileName = myTemp;

                    } else {

                        //Just use default one
                        gQualifiedCountFileName = cProgramDirectory.gkQualifiedCountFileName;

                    }

                } else {

                    //Just use default one
                    gQualifiedCountFileName = cProgramDirectory.gkQualifiedCountFileName;

                }

                #endregion

                #region Get QualifiedResolutionFileName

                myStatus = GetStringValue(cFactoryConfigParameter.gkQualifiedResolutionRangeFile, out myTemp);

                if (myStatus == 0) {

                    if (myTemp != null) {

                        gQualifiedResolutionFileName = myTemp;

                    } else {

                        //Just use default one
                        gQualifiedResolutionFileName = cProgramDirectory.gkQualifiedResolutionFileName;

                    }

                } else {

                    //Just use default one
                    gQualifiedResolutionFileName = cProgramDirectory.gkQualifiedResolutionFileName;

                }

                #endregion

            }

            //MessageBox.Show ( gMaxEnergyCount.ToString ( ) );

            return myStatus;
        
        }

        int LoadConfigurationFile(string pConfigFile) {

            int myStatus = 0;

            XmlDocument myDocument = new XmlDocument ( );

            try {

                using (Stream myStream = File.OpenRead ( pConfigFile )) {

                    myDocument.Load ( myStream );

                    gOriginConfigDocument = myDocument;

                }


                XmlNodeList myPnodes = myDocument.SelectNodes ( "/ConfigParamContainer/ParameterList/ConfigParamPair" );

                foreach (XmlNode myNode in myPnodes) {

                    string myKey = myNode.ChildNodes.Item ( 0 ).InnerText;

                    string myValue = myNode.ChildNodes.Item ( 1 ).InnerText;

                    try {

                        gConfigurationDict.Add ( myKey, myValue );


                    } catch (Exception) {

                        //Todo: log this error
                    
                    }

                
                
                }


            } catch (Exception) {

                //Todo: log this error
                MessageBox.Show ( "Can't load configuration file, use default value (无法加载配置文件，将用默认值)" );
            
            
            }




            return myStatus;
        
        }

        int LoadProtocolFile(string pProtocolFile, string pProtocol) {


            int myStatus = 0;

            Dictionary<string, string> myProtocol = new Dictionary<string, string>();

            XmlDocument myDocument = new XmlDocument();

            try {

                using (Stream myStream = File.OpenRead(pProtocolFile)) {

                    myDocument.Load(myStream);

                }


                XmlNodeList myPnodes = myDocument.SelectNodes("/ScanParamContainer/ParameterList/ScanParamPair");

                foreach (XmlNode myNode in myPnodes) {

                    string myKey = myNode.ChildNodes.Item(0).InnerText;

                    string myValue = myNode.ChildNodes.Item(1).InnerText;

                    try {

                        myProtocol.Add(myKey, myValue);


                    } catch (Exception) {

                        //Todo: log this error

                    }



                }

                gProtocolsDict.Add(pProtocol, myProtocol);


            } catch (Exception) {

                //Todo: log this error
                MessageBox.Show("Can't load configuration file, use default value (无法加载配置文件，将用默认值)");

            }

            return myStatus;      
        
        }

        

        public int LoadSinglePixelCalibrationFile( string pFileName ) {

            int myStatus = 0;

            int myFilledPixelIndex = 0;

            int myReadPixelIndex = 0;

            float myReadSlopeValue = 1f;

            float myReadOffsetValue = 0f;

            int myReadLineIndex = 0;

            //First reset all calibration values
            gSinglePixelCalibrationBuffer.mInitializeCalibrationBuffer();

            if ( File.Exists ( pFileName ) ) {

                System.IO.FileStream myCalibrationFileHandler = new FileStream ( pFileName, System.IO.FileMode.Open, System.IO.FileAccess.Read );
                StreamReader myStreamReader = new StreamReader ( myCalibrationFileHandler );

                string myLineFromCalibrationFile = "";

                do {

                    myLineFromCalibrationFile = myStreamReader.ReadLine ( );

                    if ( myReadLineIndex < 2 ) {

                        //First line is used to save the calibration information such as 
                        if ( myReadLineIndex == 0 ) {

                            gSinglePixelCalibrationBuffer.mId = myLineFromCalibrationFile;

                        } else { 
                        
                            //It is the header filed, do need do anything now
                        
                        }

                    } else {

                        if ( ( myLineFromCalibrationFile != null ) && ( myLineFromCalibrationFile.Length > 0 ) ) {

                            string[ ] myFileds = myLineFromCalibrationFile.Split ( ',' );

                            if ( myFileds.Count ( ) == 3 ) {

                                //Yes, it has all filed
                                if ( int.TryParse ( myFileds[0], out myReadPixelIndex ) ) {

                                    if ( !float.TryParse ( myFileds[1], out myReadSlopeValue ) ) {

                                        myReadSlopeValue = 1f;

                                    }

                                    if ( !float.TryParse ( myFileds[2], out myReadOffsetValue ) ) {

                                        myReadOffsetValue = 0f;

                                    }

                                    gSinglePixelCalibrationBuffer.mCalibrationBuffer[myReadPixelIndex, 0] = myReadSlopeValue;
                                    gSinglePixelCalibrationBuffer.mCalibrationBuffer[myReadPixelIndex, 1] = myReadOffsetValue;

                                } else {

                                    //It will use the default value slope = 1, offset = 0

                                }

                            } else {

                                //No, then use the default 

                            }



                        }//

                    }

                    myReadLineIndex++;

                } while ( myLineFromCalibrationFile != null );

            }

            return myStatus;

        }

        public int FindPixelIndex(float pXAxis, float pYAxis) {

            int myPixelIndex = -1;

            //foreachgSinglePxielPositionMap

            foreach (KeyValuePair<int, cPixelPosition > myPositionMap in gSinglePxielPositionMap) {

                if ((Math.Abs(myPositionMap.Value.mXAxis - pXAxis) <= myPositionMap.Value.mOffsetRange) &&
                    (Math.Abs(myPositionMap.Value.mYAxis - pYAxis) <= myPositionMap.Value.mOffsetRange)) {

                        myPixelIndex = myPositionMap.Key;

                        break;
                
                }
            
            
            }


            return myPixelIndex;
        
        }

        public int LoadCalibrationFile( string pFileName ) {

            int myStatus = 0;

            int myFilledPixelIndex = 0;

            int myReadPixelIndex = 0;

            float myReadSlopeValue = 1f;

            float myReadOffsetValue = 0f;

            int myReadLineIndex = 0;

            //First reset all calibration values
            gCalibrationBuffer.mInitializeCalibrationBuffer ( );

            if ( File.Exists ( pFileName ) ) {

                System.IO.FileStream myCalibrationFileHandler = new FileStream ( pFileName, System.IO.FileMode.Open, System.IO.FileAccess.Read );
                StreamReader myStreamReader = new StreamReader ( myCalibrationFileHandler );

                string myLineFromCalibrationFile = "";

                do {

                    myLineFromCalibrationFile = myStreamReader.ReadLine ( );

                    if ( myReadLineIndex < 2 ) {

                        //First line is used to save the calibration information such as 
                        if ( myReadLineIndex == 0 ) {

                            gCalibrationBuffer.mId = myLineFromCalibrationFile;

                        } else { 
                        
                            //It is the header filed, do need do anything now
                        
                        }

                    } else {

                        if ( ( myLineFromCalibrationFile != null ) && ( myLineFromCalibrationFile.Length > 0 ) ) {

                            string[ ] myFileds = myLineFromCalibrationFile.Split ( ',' );

                            if ( myFileds.Count ( ) == 3 ) {

                                //Yes, it has all filed
                                if ( int.TryParse ( myFileds[0], out myReadPixelIndex ) ) {

                                    if ( !float.TryParse ( myFileds[1], out myReadSlopeValue ) ) {

                                        myReadSlopeValue = 1f;

                                    }

                                    if ( !float.TryParse ( myFileds[2], out myReadOffsetValue ) ) {

                                        myReadOffsetValue = 0f;

                                    }

                                    gCalibrationBuffer.mCalibrationBuffer[myReadPixelIndex, 0] = myReadSlopeValue;
                                    gCalibrationBuffer.mCalibrationBuffer[myReadPixelIndex, 1] = myReadOffsetValue;

                                } else {

                                    //It will use the default value slope = 1, offset = 0

                                }

                            } else {

                                //No, then use the default 

                            }



                        }//

                    }

                    myReadLineIndex++;

                } while ( myLineFromCalibrationFile != null );

            }

            return myStatus;

        }

        int GetStringValue(string pKey, out string pValue) {

            int myStatus = 0;

            pValue = "";

            if (gConfigurationDict.TryGetValue ( pKey, out pValue )) {

                myStatus = 0;

            } else {

                myStatus = -1;
            
            }

            return myStatus;
        
        }

        int GetIntValue(string pKey, out int pValue) {

            int myStatus = 0;

            pValue = 0;

            string myValue = "";

            if (gConfigurationDict.TryGetValue ( pKey, out myValue )) {

                int myIntValue = 0;

                if (int.TryParse ( myValue, out myIntValue )) {

                    pValue = myIntValue;


                } else {

                    myStatus = -2;


                }

            } else {


                myStatus = -1;
            
            }


            return myStatus;
        
        }

        int GetFloatValue(string pKey, out float pValue) {

            int myStatus = 0;

            pValue = (float)0.0;

            string myValue = "";

            if (gConfigurationDict.TryGetValue ( pKey, out myValue )) {

                float myFloatValue = (float)0.0;

                if (float.TryParse ( myValue, out myFloatValue )) {

                    pValue = myFloatValue;


                } else {

                    myStatus = -2;


                }

            } else {


                myStatus = -1;

            }

            return myStatus;

        }

        public int AddorModifyConfigParameter( string pKey, string pValue ) {

            int myStatus = 0;

            //gkXmlConfigNodePath
            string myNodePath = gkXmlConfigNodePath + "[key='" + pKey + "']";

            XmlNode myModifiedNode = gOriginConfigDocument.SelectSingleNode( myNodePath );

            //Becareful here, pValue can not be empty otherwise it will mess up the
            //xml file format
            if( myModifiedNode != null ) {

                myModifiedNode["value"].InnerText = pValue;

            } else {

                AddConfigParameter( pKey, pValue );
            
            }

            return myStatus;
        
        }

        public int ModifyConfigParameter(string pKey, string pValue) {

            int myStatus = 0;

            //gkXmlConfigNodePath
            string myNodePath = gkXmlConfigNodePath + "[key='" + pKey + "']";

            XmlNode myModifiedNode = gOriginConfigDocument.SelectSingleNode(myNodePath);

            //Becareful here, pValue can not be empty otherwise it will mess up the
            //xml file format
            if (myModifiedNode != null) {

                myModifiedNode["value"].InnerText = pValue;

            }

            return myStatus;

        }

        public int AddConfigParameter( string pKey, string pValue ) {

            int myStatus = 0;

            //gkXmlConfigNodePath
            string myNodePath = gkXmlConfigAddNodePath;

            XmlNode myAddedNode = gOriginConfigDocument.SelectSingleNode( myNodePath );

            XmlElement myContainerNode = gOriginConfigDocument.CreateElement( "ConfigParamPair" );
            XmlElement myKeyNode = gOriginConfigDocument.CreateElement( "key" );
            XmlElement myValueNode = gOriginConfigDocument.CreateElement( "value" );

            myKeyNode.InnerText = pKey;

            myValueNode.InnerText = pValue;

            myContainerNode.AppendChild( myKeyNode );
            myContainerNode.AppendChild( myValueNode );

            myAddedNode.AppendChild( myContainerNode );

            return myStatus;

        }

        public int SaveConfigParameter(XmlDocument pXmlDoc, string pFile) {

            int myStatus = 0;

            pXmlDoc.Save(pFile);

            return myStatus;

        }

        #endregion

        #region txtTriggerEvent_TextChanged

        private void txtTriggerEvent_TextChanged(object sender, EventArgs e) {

        }

        #endregion

        #region factoryDefaultConfigToolStripMenuItem_Click
        
        private void factoryDefaultConfigToolStripMenuItem_Click(object sender, EventArgs e) {

            //This is used to config the default factory configuration parameters
            //Event trigger 
            ConfigEditor myNewConfigEditor = new ConfigEditor (this,  gSelectedLanguage);

            myNewConfigEditor.Show ( );

        }

        #endregion

        #region ttStatusTooltops_Popup

        private void ttStatusTooltops_Popup(object sender, PopupEventArgs e) {

            ttStatusTooltops.ToolTipTitle = "Okay";

        }

        #endregion

        #region picBarDisplayEnergy_Click

        private void picBarDisplayEnergy_Click(object sender, EventArgs e) {

            if (gAutoFittingEnable) {

                picBarDisplayEnergy.Image = Resources.GreenOff;
                gAutoFittingEnable = false;

            } else {

                picBarDisplayEnergy.Image = Resources.GreenOn;
                gAutoFittingEnable = true;
            
            }


        }

        #endregion

        #region scanProtocolToolStripMenuItem_Click

        private void scanProtocolToolStripMenuItem_Click(object sender, EventArgs e) {

            //Load the scan protocol view
            ScanProtocolConfig myScanProtocolView = new ScanProtocolConfig(this);

            myScanProtocolView.Show();

        }

        #endregion

        #region FindAllProtocolFiles

        public int FindAllProtocolFiles(string pRootDirectory) {

            int myProtocolNum = 0;

            gDictProtocolScanParameters.Clear();

            string[] myProtocolFiles = Directory.GetFiles(pRootDirectory);

            myProtocolNum = myProtocolFiles.Count();

            foreach (string myProtocolFileFullPath in myProtocolFiles) {

                string myProtocolName = myProtocolFileFullPath.Replace(pRootDirectory, "");

                myProtocolName = myProtocolName.Replace(".xml", "");

                cScanParameters myProtocolScanParameter = new cScanParameters ( );

                myProtocolScanParameter.FillScanParameterFromFile(myProtocolFileFullPath);

                if (!gDictProtocolScanParameters.ContainsKey(myProtocolName)) {
                 
                    gDictProtocolScanParameters.Add(myProtocolName, myProtocolScanParameter);

                }
                //LoadProtocolFile(myProtocolFile, myProtocolName);

            }

            return myProtocolNum;

        }

        #endregion

        #region RefreshProtocolGUIList

        public void RefreshProtocolGUIList() {

            //Add this section makes the GUI looks better
            //First initialize protocol list
            //This is first initialize, event later the protocol list changes, it will automatically updated
            if (gDictProtocolScanParameters.Count() > 0) {

                foreach (KeyValuePair<string, cScanParameters> myProtocol in gDictProtocolScanParameters) {

                    cbboxPRPRProtocolList.Items.Add(myProtocol.Key);

                }

                cbboxPRPRProtocolList.SelectedIndex = 0;

            }
        
        }

        #endregion

        #region pcBoxPixel_MouseClick

        private void pcBoxPixel_MouseClick(object sender, MouseEventArgs e) {

            EnergyDiagram myEnergyView;

            PictureBox myObject = (PictureBox)sender;

            int myPixelWidth = myObject.ClientSize.Width / gPixelPerRow;
            int myPixelHeight = myObject.ClientSize.Height / gPixelPerCol;

            int myPixelIndexX = (e.X) / myPixelWidth;

            int myPixelIndexY = (e.Y) / myPixelHeight;

            bool myIsAlreadyOpen = false;

            //Check if there is already an energy diagram windows open
            string myFormName = gkFormNamePrefix + "_" + (myPixelIndexY * gPixelPerCol + myPixelIndexX).ToString();

            for (int i = Application.OpenForms.Count - 1; i >= 0; i--) {

                if (Application.OpenForms[i].Name.ToLower() == myFormName.ToLower()) {

                    myIsAlreadyOpen = true;

                    Application.OpenForms[i].Focus();

                }

            }

            //If now windows open, then open one
            if (myIsAlreadyOpen == false) {

                myEnergyView = new EnergyDiagram(this, myPixelIndexY * gPixelPerCol + myPixelIndexX);

                myEnergyView.Show();

            }

        }

        #endregion

        #region Data Acquization Functions

        #region Get Selected Protocol Parameters 
        
        int GetSelectedProtocolParameters(string pProtocol) {

            int myStatus = 0;

            if (gDictProtocolScanParameters.TryGetValue(pProtocol, out gSelectedScanParametersForDataAcq)) {

                //Get the protocol

            } else { 
            
                //No protocol found
                myStatus = cErrorCode.gkecDataAcq_InvalidParameter;
            
            }

            return myStatus;
        
        }



        #endregion 

        private void btnBoxStatus_Click(object sender, EventArgs e)
        {
        
            cScanParameters mySelected = new cScanParameters();

            if(gDictProtocolScanParameters.TryGetValue("DefaultProtocol", out mySelected)){
            
               
                mySelected.SaveScanParametersFiles(mySelected.gOriginalXmlFile, "C:\\Temp\\mytest.xml");


            
            }


        }


        #endregion
         
        #region Used to refresh the comb list

        //Even this more work but it makes sure the list always the latest one
        private void cbboxPRPRProtocolList_MouseClick(object sender, MouseEventArgs e) {

            if (gDictProtocolScanParameters.Count() > 0) {

                cbboxPRPRProtocolList.Items.Clear();

                foreach (KeyValuePair<string, cScanParameters> myProtocol in gDictProtocolScanParameters) {

                    cbboxPRPRProtocolList.Items.Add(myProtocol.Key);

                }
                cbboxPRPRProtocolList.SelectedIndex = 0;

            }

        }

        #endregion

        #region cbboxPRPRProtocolList_SelectedIndexChanged

        private void cbboxPRPRProtocolList_SelectedIndexChanged(object sender, EventArgs e) {

            //Move all this to cbboxPRPRProtocolList_SelectionChangeCommitted to adjust the
            //pixel count box better
            
            if (gDictProtocolScanParameters.Count() > 0) { 
            
                string mySelectedString = "";

                int myTriggerCount = 0;
                
                if (cbboxPRPRProtocolList.SelectedItem != null) {

                    mySelectedString = cbboxPRPRProtocolList.SelectedItem.ToString();

                    cScanParameters myCurrentSelected = new cScanParameters();

                    if (gDictProtocolScanParameters.TryGetValue(mySelectedString, out myCurrentSelected)) {

                        cbboxPRPATriggerType.SelectedIndex = myCurrentSelected.gScanTriggerType;

                        GetCorrectTriggerValueForTriggerType(myCurrentSelected, myCurrentSelected.gScanTriggerType, out myTriggerCount);

                        txtPRPATriggerValue.Text = myTriggerCount.ToString();

                        GetSelectedProtocolParameters(mySelectedString);

                        if( ( int )gSelectedScanParametersForDataAcq.gSourceType == ( int )cScanParameters.eSourceType.LightShare ||
                            ( int )gSelectedScanParametersForDataAcq.gSourceType == ( int )cScanParameters.eSourceType.LightShareSiglePixel ) {

                            lblLightSharePixelCount.Visible = true;
                            txtLightSharePixelCount.Visible = true;
                            panelLighSharePixelCount.Visible = true;


                        } else {

                            lblLightSharePixelCount.Visible = false;
                            txtLightSharePixelCount.Visible = false;
                            panelLighSharePixelCount.Visible = false;

                        }

                    
                    }
                
                }
            
            }


        }

        #endregion

        #region Back Ground Work Related Functions

        private void BackGroundWorkDisapear(bool pFlag) {

            if (pFlag) {

                BeginInvoke(((Action)(() => picProgress.Visible = false)));

                BeginInvoke(((Action)(() => lblLoading.Visible = false)));

            } else {

                BeginInvoke(((Action)(() => picProgress.Visible = true)));

                BeginInvoke(((Action)(() => lblLoading.Visible = true)));
            
            }
        
        }

        private void BackGroundWork_RunBackgroundwork(string pMessage) {

            BeginInvoke(((Action)(() => picProgress.Image = Properties.Resources.BlackSpinner)));

            BeginInvoke(((Action)(() => lblLoading.Text = pMessage)));
        
        }

        private void BackGroundWork_Complete() {

            backgroundWorkerFittingProgress.CancelAsync();
        
        }

        private void backgroundWorkerFittingProgress_DoWork(object sender, DoWorkEventArgs e) {

            Random rand = new Random();

            string myMessage = e.Argument.ToString();

            for (int i = 0; i < 10000; i++) {

                if (backgroundWorkerFittingProgress.CancellationPending) {
                    e.Cancel = true;
                    break;
                }
                // report progress
                backgroundWorkerFittingProgress.ReportProgress(-1, myMessage + ":" + i.ToString());

                // simulate operation step
                System.Threading.Thread.Sleep(rand.Next(100, 1000));

            }

        }

        private void backgroundWorkerFittingProgress_ProgressChanged(object sender, ProgressChangedEventArgs e) {

            if (e.UserState is String) {

                BeginInvoke(((Action)(() => lblLoading.Text = (String)e.UserState)));

            }

        }

        private void backgroundWorkerFittingProgress_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {

            // show result indication
            if (e.Cancelled) {

                BeginInvoke(((Action)(() => lblLoading.Text = "Operation Done")));

                picProgress.Image = null;

            }

        }


        #endregion

        #region Utility Functions

        #region GetCorrectTriggerValueForTriggerType

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pSelectedScanParameter"></param>
        /// <param name="pTriggerType"></param>
        /// <param name="pTriggerValue"></param>
        /// <returns></returns>

        int GetCorrectTriggerValueForTriggerType(cScanParameters pSelectedScanParameter, int pTriggerType, out int pTriggerValue) {

            int myStatus = 0;

            pTriggerValue = -1;

            switch (pTriggerType) {

                case (int)cScanParameters.eScanTriggerType.TimeTrigger:

                    pTriggerValue = pSelectedScanParameter.gScanTriggerTimePeriod;

                    break;

                case (int)cScanParameters.eScanTriggerType.SinglePixelEventCountTrigger:

                    pTriggerValue = pSelectedScanParameter.gScanTriggerSinglePixelEventCount;

                    break;

                case (int)cScanParameters.eScanTriggerType.TotalEventCountTrigger:

                    pTriggerValue = pSelectedScanParameter.gScanTriggerTotalEventCount;

                    break;

                default:

                    break;



            }

            return myStatus;

        }

        #endregion

        #endregion

        #region Event Handler        
        
        #region Button Events

        #region btnGenerateReport_Click

        /// <summary>
        /// This is the handle of the generate report button used to generate result report
        /// this button is only enabled when the fitting is done otherwise it does not 
        /// know to to determine which pixel is pass which pixel fail
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void btnGenerateReport_Click(object sender, EventArgs e) {

            string myExportFileName = "";

            string myFileName = "";

            int myCountCnt = 0;
            int myCountMin = 2147483600;
            int myCountMax = 0;
            int myCountAvg = 0;
            int myCountSum = 0;
            int myAvgSampleCount = 0;

            string myOrderNo = txtOrderNo.Text;
            string myArrayNo = txtArrayNo.Text;

            if ((myOrderNo.Length > 10) || (myArrayNo.Length > 10)) {

                gErrorOutput.OutPutErrorMessage(cErrorCode.gkecReportNoteToolong, "");

                return;

            } else {

                if ((myOrderNo.Length > 0) && (myArrayNo.Length > 0)) {

                    gReportNotes = myOrderNo + ":" + myArrayNo;

                } else {

                    gReportNotes = myOrderNo + myArrayNo;
                
                }
            
            }

            SaveFileDialog mySaveFileDialog = new SaveFileDialog();

            if( gSelectedLanguage == ( int )gLanguageVersion.English ) {

                mySaveFileDialog.Filter = "PDF File|*.pdf";
                mySaveFileDialog.Title = "Save Result Report";

            } else {

                mySaveFileDialog.Filter = "PDF 文件|*.pdf";
                mySaveFileDialog.Title = "保存报告";
            }

            mySaveFileDialog.ShowDialog();

            if (mySaveFileDialog.FileName != "") {

                // Saves the Image via a FileStream created by the OpenFile method.
                myExportFileName = mySaveFileDialog.FileName;
                cReportPDFGenerator myReportGenerator;

                try {

                    myReportGenerator = new cReportPDFGenerator(this, myExportFileName, "TOFTEK", "TOFTEK Report Generator", "Pixel Report", "Pixel Report", "TOFTEK Pixel Report");

                    myReportGenerator.AddGraph(gReportLogo, "");

#if LOCAL_DEBUG
                        
                        for( int i = 0; i <= 255; i++ ) {

                            gEngeryCont[i] = (byte)(gMinimumDisplayEnergyCount + 10);

                            cRawData myTEST = new cRawData( );

                            gEventDataInfo[i].Add( myTEST );
                    
                        }


#endif

                    //Get all count info
                    #region Get all count info

                    for( int myPixelNo = 0; myPixelNo < 256; myPixelNo++ ) {

                        myCountCnt = gFitting.gCountCnt[myPixelNo];

                        if( myCountCnt > gMinimumDisplayEnergyCount ) {

                            gFitting.gCountCnt[myPixelNo] = myCountCnt;

                            myAvgSampleCount++;

                            myCountSum += myCountCnt;

                            if( myCountCnt > myCountMax ) {

                                myCountMax = myCountCnt;

                            }
                            
                            if( myCountCnt < myCountMin ) {

                                myCountMin = myCountCnt;

                            }

                        } else { 
                        
                            //Not enough data, it might be light leak
                        
                        }


                    }

                    gFitting.gCountMax = myCountMax;
                    gFitting.gCountMin = myCountMin;

                    if (myAvgSampleCount > 0) {

                        gFitting.gCountAvg = myCountSum / myAvgSampleCount;


                    } else {

                        gFitting.gCountAvg = 0;
                    
                    }
                    #endregion

                    myReportGenerator.AddResultTable( "TOFTEK Result Report", 17, gFitting, gEngeryCont );

                    //If need report the resolution grey level
                    if (gIsIncludeResolutionGreyPic) {
                     
                        myReportGenerator.AddNewPage();

                        myReportGenerator.AddGraph(gReportLogo, "");

                        myReportGenerator.AddResultTable("TOFTEK Result Report", 17, gFitting, gEngeryCont, cReportPDFGenerator.gkEnergyResolution, true);

                    }

                    //If need report the enegery grey level
                    if (gIsIncludeEnergyGreyPic) {

                        myReportGenerator.AddNewPage();

                        myReportGenerator.AddGraph(gReportLogo, "");

                        myReportGenerator.AddResultTable("TOFTEK Result Report", 17, gFitting, gEngeryCont, cReportPDFGenerator.gkEnergyPeak, false);

                    }


                    //If need report the energy count grey level
                    if (gIsIncludeCountGreyPic) {

                        myReportGenerator.AddNewPage();

                        myReportGenerator.AddGraph(gReportLogo, "");

                        myReportGenerator.AddResultTable("TOFTEK Result Report", 17, gFitting, gEngeryCont, cReportPDFGenerator.gkEnergyCount, false);

                    }
                    //Because using the 
                    if (gIsUseDifferentRangesForPixels) {

                        myReportGenerator.AddNewPage ( );

                        if (gSelectedQualifiedType == 0) {
                         
                            myReportGenerator.AddQualifiedCountTable ( "", 17, gEnergyCountQualifiedLevel );

                        } else if (gSelectedQualifiedType == 1) {

                            myReportGenerator.AddQualifiedResolutionTable ( "", 17, gEnergyResolutionQualifiedLevel );
                        
                        }
                    
                    }

                    //Start to save the energy specturm picture in report
                    if( gIsIncludeEnergySpectrumInReport ) {
                        
                        HangMessageBox myGenerateBox = null;

                        if( gSelectedLanguage == ( int )gLanguageVersion.English ) {

                            myGenerateBox = new HangMessageBox( this, "Drawing Energy Spectrum to report" );

                        } else {

                            myGenerateBox = new HangMessageBox( this, "正在绘制能谱图" );

                        
                        }

                        myGenerateBox.Show(  );

                        //Only if want to include energy spectrum
                        myReportGenerator.AddNewPage( );

                        myReportGenerator.AddGraph(gReportLogo, "");

                        myReportGenerator.AddHeaderMiddleAligned("Energy Spectrum Graphs");
                        
                        Dictionary<int, int> myWholeEngergySpectrum = new Dictionary<int, int>( );
                        
                        #region Loop all pixel

                        // //Add this check because don't want to save the leaking light pixel data
                        for( int myPixelNo = 0; myPixelNo < gkTotalPixelNumbers; myPixelNo++ ) {

                            if( ( ( int )gSelectedScanParametersForDataAcq.gSourceType >= ( int )cScanParameters.eSourceType.LightShare ) ) {

                                #region Use Light Share

                                int myFoundMaxYValue = 0;

                                if( gEventDataInfo[myPixelNo].Count > 0 ) {

                                    IList<System.Windows.Forms.DataVisualization.Charting.DataPoint> myDataSeries = new List<System.Windows.Forms.DataVisualization.Charting.DataPoint>( );
                                    IList<System.Windows.Forms.DataVisualization.Charting.DataPoint> myDataPeakSeries = new List<System.Windows.Forms.DataVisualization.Charting.DataPoint>( );

                                    foreach( cRawData myData in gEventDataInfo[myPixelNo] ) {

                                        if( myData.mData > myFoundMaxYValue ) {

                                            myFoundMaxYValue = myData.mData;
                                        
                                        }

                                        System.Windows.Forms.DataVisualization.Charting.DataPoint myDataPoint = new DataPoint( myData.mAddress * 64, myData.mData );

                                        myDataSeries.Add( myDataPoint );

                                        if (myWholeEngergySpectrum.ContainsKey(myData.mAddress * 64)) {

                                            myWholeEngergySpectrum[myData.mAddress * 64] += myData.mData;

                                        } else {

                                            myWholeEngergySpectrum.Add(myData.mAddress * 64, myData.mData);
                                        
                                        }


                                    }

                                    #region Get Pixel Peak

                                    cFittingParameters myFittingParameters = new cFittingParameters();

                                    if( gFitting.gDictFittingParameters.TryGetValue( myPixelNo, out myFittingParameters ) ) {

                                        if( myFittingParameters.mStatus == ( int )cFittingParameters.eStatus.OK ) {

                                            float myXValue = 0f;
                                            float myYValue = 0f;
                                            float myYG1Value = 0f;
                                            float myYG2Value = 0f;
                                            float myYG3Value = 0f;
                                            float myYLinearValue = 0f;
                                            float myG2Peak = 0f;
                                            float myG2PeakXValue = 0f;

                                            float myRecordG2PeakYValue = 0f;
                                            float myRecordG2PeakXValue = 0f;

                                            float myCalibratedSlope = 1.0f;
                                            float myCalibratedOffset = 0.0f;

                                            int myPixelPosition = 0;

                                            gBinSize = 64;

                                            myPixelPosition = gPixelNumToPositionMap[myPixelNo];

                                            myCalibratedSlope = gSinglePixelCalibrationBuffer.mCalibrationBuffer[myPixelPosition, 0];
                                            myCalibratedOffset = gSinglePixelCalibrationBuffer.mCalibrationBuffer[myPixelPosition, 1];

                                            for( int myIndex = 0; myIndex < 16384 / gBinSize; myIndex++ ) {

                                                myXValue = ( float )( myIndex * gBinSize + gBinSize / 2.0 );

                                                gFitting.FillFittingFormula( myFittingParameters, myXValue, out myYValue, out myYG1Value, out myYG2Value, out myYG3Value, out myYLinearValue, out myG2Peak, out myG2PeakXValue );

                                                /*
                                                if (gSelectedScanParametersForDataAcq.gSourceType == (int)cScanParameters.eSourceType.LightShareSiglePixel) {

                                                    //beause don't change the gause formula then can only do this
                                                    //This is used to solve the problem that the single pixel raw data is calibarted
                                                    //But the fitting is still not using calibrated value
                                                    myXValue = (float)((myIndex * gBinSize + gBinSize / 2.0) * myCalibratedSlope + myCalibratedOffset);

                                                }
                                                 */

                                                #region Add Fitting X Value

                                                if( myYG2Value > myRecordG2PeakYValue ) {

                                                    myRecordG2PeakXValue = myXValue;
                                                    myRecordG2PeakYValue = myYG2Value;

                                                }

                                                #endregion

                                            }

                                            System.Windows.Forms.DataVisualization.Charting.DataPoint myDataPoint0 = new DataPoint( myRecordG2PeakXValue, 0 );

                                            myDataPeakSeries.Add( myDataPoint0 );

                                            System.Windows.Forms.DataVisualization.Charting.DataPoint myDataPoint1 = new DataPoint( myRecordG2PeakXValue, myFoundMaxYValue );

                                            myDataPeakSeries.Add( myDataPoint1 );



                                        }

                                    }

                                    #endregion

                                    myFileName = gkTOFTEKTempRoot + myPixelNo.ToString( ) + ".png";

                                    GeneratePlot( myDataSeries,myDataPeakSeries, myFileName, SeriesChartType.Line );

                                    myReportGenerator.AddPngToReport( myFileName, myPixelNo, true );

                                }

                                #endregion

                            } else {

                                #region No Use Light Share

                                if( ( gEventDataInfo[myPixelNo].Count > 0 ) && ( gEngeryCont[myPixelNo] > gMinimumDisplayEnergyCount ) ) {


                                    //There is data for this pixel
                                    int myDataCountForThisPixel = gEventDataInfo[myPixelNo].Count;

                                    int myDisplayCount = 0;

                                    int myFoundMaxYValue = 0;

                                    IList<System.Windows.Forms.DataVisualization.Charting.DataPoint> myDataSeries = new List<System.Windows.Forms.DataVisualization.Charting.DataPoint>( );
                                    IList<System.Windows.Forms.DataVisualization.Charting.DataPoint> myDataPeakSeries = new List<System.Windows.Forms.DataVisualization.Charting.DataPoint>( );


                                    for( int myIndex = 0; myDisplayCount < myDataCountForThisPixel; myIndex++ ) {

                                        List<cRawData> mySatisfiedRange = gEventDataInfo[myPixelNo].FindAll( cRawData => (
                                            cRawData.mData >= gBinSize * myIndex ) && ( cRawData.mData < gBinSize * ( myIndex + 1 ) ) );

                                        myDisplayCount += mySatisfiedRange.Count( );

                                        if( mySatisfiedRange.Count > myFoundMaxYValue ) {

                                            myFoundMaxYValue = mySatisfiedRange.Count;

                                        }

                                        System.Windows.Forms.DataVisualization.Charting.DataPoint myDataPoint = new DataPoint( gBinSize * myIndex + gBinSize / 2, mySatisfiedRange.Count );

                                        myDataSeries.Add( myDataPoint );

                                    }

                                    #region Get Pixel Peak

                                    cFittingParameters myFittingParameters = new cFittingParameters( );

                                    if( gFitting.gDictFittingParameters.TryGetValue( myPixelNo, out myFittingParameters ) ) {

                                        if( myFittingParameters.mStatus == ( int )cFittingParameters.eStatus.OK ) {

                                            float myXValue = 0f;
                                            float myYValue = 0f;
                                            float myYG1Value = 0f;
                                            float myYG2Value = 0f;
                                            float myYG3Value = 0f;
                                            float myYLinearValue = 0f;
                                            float myG2Peak = 0f;
                                            float myG2PeakXValue = 0f;

                                            float myRecordG2PeakYValue = 0f;
                                            float myRecordG2PeakXValue = 0f;

                                            gBinSize = 64;

                                            for( int myIndex = 0; myIndex < 16384 / gBinSize; myIndex++ ) {

                                                myXValue = ( float )( myIndex * gBinSize + gBinSize / 2.0 );

                                                gFitting.FillFittingFormula( myFittingParameters, myXValue, out myYValue, out myYG1Value, out myYG2Value, out myYG3Value, out myYLinearValue, out myG2Peak, out myG2PeakXValue );

                                                #region Add Fitting X Value

                                                if( myYG2Value > myRecordG2PeakYValue ) {

                                                    myRecordG2PeakXValue = myXValue;
                                                    myRecordG2PeakYValue = myYG2Value;

                                                }

                                                #endregion

                                            }

                                            System.Windows.Forms.DataVisualization.Charting.DataPoint myDataPoint0 = new DataPoint( myRecordG2PeakXValue, 0 );

                                            myDataPeakSeries.Add( myDataPoint0 );

                                            System.Windows.Forms.DataVisualization.Charting.DataPoint myDataPoint1 = new DataPoint( myRecordG2PeakXValue, myFoundMaxYValue );

                                            myDataPeakSeries.Add( myDataPoint1 );



                                        }

                                    }

                                    #endregion

                                    myFileName = gkTOFTEKTempRoot + myPixelNo.ToString( ) + ".png";

                                    GeneratePlot( myDataSeries, myDataPeakSeries, myFileName, SeriesChartType.Line );

                                    myReportGenerator.AddPngToReport( myFileName, myPixelNo, true );


                                } else {

                                    //There is no data for this pixel

                                }

                                #endregion


                            }


                        }


                        #endregion

                        myReportGenerator.AddRestPngToReport( );

                        //Only if want to include energy spectrum
                        myReportGenerator.AddNewPage();

                        myReportGenerator.AddGraph(gReportLogo, "");

                        myReportGenerator.AddHeaderMiddleAligned("Whole Energy Spectrum Graphs");

                        //Generate whole graph
                        #region Add Whole

                        IList<System.Windows.Forms.DataVisualization.Charting.DataPoint> myWholeDataSeries = new List<System.Windows.Forms.DataVisualization.Charting.DataPoint>();
                        
                        IList<System.Windows.Forms.DataVisualization.Charting.DataPoint> myPeakSeries = new List<System.Windows.Forms.DataVisualization.Charting.DataPoint>();

                        int myPeakYValue = -512;
                        int myPeakXValue = -512;

                        foreach (KeyValuePair<int, int> myPair in myWholeEngergySpectrum.OrderBy(i => i.Key)) {

                            System.Windows.Forms.DataVisualization.Charting.DataPoint myDataPoint = new DataPoint(myPair.Key, myPair.Value);

                            if (myPair.Value > myPeakYValue) {

                                myPeakXValue = myPair.Key;

                                myPeakYValue = myPair.Value;
                            
                            }

                            myWholeDataSeries.Add(myDataPoint);

                            gEnergyArea += (double)(myPair.Value);

                        }

                        System.Windows.Forms.DataVisualization.Charting.DataPoint myPeakDataPoint0 = new DataPoint(myPeakXValue, 0);

                        myPeakSeries.Add(myPeakDataPoint0);

                        System.Windows.Forms.DataVisualization.Charting.DataPoint myPeakDataPoint1 = new DataPoint(myPeakXValue, myPeakYValue);

                        myPeakSeries.Add(myPeakDataPoint1);

                        myFileName = gkTOFTEKTempRoot + DateTime.Now.ToFileTime() + ".png";

                        GeneratePlot(myWholeDataSeries, myPeakSeries, myFileName, SeriesChartType.Line, 3);

                        myReportGenerator.AddPngToReport(myFileName);

                        #endregion

                        myGenerateBox.Close( );

                    }

                    if( ( ( int )gSelectedScanParametersForDataAcq.gSourceType >= ( int )cScanParameters.eSourceType.LightShare ) ) {

                        HangMessageBox myGenerateBox = null;

                        if( gSelectedLanguage == ( int )gLanguageVersion.English ) {

                            myGenerateBox = new HangMessageBox( this, "Drawing Count Map to Report" );

                        } else {

                            myGenerateBox = new HangMessageBox( this, "正在绘制Count Map" );


                        }

                        myGenerateBox.Show( );

                        myReportGenerator.AddNewPage( );

                        myReportGenerator.AddGraph(gReportLogo, "");

                        myReportGenerator.AddHeaderMiddleAligned("Count Map");

                        myReportGenerator.AddGraph( gReportCountMapCopy, "" );

                        myGenerateBox.Close( );

                    }

                    myReportGenerator.Close( );

                    if (gSelectedLanguage == (int)gLanguageVersion.Chinese) {

                        MessageBox.Show("保存文件成功");

                    } else {

                        MessageBox.Show("Save file successfully");

                    }


                } catch (IOException) {

                    gErrorOutput.OutPutErrorMessage ( cErrorCode.gkecFileSubError_FileUsedByOthers , "" );

                }



            }


        }

        #endregion

        #region btnStartDataCollection_Click

        /// <summary>
        /// This is the start button event handle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void btnStartDataCollection_Click(object sender, EventArgs e) {

            int myStatus = 0;

            UInt16 myFPGAStatus = 0;

            string mySelectedProtocol = "";

            string mySelectedTriggerType = "";

#if LOCAL_DEBUG

            InitializeParameter( );

            #region Reset Display

            SetPixelValue( 0, ( byte )gEngeryCont[0], pcBoxPixel, gEngeryCont );

            gDisplayTracker = 1;

            gSwitchDisplay = false;

            BeginInvoke( ( ( Action )( ( ) => picLightDivideDisplay.Visible = false ) ) );

            #endregion

            //Only for debug
            #region Debug

            gIsFittingDone = false;

            //Use to share with Energy Diagram and refresh the 
            //graph display
            gRefreshAllEnergyDiagram = true;

            gIsInFittingProcess = false;

            EnergyDiagram.ResetClearEnergyDisplayFlag( );

            EnergyDiagram.ResetClearFittingFlag( );

            gFitting.gDictFittingParameters.Clear( );

            if( (int)gSelectedScanParametersForDataAcq.gSourceType == (int)cScanParameters.eSourceType.LightShare ) {
             
                if( !UInt32.TryParse( txtLightSharePixelCount.Text, out gLightSharePixelNo ) ) {

                    myStatus = cErrorCode.gkedFitting_PixelCountMissing;

                    gErrorOutput.OutPutErrorMessage( myStatus, "" );

                    return;

                }

            }

            if( float.TryParse( txtPeakLowLimit.Text, out gPeakLowLimit ) && float.TryParse( txtPeakHighLimit.Text, out gPeakHighLimit ) ) {



            } else {

                gPeakHighLimit = 0f;
                gPeakLowLimit = 0f;

            }

            gFittingDataFile = gkRawDataDirectory + "2017_10_26_16_36_16_lightShareSinglePixel.npz"; //gkRawDataDirectory + "2017_7_29_17_2_49_lightShare.npz";

            BeginInvoke(((Action)(() => btnGenerateReport.Visible = false)));

            BeginInvoke(((Action)(() => grpReportNote.Visible = false)));

            //gFittingDataFile = gkRawDataDirectory + "2017_8_12_11_32_3_lightShare.npz";



            #region Read Raw Data
            
            /*
            char[] myDelimiters = new char[] { '\r', '\n', ',' };

            System.IO.FileStream myFileStream = new System.IO.FileStream( gFittingDataFile, System.IO.FileMode.Open, System.IO.FileAccess.Read );

            StreamReader myReader = new StreamReader( myFileStream );

            string myContent = myReader.ReadToEnd( );

            string[] mySeperatedData = Regex.Split( myContent, "\r\n" ); // myContent.Split( "\r\n", StringSplitOptions.RemoveEmptyEntries );

            for( int i = 0; i < mySeperatedData.Count( ); i++ ) {

                string[] myData = mySeperatedData[i].Split( ',' );

                byte myAddress = 0;

                float myRawData = 0f;


                if( byte.TryParse( myData[0], out myAddress ) && float.TryParse( myData[1], out myRawData ) ) {

                    cRawData myPrecheckDataPoint = new cRawData(myAddress, (int)myRawData);

                    #region Light Share

                    gChecksumDataQueue.Enqueue(myPrecheckDataPoint);

                    gChecksumDataChecksum += myPrecheckDataPoint.mAddress;

                    if ((gChecksumDataChecksum == gkLightShareEvenChecksum) || (gChecksumDataChecksum == gkLightShareOddChecksum)) {

                        for (int j = 0; j < 4; j++) {

                            cRawData myDataPoint = new cRawData();

                            gChecksumDataQueue.TryDequeue(out myDataPoint);

                            gRawDataQueue.Enqueue(myDataPoint);

                            //Total Event count ++
                            gTotalEventCount++;

                            gChecksumDataChecksum = 0;

                        }

                    } else if (gChecksumDataQueue.Count >= 4) {

                        //Get the bad data from the queue
                        cRawData myDiscardDataPoint = new cRawData();

                        gChecksumDataQueue.TryDequeue(out myDiscardDataPoint);

                        //Get the bad data address from the queue
                        gChecksumDataChecksum -= myDiscardDataPoint.mAddress;

                    }

                    #endregion

                }


            }

            myReader.Close( );
            myFileStream.Close( );

            gFittingDataFile = gkRawDataDirectory + "2017_8_30_10_1_56_lightShare196Version.npz";
            SaveFileForFitting(gEventDataInfo, gFittingDataFile);
            */

            #endregion

            if( UInt32.TryParse( txtFittingLowBand.Text, out gFittingLowBand ) && UInt32.TryParse( txtFittingUpBand.Text, out gFittingUpBand ) ) {


            } else {

                gFittingLowBand = 2550;
                gFittingUpBand = 5000;

            }

            gFittingStartEvent.Set( );

            return;

            #endregion

#endif


            if (btnStartDataCollection.Text.ToLower() == "start" || btnStartDataCollection.Text == "开始") {

                if( ( int )gSelectedScanParametersForDataAcq.gSourceType == ( int )cScanParameters.eSourceType.LightShare ) {
                 
                    if( !UInt32.TryParse( txtLightSharePixelCount.Text, out gLightSharePixelNo ) ) {

                        myStatus = cErrorCode.gkedFitting_PixelCountMissing;

                        gErrorOutput.OutPutErrorMessage( myStatus, "" );

                        return;

                    }

                }


                if (float.TryParse(txtPeakLowLimit.Text, out gPeakLowLimit) && float.TryParse(txtPeakHighLimit.Text, out gPeakHighLimit)) {



                } else {

                    gPeakHighLimit = gkPeakLowLimit;
                    gPeakLowLimit = gkPeakHighLimit;

                }

                if( UInt32.TryParse( txtFittingLowBand.Text, out gFittingLowBand ) && UInt32.TryParse( txtFittingUpBand.Text, out gFittingUpBand ) ) {


                } else {

                    gFittingLowBand = 2550;
                    gFittingUpBand = 5000;

                }


                if (cbboxPRPRProtocolList.SelectedItem != null) {

                    mySelectedProtocol = cbboxPRPRProtocolList.SelectedItem.ToString();

                    if (mySelectedProtocol.Length > 0) {

                        GetSelectedProtocolParameters(mySelectedProtocol);

                        #region Send Message and start to acquire data

                        InitializeParameter();

                        #region Reset Display

                        SetPixelValue( 0, ( byte )gEngeryCont[0], pcBoxPixel, gEngeryCont );

                        gDisplayTracker = 1;

                        gSwitchDisplay = false;

                        BeginInvoke( ( ( Action )( ( ) => picLightDivideDisplay.Visible = false ) ) );

                        #endregion

                        #region Set Configuration

                        //Set Vref Power
                        SetPower(gkVrefPowerIndex, (UInt16)gSelectedScanParametersForDataAcq.gVref);

                        //Set Vbias Power
                        SetPower( gkVbiasPowerIndex, ( UInt16 )gSelectedScanParametersForDataAcq.gVbias );

                        //Write mode
                        //Correction Byte|Integral Time|Encoding Mode|ADC Trigger Threshold
                        if( ( int )gSelectedScanParametersForDataAcq.gSourceType >= (int)cScanParameters.eSourceType.LightShare) {

                            #region Light Share

                            //Correction byte bit6 = 1 & Integral time | Not Encoding | Trigger
                            if( gSelectedScanParametersForDataAcq.gADCTriggerThreshold > 3 ) {

                                //When threshold is greater than 3 then it means it use a continus threshold then in mode register it does not matter
                                //So here we don't care the threshold value
                                WriteADCModeReg( ( byte )( 0x40 | ( gSelectedScanParametersForDataAcq.gIntegralTime << 4 ) | 0x0C  ) );

                                WriteLightShareADCThresholdReg( ( UInt16 )gSelectedScanParametersForDataAcq.gADCTriggerThreshold );

                            } else {

                                WriteADCModeReg( ( byte )( 0x40 | ( gSelectedScanParametersForDataAcq.gIntegralTime << 4 ) | 0x0C | ( gSelectedScanParametersForDataAcq.gADCTriggerThreshold ) ) );

                            }

                            #endregion


                        } else {

                            #region Not Light Share 
                            WriteADCModeReg((byte)((gSelectedScanParametersForDataAcq.gCorrectionOption << 6) | (gSelectedScanParametersForDataAcq.gIntegralTime << 4)
                                            | (gSelectedScanParametersForDataAcq.gEncodingMode << 2) | (gSelectedScanParametersForDataAcq.gADCTriggerThreshold)));

                            #endregion
                        
                        }


                        //Set ADC range
                        WriteADCConfig(gkADCRangeIndex, (byte)gSelectedScanParametersForDataAcq.gADCRange);

                        if ((cbboxPRPATriggerType.SelectedItem != null) && (txtPRPATriggerValue.Text.Length > 0)) {

                            //No matter if it is same with the protocol, if there is value, then it is high priority
                            int myTempBuffer = -1;

                            if (int.TryParse(txtPRPATriggerValue.Text, out myTempBuffer)) {

                                //Get the trigger count 
                                gTemperaryTriggerCount = myTempBuffer;

                                //It means there are trigger type need to overwrite
                                mySelectedTriggerType = cbboxPRPATriggerType.SelectedItem.ToString();

                                if (mySelectedTriggerType.ToLower() == cScanParameters.eScanTriggerType.AnalysisResultTrigger.ToString().ToLower()) {

                                    gTemperaryTriggerType = (int)cScanParameters.eScanTriggerType.AnalysisResultTrigger;

                                } else if (mySelectedTriggerType.ToLower() == cScanParameters.eScanTriggerType.SinglePixelEventCountTrigger.ToString().ToLower()) {

                                    gTemperaryTriggerType = (int)cScanParameters.eScanTriggerType.SinglePixelEventCountTrigger;

                                } else if (mySelectedTriggerType.ToLower() == cScanParameters.eScanTriggerType.TotalEventCountTrigger.ToString().ToLower()) {

                                    gTemperaryTriggerType = (int)cScanParameters.eScanTriggerType.TotalEventCountTrigger;

                                } else if (mySelectedTriggerType.ToLower() == cScanParameters.eScanTriggerType.TimeTrigger.ToString().ToLower()) {

                                    gTemperaryTriggerType = (int)cScanParameters.eScanTriggerType.TimeTrigger;

                                }

                            } else {

                                //No value entered for the trigger type
                                //Use the protocol's settings
                                gTemperaryTriggerType = gSelectedScanParametersForDataAcq.gScanTriggerType;

                                GetCorrectTriggerValueForTriggerType(gSelectedScanParametersForDataAcq, gTemperaryTriggerType, out gTemperaryTriggerCount);

                            }



                        } else {

                            //No over write selected for this
                            //Use the protocol's settings
                            gTemperaryTriggerType = gSelectedScanParametersForDataAcq.gScanTriggerType;

                            GetCorrectTriggerValueForTriggerType(gSelectedScanParametersForDataAcq, gTemperaryTriggerType, out gTemperaryTriggerCount);

                        }

                        myStatus = CheckStatus(out myFPGAStatus);

                        #endregion

                        if (myStatus != 0) {

                            //Retry one more time
                            #region Set Configuration

                            //Set Vref Power
                            SetPower( gkVrefPowerIndex, ( UInt16 )gSelectedScanParametersForDataAcq.gVref );

                            //Set Vbias Power
                            SetPower( gkVbiasPowerIndex, ( UInt16 )gSelectedScanParametersForDataAcq.gVbias );

                            if( ( ( int )gSelectedScanParametersForDataAcq.gSourceType >= ( int )cScanParameters.eSourceType.LightShare )  ) {

                                #region Light Share

                                //Correction byte bit6 = 1 & Integral time | Not Encoding | Trigger
                                WriteADCModeReg( ( byte )( 0x40 | ( gSelectedScanParametersForDataAcq.gIntegralTime << 4 ) | 0x0C | ( gSelectedScanParametersForDataAcq.gADCTriggerThreshold ) ) );

                                #endregion

                            } else {

                                #region Not Light Share
                                //Write mode
                                //Correction Byte|Integral Time|Encoding Mode|ADC Trigger Threshold
                                WriteADCModeReg( ( byte )( ( gSelectedScanParametersForDataAcq.gCorrectionOption << 6 ) | ( gSelectedScanParametersForDataAcq.gIntegralTime << 4 )
                                                | ( gSelectedScanParametersForDataAcq.gEncodingMode << 2 ) | ( gSelectedScanParametersForDataAcq.gADCTriggerThreshold ) ) );

                                #endregion

                            }

                            //Set ADC range
                            WriteADCConfig( gkADCRangeIndex, ( byte )gSelectedScanParametersForDataAcq.gADCRange );

                            if( ( cbboxPRPATriggerType.SelectedItem != null ) && ( txtPRPATriggerValue.Text.Length > 0 ) ) {

                                //No matter if it is same with the protocol, if there is value, then it is high priority
                                int myTempBuffer = -1;

                                if( int.TryParse( txtPRPATriggerValue.Text, out myTempBuffer ) ) {

                                    //Get the trigger count 
                                    gTemperaryTriggerCount = myTempBuffer;

                                    //It means there are trigger type need to overwrite
                                    mySelectedTriggerType = cbboxPRPATriggerType.SelectedItem.ToString( );

                                    if( mySelectedTriggerType.ToLower( ) == cScanParameters.eScanTriggerType.AnalysisResultTrigger.ToString( ).ToLower( ) ) {

                                        gTemperaryTriggerType = ( int )cScanParameters.eScanTriggerType.AnalysisResultTrigger;

                                    } else if( mySelectedTriggerType.ToLower( ) == cScanParameters.eScanTriggerType.SinglePixelEventCountTrigger.ToString( ).ToLower( ) ) {

                                        gTemperaryTriggerType = ( int )cScanParameters.eScanTriggerType.SinglePixelEventCountTrigger;

                                    } else if( mySelectedTriggerType.ToLower( ) == cScanParameters.eScanTriggerType.TotalEventCountTrigger.ToString( ).ToLower( ) ) {

                                        gTemperaryTriggerType = ( int )cScanParameters.eScanTriggerType.TotalEventCountTrigger;

                                    } else if( mySelectedTriggerType.ToLower( ) == cScanParameters.eScanTriggerType.TimeTrigger.ToString( ).ToLower( ) ) {

                                        gTemperaryTriggerType = ( int )cScanParameters.eScanTriggerType.TimeTrigger;

                                    }

                                } else {

                                    //No value entered for the trigger type
                                    //Use the protocol's settings
                                    gTemperaryTriggerType = gSelectedScanParametersForDataAcq.gScanTriggerType;

                                    GetCorrectTriggerValueForTriggerType( gSelectedScanParametersForDataAcq, gTemperaryTriggerType, out gTemperaryTriggerCount );

                                }



                            } else {

                                //No over write selected for this
                                //Use the protocol's settings
                                gTemperaryTriggerType = gSelectedScanParametersForDataAcq.gScanTriggerType;

                                GetCorrectTriggerValueForTriggerType( gSelectedScanParametersForDataAcq, gTemperaryTriggerType, out gTemperaryTriggerCount );

                            }

                            myStatus = CheckStatus( out myFPGAStatus );

                            #endregion

                            //Show error 
                            if( myStatus != 0 ) {
                             
                                myStatus = cErrorCode.gkecHardwareConnectionError;

                                gErrorOutput.OutPutErrorMessage( myStatus, "" );

                            }

                        } 
                        
                        //Updated this because if second retry can fix this problem, it can go ahead to 
                        //acquiring data
                        if(myStatus == 0 ) {

                            #region Check if status okay to run data acq

                            //if ( ( (myFPGAStatus & gkDarkBoxStatusBitMask) == gkDarkBoxClosed) &&
                            //     ( (myFPGAStatus & gkADCPLLStatusBitMask) == gkADCPLLOK) &&
                            //     ( (myFPGAStatus & gkADCBiasStatusBitMask) == gkADCBiasOn ) &&
                            //     ( (myFPGAStatus & gkRunningStatusBitMask) == 0) ) { 

                            if (myFPGAStatus == 0) {

                                //Update temperature
                                if (GetThermoInfo(gkRequestAvgThermoSensorIndex, out gAverageTemperature) == 0) {

                                    if( gIsAutoAdjustVbias ) {

                                        SetPower( gkVbiasPowerIndex, ( UInt16 )( gSelectedScanParametersForDataAcq.gVbias + Math.Round( ( gAverageTemperature - 25.0 ) * 4.8 ) ) );

                                    } 

                                    vprgBarAveTemp.Value = (int)gAverageTemperature;

                                    lblAverage.Text = gAverageTemperature.ToString("F1");

                                    if( gAverageTemperature > 40.0f ) {

                                        vprgBarAveTemp.Color = Color.Red;

                                    } else {

                                        vprgBarAveTemp.Color = Color.SpringGreen;

                                    }

                                }

                                //Check Box is closed
                                //ADC PLL is okay
                                //ADC Bias is ON
                                //It is not running yet

                                #region TODO:Add this later
                                /*
                                #region Set the pixel box graph section

                                if ((gSelectedScanParametersForDataAcq.gPixelNumsPerRow == 8) && (gSelectedScanParametersForDataAcq.gPixelNumsPerCol == 8)) {

                                    gSubModulePerBankRow = 1;
                                    gSubModulePerBankCol = 1;
                                    gPixelPerRow = 8;
                                    gPixelPerCol = 8;
                                    gPixelPerSubModuleRow = 8;
                                    gPixelPerSubModuleCol = 8;

                                    pcBoxPixel.Refresh();

                                } else if ((gSelectedScanParametersForDataAcq.gPixelNumsPerRow == 8) && (gSelectedScanParametersForDataAcq.gPixelNumsPerCol == 8)) {

                                    gSubModulePerBankRow = 2;
                                    gSubModulePerBankCol = 2;
                                    gPixelPerRow = 16;
                                    gPixelPerCol = 16;
                                    gPixelPerSubModuleRow = 8;
                                    gPixelPerSubModuleCol = 8;

                                    pcBoxPixel.Refresh();


                                }

                                #endregion
                                */
                                #endregion

                                btnStartDataCollection.Image = Resources.SimpleCancelBlack;

                                if (gSelectedLanguage == (int)gLanguageVersion.Chinese) {

                                    btnStartDataCollection.Text = "停止";

                                } else {

                                    btnStartDataCollection.Text = "Stop";

                                }

                                myStatus = StartDataCollection(true);

                                gRefreshTimer.Start();

                                BeginInvoke(((Action)(() => btnGenerateReport.Visible = false)));

                                BeginInvoke(((Action)(() => grpReportNote.Visible = false)));
                                

                                //Clear Fitting flag
                                gIsFittingDone = false;

                                Fitting.gCurrentRecvLineNo = 0;

                                //Use to share with Energy Diagram and refresh the 
                                //graph display
                                gRefreshAllEnergyDiagram = true;

                                gIsInFittingProcess = false;

                                EnergyDiagram.ResetClearEnergyDisplayFlag();

                                EnergyDiagram.ResetClearFittingFlag();

                                gFitting.gDictFittingParameters.Clear();

                                if (myStatus != 0) {

                                    MessageBox.Show("Please check connection(请验证线缆连接)");

                                } else {

                                    gCollectionProcessDone = false;

                                }

                                FTDIDequeueStart.Set();

                            }

                            #endregion

                        }



                        #endregion


                    } else {

                        //Selected item is wrong
                        //Response to if (mySelectedProtocol.Length <= 0)

                        myStatus = cErrorCode.gkecDataAcq_InvalidProtocol;

                        gErrorOutput.OutPutErrorMessage(myStatus, "");

                    }

                } else {

                    //No protocol selected 
                    //Response to if (cbboxPRPRProtocolList.SelectedItem != null) {

                    myStatus = cErrorCode.gkecDataAcq_InvalidProtocol;

                    gErrorOutput.OutPutErrorMessage(myStatus, "");

                }



            } else {

                #region Stop button is pressed

                if (gSelectedLanguage == (int)gLanguageVersion.Chinese) {

                    btnStartDataCollection.Text = "开始";

                } else {

                    btnStartDataCollection.Text = "Start";

                }
                btnStartDataCollection.Image = Resources.CircledPlayBlack1;

                gCollectionProcessDone = true;

                gIsDataReadyToSave = false;

                //When manually stop the ACQ, need clear this
                gTemperaryTriggerCount = 0;

                myStatus = StartDataCollection(false);

                if (myStatus != 0) {

                    MessageBox.Show("Please check connection(请验证线缆连接)");

                } else {

                    if (gIsAutoFitting) {

                        SentFittingNotice();

                    }

                    gCollectionProcessDone = false;

                }
                #endregion

            }

            Thread.Sleep(100);

                

        }

        #endregion

        #region btnExport_Click

        private void btnExport_Click(object sender, EventArgs e) {

            string myExportFileName = "";
            string myTimestamp = "";

            SaveFileDialog mySaveFileDialog = new SaveFileDialog();

            if (gSelectedLanguage == (int)gLanguageVersion.English) {

                mySaveFileDialog.Filter = "Binary File(Can Use for fitting)|*.npz|CSV File(Only for view)|*.csv|TXT File(Only for view)|*.txt";
                mySaveFileDialog.Title = "Save Raw Data";

            } else {

                mySaveFileDialog.Filter = "二进制文件(可以用来fitting)|*.npz|CSV 文件(不可用来fitting)|*.csv|TXT 文件(不可用来fitting)|*.txt";
                mySaveFileDialog.Title = "保存原始数据";
            }

            mySaveFileDialog.ShowDialog();

            if (mySaveFileDialog.FileName != "") {

                // Saves the Image via a FileStream created by the OpenFile method.
                myExportFileName = mySaveFileDialog.FileName;

                int myTotalPixelCount = gEventDataInfo.Count();

                HangMessageBox myHangmessagebox = new HangMessageBox(this, myTotalPixelCount);

                myHangmessagebox.StartPosition = FormStartPosition.CenterScreen;

                myHangmessagebox.Show();

                switch (mySaveFileDialog.FilterIndex) {

                    case 1:
                        //Save binary file
                        #region Save As NPZ File

                        try {

                            System.IO.FileStream myBinFileHandler = new FileStream(myExportFileName, System.IO.FileMode.CreateNew, System.IO.FileAccess.Write);
                            BinaryWriter myBinStreamWrite = new BinaryWriter(myBinFileHandler);

                            #region Add Header Section

                            myBinStreamWrite.Write( ( UInt32 )gSelectedScanParametersForDataAcq.gSourceType );

                            for( int myLineNumber = 1; myLineNumber < 16; myLineNumber++ ) {

                                myBinStreamWrite.Write( ( UInt32 )myLineNumber );

                            }

                            #endregion

                            if( ( ( int )gSelectedScanParametersForDataAcq.gSourceType >= ( int )cScanParameters.eSourceType.LightShare ) ) {

                                #region Light Share Mode

                                /*
                                int myDatCount = gRawDataQueue.Count;

                                cRawData myRawData = new cRawData();

                                for (int i = 0; i < myDatCount; i++) {

                                    if (gRawDataQueue.TryDequeue(out myRawData)) {

                                        myBinStreamWrite.Write(myRawData.mAddress);

                                        myBinStreamWrite.Write((short)myRawData.mData);

                                    }

                                }
                                */

                                foreach( cRawData myRawData in gLightShareRawData ) {

                                    myBinStreamWrite.Write( ( ushort )myRawData.mAddress );

                                    myBinStreamWrite.Write( ( ushort )myRawData.mData );

                                }

                                #endregion

                            } else {
                                
                                #region Not Light Share Mode

                                myTotalPixelCount = gEventDataInfo.Count();

                                for (int i = 0; i < myTotalPixelCount; i++) {

                                    //Add this check because don't want to save the leaking light pixel data
                                    if (gEngeryCont[i] > gMinimumDisplayEnergyCount) {

                                        foreach (cRawData myRawData in gEventDataInfo[i]) {

                                            myBinStreamWrite.Write( ( ushort )myRawData.mAddress );

                                            myBinStreamWrite.Write( ( ushort )myRawData.mData );

                                        }

                                    }

                                }

                                #endregion
                            
                            }

                            myBinFileHandler.Close();
                            myBinStreamWrite.Close();

                        } catch {


                        }

                        #endregion

                        break;

                    case 2:

                        //Save as .csv file
                        #region Save As CSV File

                        myTimestamp = DateTime.Now.ToString(); ;
                        myTimestamp = myTimestamp.Replace(',', ' ');

                        try {

                            System.IO.FileStream myFileHandler = new FileStream(myExportFileName, System.IO.FileMode.Create, System.IO.FileAccess.Write);
                            StreamWriter myStreamWriter = new StreamWriter(myFileHandler, System.Text.Encoding.UTF8);

                            #region Add Header Section

                            //Write Header
                            myStreamWriter.WriteLine( "Header0:" + gSelectedScanParametersForDataAcq.gSourceType.ToString() );

                            for( int myLineNumber = 1; myLineNumber < 16; myLineNumber++ ) {

                                myStreamWriter.WriteLine( "Header" + myLineNumber.ToString( ) );

                            }

                            #endregion

                            if( ( ( int )gSelectedScanParametersForDataAcq.gSourceType >= ( int )cScanParameters.eSourceType.LightShare ) ) {

                                #region Light Share Mode

                                foreach( cRawData myRawData in gLightShareRawData ) {

                                    myStreamWriter.WriteLine( myRawData.mAddress.ToString( ) + "," + myRawData.mData.ToString( ) );
                                
                                }
                                /*
                                int myDatCount = gRawDataQueue.Count;

                                cRawData myRawData = new cRawData();

                                for (int i = 0; i < myDatCount; i++) {

                                    if (gRawDataQueue.TryDequeue(out myRawData)) {

                                        myStreamWriter.WriteLine(myRawData.mAddress.ToString() + "," + myRawData.mData.ToString());

                                    }

                                }*/

                                #endregion

                            } else {
                            
                                 #region Not Light Share Mode

                                for (int i = 0; i < myTotalPixelCount; i++) {

                                    //Add this check because don't want to save the leaking light pixel data
                                    if (gEngeryCont[i] > gMinimumDisplayEnergyCount) {

                                        foreach (cRawData myRawData in gEventDataInfo[i]) {

                                            myStreamWriter.WriteLine(i.ToString() + "," + myRawData.mData.ToString());

                                        }

                                    }


                                }

                                #endregion                           
                            }

                            myStreamWriter.Close();
                            myFileHandler.Close();



                        } catch (IOException) {

                            gErrorOutput.OutPutErrorMessage(cErrorCode.gkecFileSubError_FileUsedByOthers, "");

                        } catch (Exception pException) {

                            gErrorOutput.OutPutErrorMessage(0, pException.Message);

                        }

                        #endregion

                        break;
                    case 3:
                        //Save as .txt file
                        #region Save As TXT File

                        myTimestamp = DateTime.Now.ToString();
                        myTimestamp = myTimestamp.Replace(',', ' ');

                        try {

                            System.IO.FileStream myFileHandler = new FileStream(myExportFileName, System.IO.FileMode.Create, System.IO.FileAccess.Write);
                            StreamWriter myStreamWriter = new StreamWriter(myFileHandler, System.Text.Encoding.UTF8);

                            #region Add Header Section

                            //Write Header
                            myStreamWriter.WriteLine( "Header0:" + gSelectedScanParametersForDataAcq.gSourceType.ToString( ) );

                            for( int myLineNumber = 1; myLineNumber < 16; myLineNumber++ ) {

                                myStreamWriter.WriteLine( "Header" + myLineNumber.ToString( ) );

                            }

                            #endregion

                            for (int i = 0; i < myTotalPixelCount; i++) {

                                /*int myPixelCount = gEventDataInfo[i].Count();

                                int myFoundCount = 0;
                                
                                for (int myIndex = 0; myFoundCount < myPixelCount; myIndex++) {

                                    List<cRawData> mySatisfiedRange = gEventDataInfo[i].FindAll(cRawData => (cRawData.mData >= gBinSize * myIndex) && (cRawData.mData < gBinSize * (myIndex + 1)));

                                    myFoundCount += mySatisfiedRange.Count;

                                    System.IO.File.AppendAllText(myExportFileName, i.ToString() + "," + (gBinSize * myIndex + gBinSize / 2).ToString() + "," + mySatisfiedRange.Count.ToString() + "\r\n");

                                    //myHangmessagebox.UpdateProgressBar(myPixelCount, i);

                                }*/

                                //Add this check because don't want to save the leaking light pixel data
                                if( gEngeryCont[i] > gMinimumDisplayEnergyCount ) {

                                    foreach( cRawData myRawData in gEventDataInfo[i] ) {

                                        myStreamWriter.WriteLine( i.ToString( ) + "," + myRawData.mData.ToString( ) );

                                    }

                                }


                            }

                            myStreamWriter.Close();
                            myFileHandler.Close();

                        } catch (IOException) {

                            gErrorOutput.OutPutErrorMessage ( cErrorCode.gkecFileSubError_FileUsedByOthers, "" );

                        } catch (Exception pException) {

                            gErrorOutput.OutPutErrorMessage(0, pException.Message);

                        }

                        #endregion
                        break;
                    default:
                        break;
                }

                myHangmessagebox.Close();
                gErrorOutput.OutPutErrorMessage(cErrorCode.gkecDataExport_FileSavedOK, "");

            } else {

                //Tested this part
                //Only click cancel button can cause this run
                //But in our situation, if cancel button is clicked, does not care the result
                //gErrorOutput.OutPutErrorMessage(cErrorCode.gkecDataExport_NoFileSelected,"");

            }


        }

        #endregion

        #region btnSend_Click

        /// <summary>
        /// This button only used in debug mode
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void btnSend_Click(object sender, EventArgs e) {

            int myDataSize = 0;

            UInt32 myBytesWritten = 0;

            cCommand myCommand = new cCommand();

            //Frist check if board is connected
            if (gFTDI.IsOpen) {

                myCommand.mCommandType = 0xFF;
                myCommand.mMessage[0] = myCommand.mCommandType;

                if (cbCmdType.SelectedIndex >= 0) {

                    myCommand.mCommand = gCommandTypes[cbCmdType.SelectedIndex];

                    myCommand.mMessage[1] = myCommand.mCommand;

                } else {

                    MessageBox.Show("Please select one command");

                    return;


                }

                myDataSize = tbDataInput.Text.ToString().Count();

                if (myDataSize > 0) {

                    UInt16 myData = 0;

                    myData = Convert.ToUInt16(tbDataInput.Text, 16);

                    myCommand.mData[0] = Convert.ToByte(((myData >> 8) & 0x00ff));

                    myCommand.mData[1] = Convert.ToByte((myData & 0x00ff));

                    myCommand.mMessage[2] = myCommand.mData[0];
                    myCommand.mMessage[3] = myCommand.mData[1];

                    gFTDI.Write(myCommand.mMessage, 4, ref myBytesWritten);

                    if (myBytesWritten != 4) {

                        myAppendText(rtbLog, "Write to device wrong", Color.Red);

                    } else {

                        string myMessage = myCommand.mMessage[0].ToString("X2") + " " +
                                           myCommand.mMessage[1].ToString("X2") + " " +
                                           myCommand.mMessage[2].ToString("X2") + " " +
                                           myCommand.mMessage[3].ToString("X2");

                        myAppendText(rtbLog, "Write: " + myMessage, Color.Blue);

                    }


                } else {

                    MessageBox.Show("Please enter data, e.g. 0x0102");

                }



            } else {

                MessageBox.Show("No device or device is not opened");

            }

            Thread.Sleep(100);

            gCollectionProcessDone = false;

            FTDIDequeueStart.Set();

        }

        #endregion

        #endregion

        #region aboutToolStripMenuItem_Click

        /// <summary>
        /// This is the menustrip click of About
        /// Used to show the software version
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e) {

            string myMessage = "";

            if (gDeviceNode != null) {

                myMessage = "Software Version: " + gkVersion + "\n" +
                              "DLL Version: " + gD2XXDLLVersion.ToString() + "\n" +
                              "Driver Version: " + gDeviceNode.Description + " " + gDeviceNode.ID + " " + gDeviceNode.SerialNumber;


            } else {

                myMessage = "Software Version: " + gkVersion + "\n" +
                            "DLL Version: " + gD2XXDLLVersion.ToString() + "\n" +
                            "No device found";

            }

            MessageBox.Show(myMessage);


        }

        #endregion

        #region DemoClose

        /// <summary>
        /// Used to confirm with user to close the application
        /// </summary>
        /// <returns></returns>

        private bool DemoClose() {

            //First check if it is in collecting data mode
            string myWarning = "Want to close when is collecting data?";
            string myTitle = "Close Comfirmation";

            if( gSelectedLanguage == (int)gLanguageVersion.Chinese ) {

                myTitle = "关闭确认";
                myWarning = "确认关闭？";
            
            }


            if (MessageBox.Show(myWarning, myTitle, MessageBoxButtons.YesNo) == DialogResult.Yes) {

                //Sure to close
                try {

                    if (gFTDI.IsOpen) {

                        if (gCollectionProcessDone == false) {

                            //Stop data collection
                            StartDataCollection(false);

                        }

                        gFTDI.Close();

                    }

                } catch (Exception) {

                    //No device opened yet
                    //Todo: log this error

                }

                return true;

            } else {

                return false;

            }


        }

        #endregion

        #region closeToolStripMenuItem_Click

        /// <summary>
        /// When the Close option is selected in menustrip
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void closeToolStripMenuItem_Click(object sender, EventArgs e) {

            //This will trigger Demo_FormClosing
            Application.Exit();

        }

        #endregion

        #region Demo_FormClosing

        /// <summary>
        /// Close the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void Demo_FormClosing(object sender, System.ComponentModel.CancelEventArgs e) {

            if (!DemoClose()) {

                e.Cancel = true;

            } else {

                gFTDIInqueueThread.Abort();
                gFTDIDequeueThread.Abort();
                gFittingThread.Abort();
                gRefreshTimer.Stop();

                e.Cancel = false;

            }

        }

        #endregion

        #region pcBoxPixel_Paint

        /// <summary>
        /// Pixel color display filed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void pcBoxPixel_Paint(object sender, PaintEventArgs e) {

            PictureBox myObject = ( PictureBox )sender;

            //Based on width to design the drawing area
            myObject.Height = myObject.Width / gSubModulePerBankCol * gSubModulePerBankRow;

            // Lines between pixels.
            for( int i = 0; i < gSubModulePerBankCol; i++ ) {

                for( int j = 1; j < gPixelPerSubModuleCol; j++ ) {

                    e.Graphics.DrawLine( new System.Drawing.Pen( System.Drawing.Color.WhiteSmoke, 2f ), new Point( myObject.Width / gSubModulePerBankCol / gPixelPerSubModuleCol * ( i * gPixelPerSubModuleCol + j ), 0 ), new Point( myObject.Width / gSubModulePerBankCol / gPixelPerSubModuleCol * ( i * gPixelPerSubModuleCol + j ), myObject.Height ) );
                }

            }

            for( int i = 0; i < gSubModulePerBankRow; i++ ) {

                for( int j = 1; j < gPixelPerSubModuleRow; j++ ) {

                    e.Graphics.DrawLine( new System.Drawing.Pen( System.Drawing.Color.WhiteSmoke, 2f ), new Point( 0, myObject.Height / gSubModulePerBankRow / gPixelPerSubModuleRow * ( i * gPixelPerSubModuleRow + j ) ), new Point( myObject.Width, myObject.Height / gSubModulePerBankRow / gPixelPerSubModuleRow * ( i * gPixelPerSubModuleRow + j ) ) );

                }

            }

            // Outline.
            e.Graphics.DrawLine( new System.Drawing.Pen( System.Drawing.Color.Green, 3f ), new Point( 0, 0 ), new Point( 0, myObject.Height ) );
            e.Graphics.DrawLine( new System.Drawing.Pen( System.Drawing.Color.Green, 3f ), new Point( myObject.Width - 1, 0 ), new Point( myObject.Width - 1, myObject.Height ) );
            e.Graphics.DrawLine( new System.Drawing.Pen( System.Drawing.Color.Green, 3f ), new Point( 0, 0 ), new Point( myObject.Width, 0 ) );
            e.Graphics.DrawLine( new System.Drawing.Pen( System.Drawing.Color.Green, 3f ), new Point( 0, myObject.Height - 1 ), new Point( myObject.Width, myObject.Height - 1 ) );

            // Lines between submodules.
            for( int i = 1; i < gSubModulePerBankCol; i++ ) {

                e.Graphics.DrawLine( new System.Drawing.Pen( System.Drawing.Color.Green, 3f ), new Point( myObject.Width / gSubModulePerBankCol * i, 0 ), new Point( myObject.Width / gSubModulePerBankCol * i, myObject.Height ) );

            }

            for( int i = 1; i < gSubModulePerBankRow; i++ ) {

                e.Graphics.DrawLine( new System.Drawing.Pen( System.Drawing.Color.Green, 3f ), new Point( 0, myObject.Height / gSubModulePerBankRow * i ), new Point( myObject.Width, myObject.Height / gSubModulePerBankRow * i ) );

            }


        }

        #endregion

        #region cmModule_SelectedIndexChanged

        ///This is disabled right now
        /// <summary>
        /// The pixel array changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void cmModule_SelectedIndexChanged(object sender, EventArgs e) {

            if (cmModule.SelectedIndex == 0) {

                gSubModulePerBankRow = 1;
                gSubModulePerBankCol = 1;
                gPixelPerSubModuleRow = 1;
                gPixelPerSubModuleCol = 1;
                gPixelPerRow = 1;
                gPixelPerCol = 1;

                this.Refresh();

            } else {

                string mySelectedItem = cmModule.SelectedItem.ToString();

                int myNumofChannel = 0;

                if (int.TryParse(mySelectedItem, out myNumofChannel)) {

                    switch (myNumofChannel) {

                        case 8:
                            //gSubModulePerBankRow = 1;
                            //gSubModulePerBankCol = 1;
                            break;

                        case 64:
                            gSubModulePerBankRow = 1;
                            gSubModulePerBankCol = 1;
                            gPixelPerRow = 8;
                            gPixelPerCol = 8;
                            break;

                        case 256:
                            gSubModulePerBankRow = 2;
                            gSubModulePerBankCol = 2;
                            gPixelPerRow = 16;
                            gPixelPerCol = 16;
                            break;


                    }


                    gPixelPerSubModuleRow = 8;
                    gPixelPerSubModuleCol = 8;

                    this.Refresh();

                }


            }




        }

        #endregion

        #region pcBoxPixel_MouseMove

        /// <summary>
        /// When the mouse moves over the pixel color display filed, this will based 
        /// on mouse position and show the pixel information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void pcBoxPixel_MouseMove(object sender, MouseEventArgs e) {

            if (gEventDataInfo.Count() > 0) {

                PictureBox myObject = (PictureBox)sender;

                float myEnergyResolution = 0f;
                float myFittingPeak = 0f;

                int myPixelPosition = 0;

                int myPixelWidth = myObject.ClientSize.Width / gPixelPerRow;
                int myPixelHeight = myObject.ClientSize.Height / gPixelPerCol;

                int myPixelIndexX = (e.X) / myPixelWidth;

                int myPixelIndexY = (e.Y) / myPixelHeight;

                int myPixelNo = myPixelIndexY * gPixelPerCol + myPixelIndexX;

                if (gFitting != null) {

                    cFittingParameters myFittingParameters = new cFittingParameters();

                    if (gFitting.gDictFittingParameters.TryGetValue(myPixelNo, out myFittingParameters)) {

                        if (myFittingParameters.mStatus == (int)cFittingParameters.eStatus.OK) {

                            myEnergyResolution = myFittingParameters.mEnergyResolution;

                            myFittingPeak = myFittingParameters.mG2Center;

                        }

                    }

                }
                //Pixel Number
                
                txtPixelAndCount.Text = myPixelNo.ToString() + " , " + gFitting.gCountCnt[myPixelNo].ToString() +
                                    " , " + myEnergyResolution.ToString("0.0#") + " , " + myFittingPeak.ToString("0.0#");


            }

        }

        #endregion

        #endregion

        #region LoadQualifiedEnergyResolutionFile

        public int LoadQualifiedEnergyResolutionFile(string pFilePath) {

            int myStatus = -1;

            int myPixelId = -1;

            float myPixelResolutionLowValue = -1.0f;

            float myPixelResolutionHighValue = -1.0f;

            System.IO.FileStream myFileStream;

            myFileStream = new System.IO.FileStream ( pFilePath, System.IO.FileMode.Open, System.IO.FileAccess.Read );

            StreamReader myReader = new StreamReader ( myFileStream );

            string myContent = myReader.ReadToEnd ( );

            //Get all lines
            string[] mySeperatedData = Regex.Split ( myContent, "\n" );

            //Get rid of first line because it is used for version track
            //The second line 
            for (int i = 2; i < mySeperatedData.Count ( ); i++) {

                string[] myData = mySeperatedData[i].Split (',');

                if (myData.Count ( ) >= 3) { 
                
                    //Get the value, the first one is ID
                    //the second one is the min 
                    //the third one is the max
                    if (int.TryParse ( myData[0], out myPixelId )) {

                        if (float.TryParse ( myData[1], out myPixelResolutionLowValue ) && float.TryParse ( myData[2], out myPixelResolutionHighValue )) {

                            if (!gEnergyResolutionQualifiedLevel.ContainsKey ( myPixelId )) {

                                cEnergyResolutionQualified myPixelQualifiedLevel = new cEnergyResolutionQualified ( );

                                myPixelQualifiedLevel.mEnergyResolutionMin = myPixelResolutionLowValue;
                                myPixelQualifiedLevel.mEnergyResolutionMax = myPixelResolutionHighValue;

                                gEnergyResolutionQualifiedLevel.Add ( myPixelId, myPixelQualifiedLevel );

                            
                            }
                        
                        }
                    
                    }

                }

            }

            return myStatus;        
        
        }

        #endregion

        #region LoadQualifiedEnergyCountFile

        public int LoadQualifiedEnergyCountFile(string pFilePath) {

            int myStatus = -1;

            int myPixelId = -1;

            float myPixelCountLowValue = -1.0f;

            float myPixelCountHighValue = -1.0f;

            System.IO.FileStream myFileStream;

            myFileStream = new System.IO.FileStream ( pFilePath, System.IO.FileMode.Open, System.IO.FileAccess.Read );

            StreamReader myReader = new StreamReader ( myFileStream );

            string myContent = myReader.ReadToEnd ( );

            //Get all lines
            string[] mySeperatedData = Regex.Split ( myContent, "\n" );

            //Get rid of first line because it is used for version track
            //The second line 
            for (int i = 2; i < mySeperatedData.Count ( ); i++) {

                string[] myData = mySeperatedData[i].Split ( ',' );

                if (myData.Count ( ) >= 3) {

                    //Get the value, the first one is ID
                    //the second one is the min 
                    //the third one is the max
                    if (int.TryParse ( myData[0], out myPixelId )) {

                        if (float.TryParse ( myData[1], out myPixelCountLowValue ) && float.TryParse ( myData[2], out myPixelCountHighValue )) {

                            if (!gEnergyCountQualifiedLevel.ContainsKey ( myPixelId )) {

                                cEnergyCountQualified myPixelQualifiedLevel = new cEnergyCountQualified ( );

                                myPixelQualifiedLevel.mEnergyCountMin = myPixelCountLowValue;
                                myPixelQualifiedLevel.mEnergyCountMax = myPixelCountHighValue;

                                gEnergyCountQualifiedLevel.Add ( myPixelId, myPixelQualifiedLevel );


                            }

                        }

                    }

                }

            }

            return myStatus;        

        }

        #endregion

        #region LoadPixelPositioningFile

        public int LoadPixelPositioningFile(string pFilePath) {

            int myStatus = -1;

            int myPixelId = -1;

            int myPixelXAxisValue = 0;

            int myPixelYAxisValue = 0;

            int myPixelOffsetRange = 0;

            System.IO.FileStream myFileStream;

            myFileStream = new System.IO.FileStream(pFilePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);

            StreamReader myReader = new StreamReader(myFileStream);

            string myContent = myReader.ReadToEnd();

            //Get all lines
            string[] mySeperatedData = Regex.Split(myContent, "\n");

            //Get rid of first line because it is used for version track
            
            //The second line is for total pixel number

            for (int i = 3; i < mySeperatedData.Count(); i++) {

                string[] myData = mySeperatedData[i].Split(',');

                if (myData.Count() >= 4) {

                    //Get the value, the first one is ID
                    //the second one is the X Axis 
                    //the third one is the Y Axis
                    //the forth one is offset range

                    if (int.TryParse(myData[0], out myPixelId)) {

                        if (int.TryParse(myData[1], out myPixelXAxisValue) &&
                            int.TryParse(myData[2], out myPixelYAxisValue) &&
                            int.TryParse(myData[3], out myPixelOffsetRange)) {

                                if ( !gSinglePxielPositionMap.ContainsKey(myPixelId) ) {

                                    cPixelPosition myPixelPositionValue = new cPixelPosition();

                                    myPixelPositionValue.mXAxis = myPixelXAxisValue;
                                    myPixelPositionValue.mYAxis = myPixelYAxisValue;
                                    myPixelPositionValue.mOffsetRange = myPixelOffsetRange;

                                    gSinglePxielPositionMap.Add(myPixelId, myPixelPositionValue);

                            }

                        }

                    }

                }

            }

            return myStatus;

        }

        #endregion

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start ( gLinkWebpage ); //链接的具体内容可动态设置,此链接即为举例
            }
            catch
            {
                //访问链接失败
            }
        }

        public string ExtractTextFromPdf( string path, int mytest ) {

            ITextExtractionStrategy its = new iTextSharp.text.pdf.parser.LocationTextExtractionStrategy( );

            using( iTextSharp.text.pdf.PdfReader reader = new iTextSharp.text.pdf.PdfReader( path ) ) {

                StringBuilder text = new StringBuilder( );

                for( int i = 1; i <= reader.NumberOfPages; i++ ) {
                    string thePage = PdfTextExtractor.GetTextFromPage( reader, i, its );
                    string[] theLines = thePage.Split( '\n' );
                    foreach( var theLine in theLines ) {
                        text.AppendLine( theLine );
                    }
                }
                return text.ToString( );
            }
        }

        public void CopyPages( PdfSharp.Pdf.PdfDocument pFrom, PdfSharp.Pdf.PdfDocument pTo ) {
            
            for( int i = 0; i < pFrom.PageCount; i++ ) {

                pTo.AddPage( pFrom.Pages[i] );
            
            }
        }

        public void CombineTwoPdfs(string pFirstFile, string pSecondFile, string pFinalFile ) {

            using( PdfSharp.Pdf.PdfDocument one = PdfSharp.Pdf.IO.PdfReader.Open( pFirstFile, PdfDocumentOpenMode.Import ) )
            using( PdfSharp.Pdf.PdfDocument two = PdfSharp.Pdf.IO.PdfReader.Open( pSecondFile, PdfDocumentOpenMode.Import ) )
            using( PdfSharp.Pdf.PdfDocument outPdf = new PdfSharp.Pdf.PdfDocument( ) ) {
                CopyPages( one, outPdf );
                CopyPages( two, outPdf );

                outPdf.Save( pFinalFile );
            }
        
        }

        public void GeneratePlot( IList<DataPoint> pDataSeries, string pOutputFile ) {

            using( var myChart = new Chart( ) ) {

                myChart.ChartAreas.Add( new ChartArea( ) );

                var myDataSeries = new Series( );

                myDataSeries.ChartType = SeriesChartType.RangeColumn;


                foreach( var myDataPoint in pDataSeries ) {
                    
                    myDataSeries.Points.Add( myDataPoint );

                }

                myChart.Series.Add( myDataSeries );

                myDataSeries.IsValueShownAsLabel = true;

                myChart.SetBounds( 100, 100, 500, 400 );

                myChart.SaveImage( pOutputFile, ChartImageFormat.Png );

            }

        }

        public void GeneratePlot( IList<DataPoint> pDataSeries, IList<DataPoint> pDataSeries2, string pOutputFile, SeriesChartType pLineType ) {

            using( var myChart = new Chart( ) ) {

                myChart.ChartAreas.Add( new ChartArea( ) );

                var myDataSeries = new Series( );

                myDataSeries.ChartType = pLineType;

                myDataSeries.BorderWidth = 2;

                
                foreach( var myDataPoint in pDataSeries ) {

                    myDataSeries.Points.Add( myDataPoint );

                }

                myChart.Series.Add( myDataSeries );
                

                var myDataSeries2 = new Series( );

                myDataSeries2.ChartType = pLineType;

                myDataSeries2.BorderWidth = 2;

                myDataSeries2.BorderColor = Color.Red;

                foreach( var myDataPoint in pDataSeries2 ) {

                    myDataSeries2.Points.Add( myDataPoint );
                
                }

                myChart.Series.Add( myDataSeries2 );

                if (myChart.Series[1].Points.Count >= 2) {
                 
                    myChart.Series[1].Points[1].Label = myDataSeries2.Points[1].XValue.ToString();

                }

                myDataSeries.IsValueShownAsLabel = false;

                myChart.SetBounds( 100, 100, 200, 200 );

                myChart.SaveImage( pOutputFile, ChartImageFormat.Png );

            }

        }

        public void GeneratePlot( IList<DataPoint> pDataSeries, string pOutputFile, SeriesChartType pLineType ) {

            using( var myChart = new Chart( ) ) {

                myChart.ChartAreas.Add( new ChartArea( ) );

                var myDataSeries = new Series( );

                myDataSeries.ChartType = pLineType;

                myDataSeries.BorderWidth = 2;

                foreach( var myDataPoint in pDataSeries ) {

                    myDataSeries.Points.Add( myDataPoint );

                }

                myChart.Series.Add( myDataSeries );

                myDataSeries.IsValueShownAsLabel = false;

                myChart.SetBounds( 100, 100, 200, 200 );

                myChart.SaveImage( pOutputFile, ChartImageFormat.Png );

            }

        }

        public void GeneratePlot(IList<DataPoint> pDataSeries, string pOutputFile, SeriesChartType pLineType, int pSize) {

            using (var myChart = new Chart()) {

                myChart.ChartAreas.Add(new ChartArea());

                var myDataSeries = new Series();

                myDataSeries.ChartType = pLineType;

                myDataSeries.BorderWidth = 2;

                foreach (var myDataPoint in pDataSeries) {

                    myDataSeries.Points.Add(myDataPoint);

                }

                myChart.Series.Add(myDataSeries);

                myDataSeries.IsValueShownAsLabel = false;

                myChart.SetBounds(80* pSize, 80 * pSize, 150 * pSize, 150*pSize);

                myChart.SaveImage(pOutputFile, ChartImageFormat.Png);

            }

        }

        public void GeneratePlot(IList<DataPoint> pDataSeries, IList<DataPoint> pDataSeries2, string pOutputFile, SeriesChartType pLineType, int pSize) {

            using (var myChart = new Chart()) {

                myChart.ChartAreas.Add(new ChartArea());

                var myDataSeries = new Series();

                myDataSeries.ChartType = pLineType;

                myDataSeries.BorderWidth = 2;


                foreach (var myDataPoint in pDataSeries) {

                    myDataSeries.Points.Add(myDataPoint);

                }

                myChart.Series.Add(myDataSeries);


                var myDataSeries2 = new Series();

                myDataSeries2.ChartType = pLineType;

                myDataSeries2.BorderWidth = 2;

                myDataSeries2.BorderColor = Color.Red;

                foreach (var myDataPoint in pDataSeries2) {

                    myDataSeries2.Points.Add(myDataPoint);

                }

                myChart.Series.Add(myDataSeries2);

                //myDataSeries2.IsValueShownAsLabel = false;

                //myDataSeries.IsVisibleInLegend = true;

                //myChart.Series[1].Points[1].Label = myDataSeries2.Points[1].XValue.ToString() + "," + myDataSeries2.Points[1].YValues[0].ToString();

                if (myChart.Series[1].Points.Count >= 2) {
                 
                    myChart.Series[1].Points[1].Label =  " " + myDataSeries2.Points[1].XValue.ToString() + "(Total: + " + gEnergyArea.ToString("#.0") + ")";

                }

                myChart.Series[0].BackImageTransparentColor = Color.Black;

                myChart.SetBounds(80 * pSize, 80 * pSize, 150 * pSize, 150 * pSize);

                myChart.SaveImage(pOutputFile, ChartImageFormat.Png);

            }

        }


        private void btnMergeReport_Click(object sender, EventArgs e) {

            const string kSufixData = "_Tof_data.pdf";
            const string kSufixCover = "_Tof_cover.pdf";
            const string kSufixHistogram = "_Tof_histogram.png";

            string myDataFileName = "";
            string myCoverFileName = "";
            string myHistogramName = "";

            string myFolderName = "";

            float myRangeMin = 0.0f;
            float myRangeMax = 0.0f;

            int myInRangeCount =0;
            int myUpRangeCount = 0;
            int myDownRangeCount = 0;

            //Give min a very small value to get over write for first time
            float myFinalResultEnergyMin = 65536.0f;
            float myFinalResultEnergyMax = 0.0f;
            float myFinalResultEnergyAvg = 0.0f;

            float myFinalResultResolutionMin = 100.0f;
            float myFinalResultResolutionMax = 0.0f;
            float myFinalResultResolutionAvg = 0.0f;

            cReportPDFGenerator myReportGenerator;

            FolderBrowserDialog myMergeFolder = new FolderBrowserDialog( );

            if( gSelectedLanguage == ( int )gLanguageVersion.English ) {

                myMergeFolder.Description = "Source Pdf files folder";

            } else {

                myMergeFolder.Description = "需要合并的源文件文件路径";
            
            }

            try {

                if( myMergeFolder.ShowDialog( ) == DialogResult.OK ) {

                    myFolderName = myMergeFolder.SelectedPath;

                    #region Define the final Report

                    SaveFileDialog mySaveFileDialog = new SaveFileDialog( );

                    if( gSelectedLanguage == ( int )gLanguageVersion.English ) {

                        mySaveFileDialog.Filter = "PDF File|*.pdf";
                        mySaveFileDialog.Title = "Save Merged Result Report";

                    } else {

                        mySaveFileDialog.Filter = "PDF 文件|*.pdf";
                        mySaveFileDialog.Title = "保存最终报告";
                    }

                    mySaveFileDialog.ShowDialog( );

                    if( mySaveFileDialog.FileName != "" ) {

                        // Saves the Image via a FileStream created by the OpenFile method.
                        string myExportFileName = mySaveFileDialog.FileName;

                        #region Get the data

                        myDataFileName = myExportFileName.Replace( ".pdf", kSufixData );

                        myReportGenerator = new cReportPDFGenerator( this, myDataFileName, "TOFTEK", "TOFTEK Report Generator", "Pixel Report", "Pixel Report", "TOFTEK Pixel Report" );

                        myReportGenerator.AddGraph(gReportLogo, "");

                        //Need to decide what data filed it is has 
                        #region Decide what data to include in report

                        if ((gIsIncludeEnergySpectrumInReport == true) &&
                            (gIsIncludeResolutionInReport == false) &&
                            (gIsIncludeEnergyCountInReport == false)) {
                            //A
                            myReportGenerator.CreateFinalResultTable( 2 );


                        } else if ((gIsIncludeEnergySpectrumInReport == false) &&
                                   (gIsIncludeResolutionInReport == true) &&
                                   (gIsIncludeEnergyCountInReport == false)) {
                            //B
                            myReportGenerator.CreateFinalResultTable( 2 );

                        } else if ((gIsIncludeEnergySpectrumInReport == false) &&
                                   (gIsIncludeResolutionInReport == false) &&
                                   (gIsIncludeEnergyCountInReport == true)) {
                            //C
                            myReportGenerator.CreateFinalResultTable( 2 );

                        } else if ((gIsIncludeEnergySpectrumInReport == true) &&
                                   (gIsIncludeResolutionInReport == true) &&
                                   (gIsIncludeEnergyCountInReport == false)) {
                            //AB
                            myReportGenerator.CreateFinalResultTable( 3 );

                        } else if ((gIsIncludeEnergySpectrumInReport == true) &&
                                   (gIsIncludeResolutionInReport == false) &&
                                   (gIsIncludeEnergyCountInReport == true)) {
                            //AC
                            myReportGenerator.CreateFinalResultTable( 3 );

                        } else if ((gIsIncludeEnergySpectrumInReport == false) &&
                                   (gIsIncludeResolutionInReport == true) &&
                                   (gIsIncludeEnergyCountInReport == true)) {
                            //BC
                            myReportGenerator.CreateFinalResultTable( 3 );

                        } else if ((gIsIncludeEnergySpectrumInReport == true) &&
                                   (gIsIncludeResolutionInReport == true) &&
                                   (gIsIncludeEnergyCountInReport == true)) {
                            //ABC
                            myReportGenerator.CreateFinalResultTable( 4 );
                        }

                        #endregion

                        cParsePdfReport myReportParser = new cParsePdfReport( );

                        int myProtocolNum = 0;

                        string[] myReportFiles = Directory.GetFiles( myFolderName );

                        myProtocolNum = myReportFiles.Count( );

                        foreach( string myProtocolFileFullPath in myReportFiles ) {

                            myReportParser.ExtractTextFromPdf( myProtocolFileFullPath );

                            int j = myReportParser.ExtractPixelInfoFromFileWithRandomFormat ( ); //myReportParser.ExtractPixelInfoFromFile( );

                            myReportGenerator.InsertDataToResultTable( myReportParser.gPixelInforForPdf, j, myReportParser.gEnergyMin, myReportParser.gEnergyMax );

                        }

                        myRangeMin = myReportParser.gEnergyMin;
                        myRangeMax = myReportParser.gEnergyMax;

                        myReportGenerator.GetFinalReportParameters( out myInRangeCount, out myUpRangeCount, out myDownRangeCount,
                                        out myFinalResultEnergyMin, out myFinalResultEnergyMax, out myFinalResultEnergyAvg,
                                        out myFinalResultResolutionMin, out myFinalResultResolutionMax, out myFinalResultResolutionAvg );


                        myReportGenerator.SaveResultTable( );

                        myReportGenerator.CreateLookupTable ( 5 );

                        myReportGenerator.InsertLoopupTableToResultTable ( myReportParser.gMaxMinArray );

                        myReportGenerator.SaveLookupTable();

                        myReportGenerator.Close( );

                        #endregion

                        #region Get a histogram

                        IList<System.Windows.Forms.DataVisualization.Charting.DataPoint> myDataSeries = new List<System.Windows.Forms.DataVisualization.Charting.DataPoint>( );

                        //System.Windows.Forms.DataVisualization.Charting.DataPoint myDataPoint1 = new DataPoint( myRangeMin, myDownRangeCount );

                        //myDataSeries.Add( myDataPoint1 );

                        for( int i = 0; i < 10; i++ ) {
                            
                            System.Windows.Forms.DataVisualization.Charting.DataPoint myDataPoint = new DataPoint( (int)(myRangeMin + (i+1)*myReportGenerator.gDelta), 
                                ( float )myReportGenerator.gHistogramDataCount[i] );

                            myDataSeries.Add( myDataPoint );
                        
                        }


                        //System.Windows.Forms.DataVisualization.Charting.DataPoint myDataPoint10 = new DataPoint( myRangeMin + ( 10 + 1 ) * myReportGenerator.gDelta, myUpRangeCount );

                        //myDataSeries.Add( myDataPoint10 );

                        myHistogramName = myExportFileName.Replace( ".pdf", kSufixHistogram );

                        GeneratePlot( myDataSeries, myHistogramName );

                        #endregion

                        #region Define A Cover Page

                        myCoverFileName = myExportFileName.Replace( ".pdf", kSufixCover );

                        cReportPDFGenerator myReportCoverGenerator = new cReportPDFGenerator( this, myCoverFileName, "TOFTEK", "TOFTEK Report Generator", "Pixel Report", "Pixel Report", "TOFTEK Pixel Report" );

                        myReportCoverGenerator.AddGraph(gReportLogo, "");

                        myReportCoverGenerator.CreateCoverPage( 17, "0-23", myRangeMin, myRangeMax, myFinalResultEnergyMin, myFinalResultEnergyMax,
                            myFinalResultEnergyAvg, myFinalResultResolutionMin, myFinalResultResolutionMax, myFinalResultResolutionAvg, myInRangeCount, myUpRangeCount, myDownRangeCount, myHistogramName );


                        myReportCoverGenerator.Close( );

                        #endregion

                        CombineTwoPdfs( myCoverFileName, myDataFileName, myExportFileName );

                        File.Delete( myCoverFileName );
                        File.Delete( myDataFileName );
                        File.Delete( myHistogramName );

                        if( gSelectedLanguage == ( int )gLanguageVersion.Chinese ) {

                            MessageBox.Show( "保存文件成功" );

                        } else {

                            MessageBox.Show( "Save file successfully" );

                        }

                    }

                    #endregion


                }

            } catch( IOException ) {

                gErrorOutput.OutPutErrorMessage( cErrorCode.gkecFileSubError_FileUsedByOthers, "" );

            }


        }

        private void picLightDivideDisplay_MouseDown( object sender, MouseEventArgs e ) {

            gSwitchDisplay = true;


        }

        #region Demo_MouseWheel

        void Demo_MouseWheel(object sender, MouseEventArgs e) {

	        //this.Width += e.Delta;
            if( gSwitchDisplay ) {

                if( gDisplayTracker == 1 ) {

                    BeginInvoke( ( ( Action )( ( ) => picLightDivideDisplay.Visible = true ) ) );
                    picLightDivideDisplay.ImageLocation = gFittingDataFile.Replace( ".npz", "_imShow.png" );
                    picLightDivideDisplay.SizeMode = PictureBoxSizeMode.StretchImage;
                    gDisplayTracker = 0;

                } else if( gDisplayTracker == 0 ) {

                    gDisplayTracker = 1;
                    BeginInvoke(((Action)(() => picLightDivideDisplay.Visible = false)));
                    SetResultOnPixelMap( );

                }

            }

        }

        #endregion

        #region checkResToolStripMenuItem_Click

        private void checkResToolStripMenuItem_Click(object sender, EventArgs e) {

        
        
        }

        #endregion

        #region energyCountRangeToolStripMenuItem_Click

        private void energyCountRangeToolStripMenuItem_Click(object sender, EventArgs e) {


        }

        #endregion

        private void cbQualifiedType_SelectedIndexChanged(object sender, EventArgs e) {

            gSelectedQualifiedType = cbQualifiedType.SelectedIndex;

        }

    }

    #region cCalibrationBuffer

    public class cCalibrationBuffer {

        public string mId = "";

        public float[,] mCalibrationBuffer;

        int mPixelNo = 256;

        int mFiledCount = 2;

        public cCalibrationBuffer(int pPixelNo, int pFiledCount ) {

            mPixelNo = pPixelNo;

            mFiledCount = pFiledCount;

            mCalibrationBuffer = new float[pPixelNo, pFiledCount];

        }

        public void mInitializeCalibrationBuffer(){

            for ( int i = 0; i < mPixelNo; i++ ) {

                mCalibrationBuffer[i, 0] = 1f;
                mCalibrationBuffer[i, 1] = 0f;
            
            }
        
        }

    }

    #endregion

    #region cCommand
    //Command class which is used to construct a command
    public class cCommand {

        public byte mCommandType;

        public byte mCommand;

        public byte[ ] mData = new byte[2];

        public byte[ ] mMessage = new byte[4];

        public UInt16 mUint16Data = 0;

        public cCommand() { 
        
        
        
        }

        public cCommand(byte pCommandType, byte pCommand, byte[] pData) {

            mCommandType = pCommandType;

            mCommand = pCommand;

            mData[0] = pData[0];

            mData[1] = pData[1];

            mMessage[0] = mCommandType;
            mMessage[1] = mCommand;
            mMessage[2] = pData[0];
            mMessage[3] = pData[1];
        
        }



    }

    #endregion

    #region cRawData

    public class cRawData {

        public byte mAddress;

        public int mData;

        public cRawData() { 
        
        
        }

        public cRawData(byte pAddress, byte[] pDataBytes) {

            mAddress = pAddress;

            mData = (int)( ((int)pDataBytes[0] << 8) | (int)pDataBytes[1] ); 

        }

        public cRawData(byte pAddress, int pDataBytes) {

            mAddress = pAddress;

            mData = pDataBytes;

        }
    
    
    
    }



    #endregion

    #region cEnergyDataInfo

    public class cEnergyDataInfo {

        public int mEnergyData = 0;

        public int mEnergyCount = 0;

    }

    #endregion

    #region cPixelInfo
    
    public class cPixelInfo {

        public byte mAddress = 0;

        public bool mIsQualify = false;

        public UInt32 mCurrentCnt = 0;

        public Dictionary<UInt16, UInt32> mEnergyData = new Dictionary<UInt16, UInt32> ( );

    }

    #endregion

    #region cProtocolParameter
    public class cProtocolParameter {

        public const string gkArraySize = "ArraySize";
        public const string gkPixelNoPerRow = "PixelPerRow";
        public const string gkPixelNoPerCol = "PixelPerCol";
        public const string gkTotalEventCount = "TotalEventCount";
        public const string gkSinglePixelEventCount = "SinglePixelEventCount";
        public const string gkTriggerTimePeriod = "TriggerTimePeriod(S)";
        public const string gkTriggerType = "TriggerType";
        public const string gkTriggerValue = "TriggerValue";
        public const string gkCorrectionType = "CorrectionType";
        public const string gkIntegralTime = "Integral Time";
        public const string gkEncodingMode = "Encoding Mode";
        public const string gkADCTriggerThreshold = "ADC Trigger Threshold";
        public const string gkADCRange = "ADC Range";
        public const string gkEnergyDataAnalysisAlgorithm = "Energy Data Analysis Algorithm";
        public const string gkVbias = "Vbias";
        public const string gkVref = "Vref";
        public const string gkFixedXAxis = "FixedXAxis";
        public const string gkFixedYAxis = "FixedYAxis";
        public const string gkSourceType = "SourceType";
        public const string gkFittingLowBand = "FittingLowBand";
        public const string gkFittingUpBand = "FittingUpBand";

    }

    #endregion

    #region cFactoryConfigParameter

    public class cFactoryConfigParameter {

        public const string gkSelectedLanguage = "Language";
        public const string gkLanguageChinese = "简体中文";
        public const string gkLanguageEnglish = "English";
        public const string gkBinSize = "BinSize";
        public const string gkMaxEnergyCountPerPixel = "MaxEnergyCount";
        public const string gkIfIncludeResolutionInReport = "Include Resolution In Report";
        public const string gkIfIncludeEnergySpectrumInReport = "Include Energy Spectrum In Report";
        public const string gkIfIncludeCountInReport = "Include Energy Count In Report";
        public const string gkIfAutoAdjustAbias = "Enable Auto Adjust Vbias";
        public const string gkReportLogoPath = "ReportLogoPath";
        public const string gkReportHeaderENG = "ReportHeaderENG";
        public const string gkReportHeaderCHA = "ReportHeaderCHA";
        public const string gkDefaultQualifiedType = "DefaultQualifiedType";
        public const string gkUseDifferentRangesForPixels = "Use Different Ranges For Pixels";
        public const string gkQualifiedPeakRangeFile = "QualifiedPeakRangeFile";
        public const string gkQualifiedResolutionRangeFile = "QualifiedResolutionRangeFile";

        public const string gkIncludeResolutionGreyPic = "IncludeResolutionGreyPic";
        public const string gkIncludeEnergyGreyPic = "IncludeEnergyGreyPic";
        public const string gkIncludeCountGreyPic = "IsIncludeCountGreyPic";

        public const string gkApplicationName = "ApplicationName";
        public const string gkLinkButtonName = "LinkPageName";
        public const string gkLinkWebpage = "LinkWebPage";
        public const string gkEnablePixelReverse = "EnablePixelReverse";
    
    }

    #endregion

    #region cDataACQState

    public class cDataACQState {

        public const int gkIDLE = 0;
        public const int gkCollectingData = 1;
        public const int gkAnalysisData = 2;
        public const int gkSavingData = 3;
    }

    #endregion

    #region cReportPixelInfo

    public class cReportPixelInfo {

        public bool mIsAvailable;
        public float mResolution;
        public float mEnergyValue;
        public float mTemperature;
        public int mCount;

    }

    #endregion

    #region cEnergyCountQualified

    public class cEnergyCountQualified {

        public float mEnergyCountMin;
        public float mEnergyCountMax;

        public cEnergyCountQualified() { 
        
        
        }
    
    }

    #endregion

    #region cEnergyResolutionQualified

    public class cEnergyResolutionQualified {

        public float mEnergyResolutionMin;
        public float mEnergyResolutionMax;

    }

    #endregion

    #region cPixelPosition

    public class cPixelPosition {

        public int mXAxis;
        public int mYAxis;
        public int mOffsetRange;

    }

    #endregion

}
