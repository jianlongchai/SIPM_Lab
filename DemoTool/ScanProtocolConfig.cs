using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Ionic.Zip;

namespace DemoTool {
    public partial class ScanProtocolConfig : Form {

        public Demo gParentWindow;

        #region Xml Template

        private const string gkDefaultTemplate = "<?xml version='1.0'?>" + 
            "<ScanParamContainer xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema'>" +
            "<ProtocolName>DefaultProtocol</ProtocolName>" +
            "<ProtocolPath></ProtocolPath>" +
            "<ProtocolDate></ProtocolDate>" + 
            "<ParameterList>" +
	        "<ScanParamPair>" + 
            "<key>ArraySize</key>" +
            "<value>256</value>" +
            "</ScanParamPair>" +
	        "<ScanParamPair>" +
            "<key>PixelPerRow</key>" +
            "<value>16</value>" +
            "</ScanParamPair>" +
	        "<ScanParamPair>" +
            "<key>PixelPerCol</key>" +
            "<value>16</value>" +
            "</ScanParamPair>" +
            "<ScanParamPair>" +
            "<key>TriggerType</key>" +
            "<value>TotalEventCountTrigger</value>" +
            "</ScanParamPair>" +
	        "<ScanParamPair>" +
            "<key>TotalEventCount</key>" +
            "<value>1000000</value>" +
            "</ScanParamPair>" +
	        "<ScanParamPair>" +
            "<key>SinglePixelEventCount</key>" +
            "<value>0</value>" +
            "</ScanParamPair>" +
	        "<ScanParamPair>" +
            "<key>TriggerTimePeriod(S)</key>" +
            "<value>0</value>" +
            "</ScanParamPair>" +
	        "<ScanParamPair>" +
            "<key>TriggerValue</key>" +
            "<value>1000000</value>" +
            "</ScanParamPair>" +
	        "<ScanParamPair>" +
            "<key>CorrectionType</key>" +
            "<value>None</value>" +
            "</ScanParamPair>" +
	        "<ScanParamPair>" +
            "<key>Integral Time</key>" +
            "<value>Option_500ns</value>" +
            "</ScanParamPair>" +
            "<ScanParamPair>" +
            "<key>Encoding Mode</key>" +
            "<value>Option_SignalPxiel</value>" +
            "</ScanParamPair>" +
	        "<ScanParamPair>" +
            "<key>ADC Trigger Threshold</key>" +
            "<value>Option_40</value>" +
            "</ScanParamPair>" +
	        "<ScanParamPair>" +
            "<key>ADC Range</key>" +
            "<value>Option_LSBP49</value>" +
            "</ScanParamPair>" +
	        "<ScanParamPair>" +
            "<key>Energy Data Analysis Algorithm</key>" +
            "<value>None</value>" +
            "</ScanParamPair>" +
	        "<ScanParamPair>" +
            "<key>Vbias</key>" +
            "<value>20</value>" +
            "</ScanParamPair>" +
	        "<ScanParamPair>" +
            "<key>Vref</key>" +
            "<value>1</value>" +
            "</ScanParamPair>" +
            "</ParameterList>" +
            "</ScanParamContainer>";
        #endregion

        public void InitializeLanguageVersion(int pLanguageVersion) {

            if (pLanguageVersion == (int)Demo.gLanguageVersion.Chinese) {

                btnPRPALoadProtocol.Text = "加载新协议";
                btnCreateNewProtocol.Text = "新建协议";
                btnRenameProtocol.Text = "重命名协议";
                grpPRPAProtocolParameters.Text = "协议参数";
                lblPRPAArraySize.Text = "阵列大小:";
                lblPRPATriggerType.Text = "扫描结束条件:";
                lblPRPATriggerValue.Text = "参数值:";
                lblDisplayEnergyLine.Text = "开启矫正";
                lblPRRPAIntegralTime.Text = "积分时间(ns):";
                lblPRPAEncodingMode.Text = "编码模式:";
                lblPRPAADCTriggerThreshod.Text = "触发阈值:";
                lblPRPAADCRange.Text = "ADC量程:";
                lblPRPAVbias.Text = "偏置电压(20-30v):";
                lblPRPAVref.Text = "参考电压(0-2v)";
                lblPRPXAxis.Text = "X 轴:";
                lblPRPYAxis.Text = "Y 轴:";
                lblPRRPASourceType.Text = "放射源";


            } else if (pLanguageVersion == (int)Demo.gLanguageVersion.English) {

                btnPRPALoadProtocol.Text = "Import New Protocol";
                btnCreateNewProtocol.Text = "Create New Protocol";
                btnRenameProtocol.Text = "Rename Protocol";
                grpPRPAProtocolParameters.Text = "Protocol Parameters";
                lblPRPAArraySize.Text = "Array Size:";
                lblPRPATriggerType.Text = "Trigger Type:";
                lblPRPATriggerValue.Text = "Trigger Value:";
                lblDisplayEnergyLine.Text = "Enable Correction";
                lblPRRPAIntegralTime.Text = "Intergral Time(ns):";
                lblPRPAEncodingMode.Text = "Encoding Mode:";
                lblPRPAADCTriggerThreshod.Text = "Trigger Threshod:";
                lblPRPAADCRange.Text = "ADC Range:";
                lblPRPAVbias.Text = "Vbias(20-30v):";
                lblPRPAVref.Text = "Vref(0-2v)";
                lblPRPXAxis.Text = "XAxis:";
                lblPRPYAxis.Text = "YAxis:";
            
            }

            if (!gParentWindow.gIsRunDebugMode) {

                btnCreateNewProtocol.Visible = false;
                panelCreateNewProtocol.Visible = false;
                btnRenameProtocol.Visible = false;
                panelRenameProtocol.Visible = false;
                
            }
        
        
        }

        public ScanProtocolConfig() {

            InitializeComponent();
        
        }

        public ScanProtocolConfig(Demo pParentWindows) {

            InitializeComponent();

            gParentWindow = pParentWindows;

            InitializeLanguageVersion(gParentWindow.gSelectedLanguage);

            if (gParentWindow.gDictProtocolScanParameters.Count() > 0) {

                foreach (KeyValuePair<string, cScanParameters> myProtocol in gParentWindow.gDictProtocolScanParameters) {

                    cbboxPRPRProtocolList.Items.Add ( myProtocol.Key );

                }
                cbboxPRPRProtocolList.SelectedIndex = 0;
            
            }
        
        }

        void RefreshProtocolGUI() {

            if (gParentWindow.gDictProtocolScanParameters.Count() > 0) {

                cbboxPRPRProtocolList.Items.Clear();

                foreach (KeyValuePair<string, cScanParameters> myProtocol in gParentWindow.gDictProtocolScanParameters) {

                    cbboxPRPRProtocolList.Items.Add(myProtocol.Key);

                }
                cbboxPRPRProtocolList.SelectedIndex = 0;

            }
        
        }

        void FillProtocolGUIParameter(Dictionary<string, string> pProtocol) {

            string myStringValue = "";

            if (pProtocol.TryGetValue(cProtocolParameter.gkArraySize, out myStringValue)) {

                cbboxArraySize.SelectedText = myStringValue;
            
            }

            if (pProtocol.TryGetValue(cProtocolParameter.gkTriggerType, out myStringValue)) {

                cbboxPRPATriggerType.SelectedText = myStringValue;

            }

            if (pProtocol.TryGetValue(cProtocolParameter.gkTriggerValue, out myStringValue)) {

                txtPRPATriggerValue.SelectedText = myStringValue;

            }

            if (pProtocol.TryGetValue(cProtocolParameter.gkVbias, out myStringValue)) {

                txtPRPAVbias.Text = myStringValue;

            }
    
            if (pProtocol.TryGetValue(cProtocolParameter.gkVref, out myStringValue)) {

                txtPRPAVref.Text = myStringValue;

            }

            if (pProtocol.TryGetValue(cProtocolParameter.gkIntegralTime, out myStringValue)) {

                cbboxPRPAIntegralTime.SelectedText = myStringValue;

            }

            if (pProtocol.TryGetValue(cProtocolParameter.gkEncodingMode, out myStringValue)) {

                cmboxEncodingMode.SelectedText = myStringValue;

            }

            if (pProtocol.TryGetValue(cProtocolParameter.gkADCTriggerThreshold, out myStringValue)) {

                cmboxPRPAADCTriggerThreshod.SelectedText = myStringValue;

            }

            if (pProtocol.TryGetValue(cProtocolParameter.gkADCRange, out myStringValue)) {

                cmboxPRPAADCRange.SelectedText = myStringValue;

            }
            
        
        
        
        }

        void FillProtocolGUIParameter(cScanParameters pScanParameter) {

            cbboxArraySize.Items.Clear();
            cbboxArraySize.Text = "";
            cbboxArraySize.SelectedText = pScanParameter.gPixelNumsPerRow.ToString() + "x" + pScanParameter.gPixelNumsPerCol.ToString() ;

            cbboxPRPATriggerType.SelectedIndex = pScanParameter.gScanTriggerType;

            //First clear the content, otherwise it will keep adding up
            txtPRPATriggerValue.Text = "";

            switch (pScanParameter.gScanTriggerType) { 

                case (int)cScanParameters.eScanTriggerType.TotalEventCountTrigger:
                    txtPRPATriggerValue.Text = pScanParameter.gScanTriggerTotalEventCount.ToString();
                    break;
                case (int)cScanParameters.eScanTriggerType.SinglePixelEventCountTrigger:
                   txtPRPATriggerValue.SelectedText = pScanParameter.gScanTriggerSinglePixelEventCount.ToString();
                    break;
                case (int)cScanParameters.eScanTriggerType.TimeTrigger:
                    txtPRPATriggerValue.SelectedText = pScanParameter.gScanTriggerTimePeriod.ToString();
                    break;

            
            }

            txtPRPAVbias.Text = pScanParameter.gVbias.ToString();
            txtPRPAVref.Text = pScanParameter.gVref.ToString();

            switch (pScanParameter.gIntegralTime) { 
                
                case (int)cScanParameters.eIntegralTimeType.Option_500ns:
                    cbboxPRPAIntegralTime.SelectedIndex = 0;
                    break;
                case (int)cScanParameters.eIntegralTimeType.Option_1000ns:
                    cbboxPRPAIntegralTime.SelectedIndex = 1;
                    break;
                case (int)cScanParameters.eIntegralTimeType.Option_1500ns:
                    cbboxPRPAIntegralTime.SelectedIndex = 2;
                    break;
                case (int)cScanParameters.eIntegralTimeType.Option_2000ns:
                    cbboxPRPAIntegralTime.SelectedIndex = 3;
                    break;
            }

            cmboxEncodingMode.SelectedIndex = pScanParameter.gEncodingMode;

            switch (pScanParameter.gADCTriggerThreshold) { 
                
                case (int)cScanParameters.eADThreshold.Option_40:
                    cmboxPRPAADCTriggerThreshod.SelectedIndex = 0;
                    break;
                case (int)cScanParameters.eADThreshold.Option_80:
                    cmboxPRPAADCTriggerThreshod.SelectedIndex = 1;
                    break;
                case (int)cScanParameters.eADThreshold.Option_160:
                    cmboxPRPAADCTriggerThreshod.SelectedIndex = 2;
                    break;
                case (int)cScanParameters.eADThreshold.Option_320:
                    cmboxPRPAADCTriggerThreshod.SelectedIndex = 3;
                    break;
            }


            cmboxPRPAADCRange.SelectedIndex = pScanParameter.gADCRange;

            txtPRPXAxis.Text = pScanParameter.gXAxis.ToString();
            txtPRPYAxis.Text = pScanParameter.gYAxis.ToString();

            cmboxPRPASourceType.SelectedIndex = pScanParameter.gSourceType;

        }

        private void btnPRPALoadProtocol_Click(object sender, EventArgs e) {

            int myErrorCode = 0;
            string myWarning = "";
            string myTitle = "";
            string myFullFileName = "";
            string mySafeFileName = "";

            bool myIsOverWriteProcol = false;

            if (gParentWindow.gSelectedLanguage == (int)Demo.gLanguageVersion.English) {

                myWarning = "Do you want to overwrite the old protocol files?";
                myTitle = "Overwrite Protocol Comfirmation";

            } else if (gParentWindow.gSelectedLanguage == (int)Demo.gLanguageVersion.Chinese) {

                myWarning = "确认覆盖原协议文件?";
                myTitle = "覆盖协议文件确认";

            }

            DialogResult mySelectionResult = MessageBox.Show(myWarning, myTitle, MessageBoxButtons.YesNoCancel);

            if (mySelectionResult == DialogResult.Cancel) {

                //Don't want to write any files
                myErrorCode = -1;


            } else {

                OpenFileDialog myLoadFileDialog = new OpenFileDialog();

                DialogResult myStatus = myLoadFileDialog.ShowDialog();

                if (mySelectionResult == DialogResult.Yes) {

                    myIsOverWriteProcol = true;


                } else if (mySelectionResult == DialogResult.No) {

                    myIsOverWriteProcol = false;


                }

                if (myStatus != System.Windows.Forms.DialogResult.None) {

                    #region Has a file selection

                    myFullFileName = myLoadFileDialog.FileName;

                    mySafeFileName = myLoadFileDialog.SafeFileName;

                    #region Check Folder First

                    if (System.IO.Directory.Exists(cProgramDirectory.gkTOFTEKProtocolFolder) == false) {

                        System.IO.Directory.CreateDirectory(cProgramDirectory.gkTOFTEKProtocolFolder);

                    }

                    #endregion

                    if (myFullFileName.ToLower().Contains(".zip")) {

                        //Contain zipped files
                        #region Has multiple files in zipped folder

                        using (ZipFile myZipFileHandle = new ZipFile(myFullFileName)) {

                            #region Loop Zip files and copy to folder

                            //Has protocol
                            for (int i = 0; i < myZipFileHandle.Count; i++) {

                                ZipEntry myZipFile = myZipFileHandle[i];
                                //foreach (ZipEntry myZipFile in myZipFileHandle) {

                                if (gParentWindow.gDictProtocolScanParameters.ContainsKey(myZipFile.FileName.Replace(".xml", "")) && (myIsOverWriteProcol == false)) {

                                    //Already has the file and don''t want to overwrite it


                                } else {

                                    if (!myZipFile.IsDirectory) {

                                        //No matter has it or not then overwrite it

                                        myZipFile.FileName = System.IO.Path.GetFileName(myZipFile.FileName);

                                        myZipFile.Extract(cProgramDirectory.gkTOFTEKProtocolFolder, ExtractExistingFileAction.OverwriteSilently);

                                    }
                                }

                            }

                            #endregion

                        }

                        #endregion


                    } else if (myFullFileName.ToLower().Contains(".xml")) {

                        //Conatains one protocol file
                        System.IO.File.Copy(myFullFileName, cProgramDirectory.gkTOFTEKProtocolFolder + mySafeFileName);

                    } else {

                        //Does not support 


                    }

                    #endregion


                } else {

                    myErrorCode = -1;
                    //No file selected


                }



            }

            if (myErrorCode == 0) {

                MessageBox.Show("Update okay");

                RefreshProtocolGUI();
                gParentWindow.FindAllProtocolFiles(cProgramDirectory.gkTOFTEKProtocolFolder);
                gParentWindow.RefreshProtocolGUIList();

            }

        }

        private void btnCreateNewProtocol_Click(object sender, EventArgs e) {

            cScanParameters myNewScanParameter = new cScanParameters();

            XmlDocument myDocument = new XmlDocument();

            myDocument.LoadXml(gkDefaultTemplate);

            myNewScanParameter.gOriginalXmlFile = myDocument;

            NewProtocol myNewProtocol = new NewProtocol(this, gParentWindow.gkProtocolDirectory, myNewScanParameter);

            myNewProtocol.Show();


        }

        private void cbboxPRPRProtocolList_MouseClick(object sender, MouseEventArgs e) {

            RefreshProtocolGUI();

        }

        private void cbboxPRPRProtocolList_SelectedIndexChanged(object sender, EventArgs e) {

            if (cbboxPRPRProtocolList.SelectedItem != null) {

                string mySelectedProtocol = cbboxPRPRProtocolList.SelectedItem.ToString();

                if (mySelectedProtocol.Length > 0) {

                    cScanParameters mySelectedScanParameter = new cScanParameters();
                    //if (gParentWindow.gDictProtocolScanParameters.ContainsKey(mySelectedProtocol)) {

                    if (gParentWindow.gDictProtocolScanParameters.TryGetValue(mySelectedProtocol, out mySelectedScanParameter)) {

                        //Dictionary<string, string> myProtocol = gParentWindow.gProtocolsDict[mySelectedProtocol];
                        //FillProtocolGUIParameter(myProtocol);

                        FillProtocolGUIParameter(mySelectedScanParameter);

                    } else {

                        MessageBox.Show("Protocol does not exist");

                    }

                } else {

                    MessageBox.Show("Nothing selected");

                }

            } else {

                MessageBox.Show("Nothing selected");

            }


        }


    }
}
