using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace DemoTool {

    public partial class EnergyDiagram :Form {

        const string gkFormNamePrefix = "Energy Spectrum";

        Demo gParentWindow;

        int gPixelNo = 0;

        int gBinSize = 0;

        int gLastEnergyCount = 0;

        int gFixedXValue = 16384;

        private static bool[] gCleanFlag =  new bool[4096];
        private static bool[] gFittingFlag = new bool[4096];

        public bool gFirstOpenLoad = true;

        //This is raw data graph data
        public List<float> gXData = new List<float>();
        public List<float> gYData = new List<float>();

        //This fitting final result graph data
        public List<float> gFittingXData = new List<float>();
        public List<float> gFittingYData = new List<float>();

        //This fitting G1 graph data
        public List<float> gFittingG1XData = new List<float>();
        public List<float> gFittingG1YData = new List<float>();

        //This fitting G2 graph data
        public List<float> gFittingG2XData = new List<float>();
        public List<float> gFittingG2YData = new List<float>();

        //This fitting G3 graph data
        public List<float> gFittingG3XData = new List<float>();
        public List<float> gFittingG3YData = new List<float>();

        //This fitting linear data
        public List<float> gFittingLinearXData = new List<float>();
        public List<float> gFittingLinearYData = new List<float>();

        //This is peak point
        public List<float> gFittingPeakXData = new List<float>( );
        public List<float> gFittingPeakYData = new List<float>( );

        public float gG2PeakXValue = 0f;
        public float gG2PeakYValue = 0f;

        float myXValue = 0f;
        float myYValue = 0f;
        float myYG1Value = 0f;
        float myYG2Value = 0f;
        float myYG3Value = 0f;
        float myYLinearValue = 0f;
        float myG2Peak = 0f;
        float myG2PeakXValue = 0f;

        int gPixelPosition = 0;

        float gCalibratedSlope = 1.0f;

        float gCalibratedOffset = 0.0f;

        public List<cRawData> gTempOfflineFittingData = new List<cRawData>();

        public System.Windows.Forms.Timer gRefreshTimer = new System.Windows.Forms.Timer();

        public void InitializeLanguage( int pLanguage ) {

            if( pLanguage == (int)Demo.gLanguageVersion.Chinese ) {

                this.Text = "能谱图";
                lblPixelNoLabel.Text = "像素序号";
                lblPixelTotalEcentCounts.Text = "事件数量";
                lblEnergyResolution.Text = "能量分辨率";
                lblFittedPeak.Text = "拟合峰值";
                btnCloseAll.Text = "关闭所有";
                btnSetPeak.Text = "设置峰值";
                zGraph1.m_SyStitle = "能谱图";
                zGraph1.m_SySnameX = "X(能量)";
                zGraph1.m_SySnameY = "Y(数量)";


            } else { 
            
            
            
            }
        
        
        
        
        
        }

        public EnergyDiagram(Demo pParenetWindow, int pPixelNo) {

            gParentWindow = pParenetWindow;

            gCleanFlag[pPixelNo] = false;

            gFittingFlag[gPixelNo] = false;

            gPixelPosition = gParentWindow.gPixelNumToPositionMap[pPixelNo];

            gCalibratedSlope = gParentWindow.gSinglePixelCalibrationBuffer.mCalibrationBuffer[gPixelPosition, 0];

            gCalibratedOffset = gParentWindow.gSinglePixelCalibrationBuffer.mCalibrationBuffer[gPixelPosition, 1];

            string myFormName = gkFormNamePrefix + "_" + pPixelNo.ToString();

            InitializeComponent();

            InitializeLanguage( gParentWindow.gSelectedLanguage );

            try {

                if (gParentWindow.gSelectedScanParametersForDataAcq != null) {

                    zGraph1.m_fXEndSYS = gParentWindow.gSelectedScanParametersForDataAcq.gXAxis;

                } else {

                    zGraph1.m_fXEndSYS = gFixedXValue;

                }

            } catch (NullReferenceException) {

                zGraph1.m_fXEndSYS = gFixedXValue;
            
            }

            this.Name = myFormName;

            btnSetPeak.Visible = true;

            #region Chart

            gXData.Clear();
            gYData.Clear();

            gFittingXData.Clear();
            gFittingYData.Clear();

            gFittingG1XData.Clear();
            gFittingG1YData.Clear();

            gFittingG2XData.Clear();
            gFittingG2YData.Clear();

            gFittingG3XData.Clear();
            gFittingG3YData.Clear();

            gFittingLinearXData.Clear();
            gFittingLinearYData.Clear();

            gFittingPeakXData.Clear( );
            gFittingPeakYData.Clear( );

            zGraph1.f_ClearAllPix();
            zGraph1.f_reXY();
            zGraph1.f_LoadOnePix(ref gXData, ref gYData, Color.Yellow, 3);

            //Don't display these two line
            //zGraph1.f_AddPix(ref gFittingXData, ref gFittingYData, Color.AliceBlue, 2);
            //zGraph1.f_AddPix(ref gFittingG1XData, ref gFittingG1YData, Color.LightBlue, 1);
            
            zGraph1.f_AddPix(ref gFittingG2XData, ref gFittingG2YData, Color.LightBlue, 1);
            zGraph1.f_AddPix(ref gFittingG3XData, ref gFittingG3YData, Color.LightBlue, 1);
            
            //Get rid of linear line
            //zGraph1.f_AddPix(ref gFittingLinearXData, ref gFittingLinearYData, Color.LightBlue, 1);

            zGraph1.f_AddPix( ref gFittingPeakXData, ref gFittingPeakYData, Color.OrangeRed, 1 );

            #endregion

            lblPixelNoValue.Text = pPixelNo.ToString();

            gPixelNo = pPixelNo;

            gBinSize = gParentWindow.gBinSize;

            gLastEnergyCount = 0;

            lblPixelTotalEcentCountsValue.Text = gLastEnergyCount.ToString();

            gRefreshTimer.Tick += new EventHandler(RefreshTimerEventProcess);
            gRefreshTimer.Interval = 500;
            gRefreshTimer.Start();

        }

        public EnergyDiagram(Demo pParenetWindow, bool pDisplayEnergy, int pPixelNo) {

            bool myIsAlreadyOpen = false;

            string myFormName = gkFormNamePrefix + "_" + pPixelNo.ToString();

            this.Name = myFormName;

            for (int i = Application.OpenForms.Count - 1; i >= 0; i--) {

                if (Application.OpenForms[i].Name.ToLower() == myFormName.ToLower()) {

                    myIsAlreadyOpen = true;

                    Application.OpenForms[i].Focus();

                }

            }

            if (myIsAlreadyOpen == false) {
             
                InitializeComponent();

                gParentWindow = pParenetWindow;

                lblPixelNoValue.Text = pPixelNo.ToString();

            }

        }

        private void RefreshTimerEventProcess( Object pObject, EventArgs pEventArgs ) {

            gRefreshTimer.Stop();

            try {

                
                //Need refresh the parameters when restart a data ACQ
                //There are two methods to do this
                //1. Use the timer check the flag
                //2. Use the function call to clear the flag
                #region Used to clear graph when a new ACQ Start

                if (gParentWindow.gRefreshAllEnergyDiagram && (gCleanFlag[gPixelNo] == false)) {

                    gCleanFlag[gPixelNo] = true;

                    //Display raw data related
                    #region Clear Raw Data Display

                    gLastEnergyCount = 0;

                    //Update Count
                    lblPixelTotalEcentCountsValue.Text = "0";

                    gXData.Clear();
                    gYData.Clear();
                    #endregion

                    //Clear fitting parameters
                    #region Clear Fitting Parameters

                    gFittingXData.Clear();
                    gFittingYData.Clear();

                    gFittingG1XData.Clear();
                    gFittingG1YData.Clear();

                    gFittingG2XData.Clear();
                    gFittingG2YData.Clear();

                    gFittingG3XData.Clear();
                    gFittingG3YData.Clear();

                    gFittingLinearXData.Clear();
                    gFittingLinearYData.Clear();

                    gFittingPeakXData.Clear( );
                    gFittingPeakYData.Clear( );

                    #endregion

                    zGraph1.f_Refresh();

                }

                #endregion

                if( (( int )gParentWindow.gSelectedScanParametersForDataAcq.gSourceType < ( int )cScanParameters.eSourceType.LightShare )  ) {
                    
                    //Only there is new data point, then refresh the graph
                    #region Disable Raw Data

                    if( gLastEnergyCount != gParentWindow.gEventDataInfo[gPixelNo].Count ) {

                        //This is used to track how many data point is displayed
                        int myDisplayedCount = 0;

                        //Get total count 
                        gLastEnergyCount = gParentWindow.gEventDataInfo[gPixelNo].Count;

                        //Update Count
                        lblPixelTotalEcentCountsValue.Text = gLastEnergyCount.ToString( );

                        gXData.Clear( );
                        gYData.Clear( );

                        for( int myIndex = 0; myDisplayedCount < gLastEnergyCount; myIndex++ ) {

                            List<cRawData> mySatisfiedRange = gParentWindow.gEventDataInfo[gPixelNo].FindAll( cRawData => ( cRawData.mData >= gBinSize * myIndex ) && ( cRawData.mData < gBinSize * ( myIndex + 1 ) ) );

                            myDisplayedCount += mySatisfiedRange.Count;

                            //This might solve the tail problem
                            if( ( ( gBinSize * myIndex + gBinSize / 2 ) > 50 ) && ( mySatisfiedRange.Count > 1 ) ) {

                                gXData.Add( gBinSize * myIndex + gBinSize / 2 );

                                gYData.Add( ( float )mySatisfiedRange.Count );

                            } else if( ( gBinSize * myIndex + gBinSize / 2 ) < 50 ) {

                                gXData.Add( gBinSize * myIndex + gBinSize / 2 );

                                gYData.Add( ( float )mySatisfiedRange.Count );

                            }

                        }

                        zGraph1.f_Refresh( );


                    }

                    #endregion

                } else {
                    
                    //Usally no raw data to display
                    
                }
                    
                #region Fitting

                if (gParentWindow.gIsAutoFitting) {

                    if (gParentWindow.gIsFittingDone && (gFittingFlag[gPixelNo] == false)) {

                        gFittingFlag[gPixelNo] = true;

                        if (gParentWindow.gFitting != null) {

                            cFittingParameters myFittingParameters = new cFittingParameters();

                            if (gParentWindow.gFitting.gDictFittingParameters.TryGetValue(gPixelNo, out myFittingParameters)) {

                                if (myFittingParameters.mStatus == (int)cFittingParameters.eStatus.OK) {

                                    #region Clear Fitting Data List

                                    gFittingXData.Clear();
                                    gFittingYData.Clear();

                                    gFittingG1XData.Clear();
                                    gFittingG1YData.Clear();

                                    gFittingG2XData.Clear();
                                    gFittingG2YData.Clear();

                                    gFittingG3XData.Clear();
                                    gFittingG3YData.Clear();

                                    gFittingLinearXData.Clear();
                                    gFittingLinearYData.Clear();

                                    gFittingPeakXData.Clear( );
                                    gFittingPeakYData.Clear( );

                                    #endregion

                                    #region Draw Light Share Raw Data

                                    if( ( ( int )gParentWindow.gSelectedScanParametersForDataAcq.gSourceType >= ( int )cScanParameters.eSourceType.LightShare) ) {

                                        gXData.Clear( );
                                        gYData.Clear( );

                                        UInt32 myTotalCount = 0;

                                        foreach( cRawData myData in gParentWindow.gEventDataInfo[gPixelNo] ) {

                                            /*
                                            if (myData.mAddress <= 254) {

                                                gXData.Add(myData.mAddress * 64);

                                                gYData.Add((float)myData.mData);

                                                myTotalCount += (UInt32)myData.mData;

                                                //In case the slope is 2, it will draw another line
                                                if (myData.mAddress == 254) {

                                                    break;
                                                
                                                }

                                            } else  {

                                                break;
                                            
                                            }
                                            */
                                             
                                            gXData.Add(myData.mAddress * 64);

                                            gYData.Add((float)myData.mData);

                                            myTotalCount += (UInt32)myData.mData;


                                        }

                                        lblPixelTotalEcentCountsValue.Text = myTotalCount.ToString( );

                                        //zGraph1.f_Refresh( );

                                    }

                                    #endregion

                                    #region Draw Fitting Line

                                    gG2PeakYValue = 0f;
                                    gG2PeakXValue = 0f;

                                    gBinSize = 64;

                                    for (int myIndex = 0; myIndex < 16384 / gBinSize; myIndex++) {

                                        myXValue = (float)(myIndex * gBinSize + gBinSize / 2.0);

                                        gParentWindow.gFitting.FillFittingFormula(myFittingParameters, myXValue, out myYValue, out myYG1Value, out myYG2Value, out myYG3Value, out myYLinearValue, out myG2Peak, out myG2PeakXValue);

                                        #region Add Fitting X Value

                                        /*
                                        if (gParentWindow.gSelectedScanParametersForDataAcq.gSourceType == (int)cScanParameters.eSourceType.LightShareSiglePixel) {

                                            //beause don't change the gause formula then can only do this
                                            //This is used to solve the problem that the single pixel raw data is calibarted
                                            //But the fitting is still not using calibrated value
                                            myXValue = (float)((myIndex * gBinSize + gBinSize / 2.0) * gCalibratedSlope + gCalibratedOffset);

                                        }
                                        */

                                        //gFittingXData.Add(myXValue);
                                        //gFittingG1XData.Add(myXValue);
                                        gFittingG2XData.Add(myXValue);
                                        //gFittingG3XData.Add(myXValue);
                                        //gFittingLinearXData.Add(myXValue);

                                        if( myYG2Value > gG2PeakYValue ) {

                                            gG2PeakXValue = myXValue;
                                            gG2PeakYValue = myYG2Value;
                                        
                                        }

                                        #endregion

                                        #region Zero negtive value

                                        if (myYValue < 0) {

                                            myYValue = 0f;

                                        }

                                        if (myYG1Value < 0) {

                                            myYG1Value = 0f;

                                        }

                                        if (myYG2Value < 0) {

                                            myYG2Value = 0f;

                                        }

                                        if (myYG3Value < 0) {

                                            myYG3Value = 0f;

                                        }

                                        if (myYLinearValue < 0) {

                                            myYLinearValue = 0f;

                                        }

                                        #endregion 

                                        #region Adding Fitting Y Value 

                                        //gFittingYData.Add(myYValue);
                                        //gFittingG1YData.Add(myYG1Value);
                                        gFittingG2YData.Add(myYG2Value);
                                        //gFittingG3YData.Add(myYG3Value);
                                        //gFittingLinearYData.Add(myYLinearValue);

                                        #endregion

                                    }


                                    zGraph1.f_reXY( );

                                    gParentWindow.gFitting.gPixelRawDataPeak[gPixelNo] = gG2PeakXValue;

                                    for( int i = 0; i < ( int )zGraph1._fYEnd; i++ ) {

                                        gFittingPeakXData.Add( gG2PeakXValue );
                                        gFittingPeakYData.Add( i );

                                    }

                                    //zGraph1.f_Refresh( );

                                    lblFittingPeakValue.Text = myFittingParameters.mG2Center.ToString("0.0#");
                                    lblEnergyResolutionValue.Text = myFittingParameters.mEnergyResolution.ToString("0.0#") + "%";
                                  
                                    lblG2CenterError.Text = myFittingParameters.mG2Error.ToString("+/- 0.0#");

                                    #endregion

                                    zGraph1.f_Refresh();

                                } else {

                                    gParentWindow.gErrorOutput.OutPutErrorMessage(cErrorCode.gkecFitting_ExceptionWhenFitting , "" );

                                }


                            } else {

                                //MessageBox.Show("No enough correct data for fitting");
                                gParentWindow.gErrorOutput.OutPutErrorMessage( cErrorCode.gkecFitting_NoEnoughCorrectData, "" );


                            }

                        } else {

                            gParentWindow.gErrorOutput.OutPutErrorMessage( cErrorCode.gkecFitting_NoFittingResult, "" );


                        }

                    }

                }

                #endregion

            } catch { 
            
            
            
            }
            
            gRefreshTimer.Start();

        }

        private void EnergyDiagram_FormClosing(object sender, FormClosingEventArgs e) {

            gRefreshTimer.Stop();

        }

        #region Event 

        #region btnCloseAll_Click

        private void btnCloseAll_Click(object sender, EventArgs e) {

            for (int i = Application.OpenForms.Count - 1; i >= 0; i--) {

                if (Application.OpenForms[i].Name.ToLower().StartsWith(gkFormNamePrefix.ToLower())) {

                    Application.OpenForms[i].Close();

                }
            
            }

        }

        #endregion

        #region btnFitting_Click

        private void btnFitting_Click(object sender, EventArgs e) {

            #region Only For Debug
            /*
            string myFileName = "";

            #region Check if it has the real time data

            if (gParentWindow.gEventDataInfo[gPixelNo].Count <= 0 ) {
            
                string myWarning = "";
                string myTitle = "";

                #region Warning Windows Setup

                if (gParentWindow.gSelectedLanguage == (int)Demo.gLanguageVersion.English) {
                    
                    myWarning = "No data for fitting, want load from file?";
                    myTitle = "Fitting Comfirmation";

                } else if(gParentWindow.gSelectedLanguage == (int)Demo.gLanguageVersion.Chinese){
                    
                    myWarning = "没有Fitting数据,确认从文件中读取?";
                    myTitle = "Fitting确认";

                }

                #endregion

                #region Show Warning Windows

                if (MessageBox.Show(myWarning, myTitle, MessageBoxButtons.YesNo) == DialogResult.Yes) {

                    //Right now there is no data acqusition for this pixel yet 
                    //try to load from a file
                    OpenFileDialog myLoadFileDialog = new OpenFileDialog();

                    DialogResult myStatus = myLoadFileDialog.ShowDialog();

                    if (myStatus != DialogResult.None) {

                        string myLineContent = "";
                        byte myAddress = 0;
                        myFileName = myLoadFileDialog.FileName;
                        int myReadDataCount = 0;

                        int myDisplayedCount = 0;

                        #region Open and read file

                        System.IO.FileStream myFileHandler = new FileStream(myFileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                        StreamReader myStreamRead = new StreamReader(myFileHandler, System.Text.Encoding.UTF8);

                        while (true) {

                            #region Loop the file

                            myLineContent = myStreamRead.ReadLine();

                            #region Check the content

                            if ((myLineContent != null) && (myLineContent.Length > 0)) {

                                string[] myReadDataLine = myLineContent.Split(',');

                                #region Parse the line contennt

                                if (myReadDataLine.Length == 2) {

                                    if (byte.TryParse(myReadDataLine[0], out myAddress)) {

                                        //if (myAddress == gPixelNo) {

                                            cRawData myRawData = new cRawData(myAddress, Convert.ToInt16(myReadDataLine[1]));

                                            gTempOfflineFittingData.Add(myRawData);

                                        //} else {

                                            //break;

                                        //}

                                    }

                                }

                                #endregion

                            } else {

                                //Read the end of file
                                myStreamRead.Close();
                                myFileHandler.Close();

                                break;

                            }

                            #endregion

                            #endregion

                        } //while(true)



                        #endregion

                        System.IO.FileStream myBinFileHandler = new FileStream(myFileName.Replace(".CSV", ".npz"), System.IO.FileMode.CreateNew, System.IO.FileAccess.Write);
                        BinaryWriter myBinStreamWrite = new BinaryWriter(myBinFileHandler);

                        foreach (cRawData myData in gTempOfflineFittingData) {

                            myBinStreamWrite.Write(myData.mAddress);

                            myBinStreamWrite.Write((short)myData.mData);
                        
                        
                        }

                        myBinFileHandler.Close();
                        myBinStreamWrite.Close();

                        return;

                        #region Paint the read data point

                        myReadDataCount = gTempOfflineFittingData.Count();

                        lblPixelTotalEcentCountsValue.Text = myReadDataCount.ToString();

                        gXData.Clear();
                        gYData.Clear();

                        for (int myIndex = 0; myDisplayedCount < myReadDataCount; myIndex++) {

                            List<cRawData> mySatisfiedRange = gTempOfflineFittingData.FindAll(cRawData => (cRawData.mData >= gBinSize * myIndex) && (cRawData.mData < gBinSize * (myIndex + 1)));

                            myDisplayedCount += mySatisfiedRange.Count;

                            //This might solve the tail problem
                            if (((gBinSize * myIndex + gBinSize / 2) > 50) && (mySatisfiedRange.Count > 1)) {

                                gXData.Add(gBinSize * myIndex + gBinSize / 2);

                                gYData.Add((float)mySatisfiedRange.Count);

                            } else if ((gBinSize * myIndex + gBinSize / 2) < 50) {

                                gXData.Add(gBinSize * myIndex + gBinSize / 2);

                                gYData.Add((float)mySatisfiedRange.Count);

                            }

                        }

                        #endregion

                    }



                } else {

                    return;

                }

                #endregion


            }

            #endregion

            #region Fitting

            Fitting myFitting = new Fitting(gParentWindow);

            myFitting.RunPython("D:\\Development\\Python Code\\csBgoEnFit.py", myFileName);

            if (myFitting != null) {

                cFittingParameters myFittingParameters = new cFittingParameters();

                if (myFitting.gDictFittingParameters.TryGetValue(gPixelNo, out myFittingParameters)) {

                    if (myFittingParameters.mStatus == (int)cFittingParameters.eStatus.OK) {

                        gFittingXData.Clear();
                        gFittingYData.Clear();

                        gFittingG1XData.Clear();
                        gFittingG1YData.Clear();

                        gFittingG2XData.Clear();
                        gFittingG2YData.Clear();

                        gFittingG3XData.Clear();
                        gFittingG3YData.Clear();

                        gFittingLinearXData.Clear();
                        gFittingLinearYData.Clear();

                        float myXValue = 0f;
                        float myYValue = 0f;
                        float myYG1Value = 0f;
                        float myYG2Value = 0f;
                        float myYG3Value = 0f;
                        float myYLinearValue = 0f;

                        for (int myIndex = 0; myIndex < 16384 / gBinSize; myIndex++) {

                            myXValue = (float)(myIndex * gBinSize + gBinSize / 2.0);

                            myFitting.FillFittingFormula(myFittingParameters, myXValue, out myYValue, out myYG1Value, out myYG2Value, out myYG3Value, out myYLinearValue);

                            gFittingXData.Add(myXValue);
                            gFittingG1XData.Add(myXValue);
                            gFittingG2XData.Add(myXValue);
                            gFittingG3XData.Add(myXValue);
                            gFittingLinearXData.Add(myXValue);

                            if (myYValue < 0) {

                                myYValue = 0f;

                            }

                            if (myYG1Value < 0) {

                                myYG1Value = 0f;
                            
                            }

                            if (myYG2Value < 0) {

                                myYG2Value = 0f;

                            }

                            if (myYG3Value < 0) {

                                myYG3Value = 0f;

                            }

                            if (myYLinearValue < 0) {

                                myYLinearValue = 0f;

                            }


                            gFittingYData.Add(myYValue);
                            gFittingG1YData.Add(myYG1Value);
                            gFittingG2YData.Add(myYG2Value);
                            gFittingG3YData.Add(myYG3Value);
                            gFittingLinearYData.Add(myYLinearValue);

                        }

                        zGraph1.f_Refresh();

                        lblEnergyResolutionValue.Text = myFittingParameters.mEnergyResolution.ToString("0.0#") + "%";
                        lblFittingPeakValue.Text = myFittingParameters.mG2Center.ToString("0.0#");

                    } else {

                        MessageBox.Show("Exception when fitting this pixel");
                    
                    }
                    

                } else {

                    MessageBox.Show("No enough correct data for fitting");

                }

            

            } else {

                MessageBox.Show("No fitting yet");
            
            }
            
            #endregion
            */

            #endregion

            //Pop up a message to fill the peak value
            cFittingPeakInput myManualFitting = new cFittingPeakInput ( gParentWindow, gPixelNo );

            myManualFitting.Show ( );
            //lblFittingPeakValue.Text = myFittingParameters.mG2Center.ToString ( "0.0#" );


        }

        #endregion

        private void EnergyDiagram_Load(object sender, EventArgs e) {
            /*
            int x = Screen.PrimaryScreen.WorkingArea.Right - this.Width;
            int y = Screen.PrimaryScreen.WorkingArea.Top + this.Height;

            // Add this for the real edge of the screen:
            //x = 0; // for Left Border or Get the screen Dimension to set it on the Right

            this.Location = new Point(x, y);
            */
            if (gFirstOpenLoad) {

                gFirstOpenLoad = false;

                gCleanFlag[gPixelNo] = false;

                gFittingFlag[gPixelNo] = false;

                //Display raw data related
                #region Clear Raw Data Display

                gLastEnergyCount = 0;

                //Update Count
                lblPixelTotalEcentCountsValue.Text = "0";

                gXData.Clear();
                gYData.Clear();
                #endregion

                //Clear fitting parameters
                #region Clear Fitting Parameters

                gFittingXData.Clear();
                gFittingYData.Clear();

                gFittingG1XData.Clear();
                gFittingG1YData.Clear();

                gFittingG2XData.Clear();
                gFittingG2YData.Clear();

                gFittingG3XData.Clear();
                gFittingG3YData.Clear();

                gFittingLinearXData.Clear();
                gFittingLinearYData.Clear();

                gFittingPeakXData.Clear( );
                gFittingPeakYData.Clear( );

                #endregion

                zGraph1.f_Refresh();

            } else {
            
            
            }


        }

        #endregion

        #region ResetClearEnergyDisplayFlag
        public static void ResetClearEnergyDisplayFlag() {

            for (int i = 0; i < 256; i++) {

                gCleanFlag[i] = false;
            
            }



        }

        #endregion

        #region ResetClearFittingFlag

        public static void ResetClearFittingFlag() {

            for (int i = 0; i < 256; i++) {

                gFittingFlag[i] = false;

            }



        }

        #endregion

        private void zGraph1_Load(object sender, EventArgs e)
        {

        }
    }

}
