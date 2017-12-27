using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace DemoTool {
    public partial class NewProtocol : Form {

        #region Error Code

        public const int gkecSubSystem = cErrorCode.gkecNewProtocolSubError;

        public const int gkecUnknownError = gkecSubSystem + 1;
        public const int gkecUndefiedParameter = gkecSubSystem + 2;
        public const int gkecInvalidValue = gkecSubSystem + 3;

        #endregion

        #region Global Variables

        ScanProtocolConfig gParenetWindow;

        cScanParameters gScanParameterXmlTemplate = new cScanParameters();

        string gRootDirectory = "";

        #endregion

        #region InitializeLanguageVersion

        public void InitializeLanguageVersion(int pLanguageVersion) {

            if (pLanguageVersion == (int)Demo.gLanguageVersion.Chinese) {

                btnSaveNewProtocol.Text = "保存";
                btnNewProtocolCancel.Text = "取消";
                lblPRPANewProtocolName.Text = "协议名称";

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

                btnSaveNewProtocol.Text = "Save";
                btnNewProtocolCancel.Text = "Cancel";
                lblPRPANewProtocolName.Text = "New Protocol Name";

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
                lblPRRPASourceType.Text = "Soure Type";

            }


        }

        #endregion

        #region FillProtocolGUIParameter

        void FillProtocolGUIParameter(cScanParameters pScanParameter) {

            cbboxArraySize.SelectedText = pScanParameter.gPixelNumsPerRow.ToString() + "x" + pScanParameter.gPixelNumsPerCol.ToString();

            cbboxPRPATriggerType.SelectedIndex = pScanParameter.gScanTriggerType;

            switch (pScanParameter.gScanTriggerType) {

                case (int)cScanParameters.eScanTriggerType.TotalEventCountTrigger:
                    txtPRPATriggerValue.SelectedText = pScanParameter.gScanTriggerTotalEventCount.ToString();
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

        }

        #endregion

        #region Construct

        public NewProtocol() {
        
            InitializeComponent();
        
        }

        public NewProtocol(ScanProtocolConfig pParent, string pRootDirectory, cScanParameters pScanParameter) {

            gParenetWindow = pParent;

            gRootDirectory = pRootDirectory;

            gScanParameterXmlTemplate = pScanParameter;

            InitializeComponent();

            InitializeLanguageVersion(gParenetWindow.gParentWindow.gSelectedLanguage);

            //Use to fill the parameters 
            gScanParameterXmlTemplate.FillScanParameterFromXmlDoc(gScanParameterXmlTemplate.gOriginalXmlFile);

            //Used to fill the GUI using parameters
            FillProtocolGUIParameter(gScanParameterXmlTemplate);

        }

        #endregion

        #region btnSaveNewProtocol_Click
        
        private void btnSaveNewProtocol_Click(object sender, EventArgs e) {

            int myStatus = 0;

            string myNewProtocolName = txtNewProtocolName.Text;

            string myErrorMessage = "";

            string mySelectedItemValue = "";

            if (myNewProtocolName.Length > 0) {

                if (!gParenetWindow.gParentWindow.gDictProtocolScanParameters.ContainsKey(myNewProtocolName)) {

                    #region Write Back Protocol Name/Path/Date

                    gScanParameterXmlTemplate.ModifyProtocolName(myNewProtocolName);
                    gScanParameterXmlTemplate.ModifyProtocolPath(gRootDirectory + myNewProtocolName + ".xml");
                    gScanParameterXmlTemplate.ModifyProtocolDate(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString());

                    #endregion

                    #region Write Back Pixel Per Row & Pixel Per Col

                    //Check if a new value is selected
                    if (cbboxArraySize.SelectedItem != null) {

                        mySelectedItemValue = cbboxArraySize.SelectedItem.ToString();

                        if (mySelectedItemValue.Length <= 0) {

                            //Correct value is selected
                            myErrorMessage += "Please select array size";

                            myStatus = gkecInvalidValue;

                        } else {

                            //The format is rowNo x col
                            //After split the first item will be row and the second will be column
                            string[] myPixelSettings = cbboxArraySize.SelectedItem.ToString().Split('x');

                            if (myPixelSettings.Length != 2) {

                                myErrorMessage += "ArraySize is wrong,";

                            } else {

                                gScanParameterXmlTemplate.ModifyParameter(cProtocolParameter.gkPixelNoPerRow, myPixelSettings[0]);

                                gScanParameterXmlTemplate.ModifyParameter(cProtocolParameter.gkPixelNoPerCol, myPixelSettings[1]);

                            }


                        }

                    } else { 
                    
                        //Will use the default one
                    
                    }

                    #endregion

                    #region Write Back Trigger Type/Value

                    mySelectedItemValue = cbboxPRPATriggerType.SelectedItem.ToString();

                    if (mySelectedItemValue.Length <= 0) {

                        myErrorMessage += "Please select trigger type(请选择触发条件)";

                        myStatus = gkecInvalidValue;

                    } else {

                        string myTriggerValue = txtPRPATriggerValue.Text.ToString();

                        if (mySelectedItemValue.ToLower() == cScanParameters.eScanTriggerType.TotalEventCountTrigger.ToString().ToLower()) {

                            gScanParameterXmlTemplate.ModifyParameter(cProtocolParameter.gkTriggerType, mySelectedItemValue);

                            if (myTriggerValue.Length <= 0) {

                                myErrorMessage += "Please enter trigger value(请添加触发值)";

                                myStatus = gkecInvalidValue;

                            } else {

                                gScanParameterXmlTemplate.ModifyParameter(cProtocolParameter.gkTotalEventCount, myTriggerValue);
                            
                            }

                        } else if (mySelectedItemValue.ToLower() == cScanParameters.eScanTriggerType.SinglePixelEventCountTrigger.ToString().ToLower()) {

                            gScanParameterXmlTemplate.ModifyParameter(cProtocolParameter.gkTriggerType, mySelectedItemValue);

                            if (myTriggerValue.Length <= 0) {

                                myErrorMessage += "Please enter trigger value(请添加触发值)";

                                myStatus = gkecInvalidValue;

                            } else {

                                gScanParameterXmlTemplate.ModifyParameter(cProtocolParameter.gkSinglePixelEventCount, myTriggerValue);

                            }


                        } else if (mySelectedItemValue.ToLower() == cScanParameters.eScanTriggerType.TimeTrigger.ToString().ToLower()) {

                            gScanParameterXmlTemplate.ModifyParameter(cProtocolParameter.gkTriggerType, mySelectedItemValue);

                            if (myTriggerValue.Length <= 0) {

                                myErrorMessage += "Please enter trigger value(请添加触发值)";

                            } else {

                                gScanParameterXmlTemplate.ModifyParameter(cProtocolParameter.gkTriggerTimePeriod, myTriggerValue);

                            }

                        } else if (mySelectedItemValue.ToLower() == cScanParameters.eScanTriggerType.AnalysisResultTrigger.ToString().ToLower()) { 
                        
                        
                        }
                    
                    }

                    #endregion 

                    #region Write Back Integral Time

                    mySelectedItemValue = cbboxPRPAIntegralTime.SelectedItem.ToString();

                    if (mySelectedItemValue.Length <= 0) {

                        myErrorMessage += "Please select integral time(请选择积分时间)";

                        myStatus = gkecInvalidValue;

                    } else {

                        gScanParameterXmlTemplate.ModifyParameter(cProtocolParameter.gkIntegralTime, mySelectedItemValue);                    
                    
                    }

                    #endregion

                    #region Write Back Encoding Mode

                    mySelectedItemValue = cmboxEncodingMode.SelectedItem.ToString();

                    if (mySelectedItemValue.Length <= 0) {

                        myErrorMessage += "Please select encoding mode(请选择编码类型)";

                        myStatus = gkecInvalidValue;

                    } else {

                        gScanParameterXmlTemplate.ModifyParameter(cProtocolParameter.gkEncodingMode, mySelectedItemValue);

                    }

                    #endregion

                    #region Write Back Trigger Threshold

                    mySelectedItemValue = cmboxPRPAADCTriggerThreshod.SelectedItem.ToString();

                    if (mySelectedItemValue.Length <= 0) {

                        myErrorMessage += "Please select AD threshold(请选择阈值设置)";

                        myStatus = gkecInvalidValue;

                    } else {

                        gScanParameterXmlTemplate.ModifyParameter(cProtocolParameter.gkADCTriggerThreshold, mySelectedItemValue);

                    }

                    #endregion

                    #region Write Back AD Range

                    mySelectedItemValue = cmboxPRPAADCRange.SelectedItem.ToString();

                    if (mySelectedItemValue.Length <= 0) {

                        myErrorMessage += "Please select AD range(请选择AD范围设置)";

                        myStatus = gkecInvalidValue;

                    } else {

                        gScanParameterXmlTemplate.ModifyParameter(cProtocolParameter.gkADCRange, mySelectedItemValue);

                    }

                    #endregion

                    #region Write Back Vbias

                    mySelectedItemValue = txtPRPAVbias.Text.ToString();

                    if (mySelectedItemValue.Length <= 0) {

                        myErrorMessage += "Please set Vbias(请设置Vbias值)";

                        myStatus = gkecInvalidValue;

                    } else {

                        gScanParameterXmlTemplate.ModifyParameter(cProtocolParameter.gkVbias, mySelectedItemValue);

                    }

                    #endregion

                    #region Write Back Vref

                    mySelectedItemValue = txtPRPAVref.Text.ToString();

                    if (mySelectedItemValue.Length <= 0) {

                        myErrorMessage += "Please set Vref(请设置Vref值)";

                        myStatus = gkecInvalidValue;

                    } else {

                        gScanParameterXmlTemplate.ModifyParameter(cProtocolParameter.gkVref, mySelectedItemValue);

                    }

                    #endregion

                    gScanParameterXmlTemplate.SaveScanParametersFiles(gScanParameterXmlTemplate.gOriginalXmlFile, gRootDirectory + myNewProtocolName + ".xml");

                    if (myStatus != 0) {

                        MessageBox.Show(myErrorMessage);

                    } else {

                        //Add this to the scan list
                        gParenetWindow.gParentWindow.gDictProtocolScanParameters.Add(myNewProtocolName, gScanParameterXmlTemplate);

                        this.Close();
                    
                    }


                } else {

                    MessageBox.Show("Protocol already exists (协议已存在)");
                
                }


            } else {

                MessageBox.Show("Please type in correct protocol name(请输入正确的协议名称)");
            
            }

        }

        #endregion

    }
}
