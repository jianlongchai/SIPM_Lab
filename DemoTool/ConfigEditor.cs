using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DemoTool.Properties;

namespace DemoTool {
    public partial class ConfigEditor : Form {

        #region Global Variables

        Demo gParenetWindow;

        #endregion

        void InitializeGUI(int pSupportLanguage) {

            if (pSupportLanguage == (int)Demo.gLanguageVersion.Chinese) {

                grpReadOnlyParams.Text = "出厂配置参数(只读)";
                grpWriteParams.Text = "出厂配置参数(可配置)";
                lblMFGName.Text = "生产商";
                lblLanguage.Text = "语言";
                lblMaxEnergyCountPerPixel.Text = "单一晶体最大采样数";
                txtMFGName.Text = "通透光电";
                lblBinSize.Text = "Bin的大小";
                lblEnergyResolution.Text = "能量分辨率";
                grpReportCustomize.Text = "报告定制";
                grpReportCustomize.Text = "报告可选项";
                lblIncludeEnergySpectrum.Text = "能谱图";
                lblIncludeEnergyCount.Text = "能量计数";
                gpConfigVbias.Text = "VBIAS配置";
                lblAutoAdjustVbias.Text = "根据温度自动调节";
                lblDefaultQualifiedType.Text = "默认合格判断条件";
                lblUseDifferentRanges.Text = "不同晶体用不同合格范围";
                lblQualifiedPeakRangeFile.Text = "默认能量合格判断标准文件";
                lblQualifiedResolutionRangeFile.Text = "默认能量分辨率合格判断标准文件";
                lblEnablePixelReverse.Text = "开启翻转功能";
                lblIncludeResolutionGreyPic.Text = "能量分辨率灰度图";
                lblIncludeEnergyGreyPic.Text = "能谱灰度图";
                lblIncludeCountGreyPic.Text = "能量计数灰度图";

            } else if (pSupportLanguage == (int)Demo.gLanguageVersion.English) { 
            
            
            }

            cbLanguage.SelectedIndex = gParenetWindow.gSelectedLanguage;

            cbDefaultQualifiedType.SelectedIndex = gParenetWindow.gSelectedQualifiedType;

            int myTempBuffer = gParenetWindow.gMaxEnergyCount;

            txtMaxEnergyCountPerPixel.Text = myTempBuffer.ToString();

            myTempBuffer = gParenetWindow.gBinSize;

            txtBinSize.Text = myTempBuffer.ToString();

            if( gParenetWindow.gIsIncludeResolutionInReport ) {

                rdEnergyResolution.Image = Resources.GreenOn;

            } else {

                rdEnergyResolution.Image = Resources.GreenOff;

            }

            if( gParenetWindow.gIsIncludeEnergySpectrumInReport ) {

                rdIncludeEnergySpectrum.Image = Resources.GreenOn;

            } else {

                rdIncludeEnergySpectrum.Image = Resources.GreenOff;

            }


            if( gParenetWindow.gIsIncludeEnergyCountInReport ) {

                rdIncludeEnergyCount.Image = Resources.GreenOn;

            } else {

                rdIncludeEnergyCount.Image = Resources.GreenOff;

            }

            if( gParenetWindow.gIsAutoAdjustVbias ) {

                rdEnableAutoVbiasAdjust.Image = Resources.GreenOn;

            } else {

                rdEnableAutoVbiasAdjust.Image = Resources.GreenOff;

            }

            if (gParenetWindow.gIsUseDifferentRangesForPixels) {

                rdUseDifferentRanges.Image = Resources.GreenOn;

            } else {

                rdUseDifferentRanges.Image = Resources.GreenOff;

            }

            if (gParenetWindow.gEnablePixelReverse) {

                cbEnablePixelReverse.SelectedIndex = 0;

            } else {

                cbEnablePixelReverse.SelectedIndex = 1;
            
            }

            if (gParenetWindow.gIsIncludeEnergyGreyPic) {

                rdIncludeEnergyGreyPic.Image = Resources.GreenOn;

            } else {

                rdIncludeEnergyGreyPic.Image = Resources.GreenOff;
            
            }

            
            if (gParenetWindow.gIsIncludeCountGreyPic) {

                rdIncludeCountGreyPic.Image = Resources.GreenOn;

            } else {

                rdIncludeCountGreyPic.Image = Resources.GreenOff;
            
            }

           
            if (gParenetWindow.gIsIncludeResolutionGreyPic) {

                rdIncludeResolutionGreyPic.Image = Resources.GreenOn;
                
            } else {

                rdIncludeResolutionGreyPic.Image = Resources.GreenOff;

            }


            txtQualifiedPeakRangeFile.Text = gParenetWindow.gQualifiedCountFileName;

            txtQualifiedResolutionRangeFile.Text = gParenetWindow.gQualifiedResolutionFileName;

        
        }

        public ConfigEditor() {

            InitializeComponent ( );
        
        }

        public ConfigEditor(Demo pParentWindow, int pSupportLanguage) {

            gParenetWindow = pParentWindow;

            InitializeComponent();

            InitializeGUI(pSupportLanguage);
            
        
        }

        private void cbLanguage_SelectionChangeCommitted(object sender, EventArgs e) {


        }

        private void btnSaveFactoryConfig_Click(object sender, EventArgs e) {

            string mySelectedValue = "";

            bool gIsNeedSaveBack = false;

            bool gIsNeedRestart = false;

            #region Modify Count

            if (txtMaxEnergyCountPerPixel.Text.Length > 0) {

                int myTempValue = 0;

                if (int.TryParse(txtMaxEnergyCountPerPixel.Text, out myTempValue)) {

                    //Only if there is update
                    if (myTempValue != gParenetWindow.gMaxEnergyCount) {
                     
                        gParenetWindow.gMaxEnergyCount = myTempValue;

                        gParenetWindow.AddorModifyConfigParameter( cFactoryConfigParameter.gkMaxEnergyCountPerPixel, myTempValue.ToString( ) );

                        gIsNeedSaveBack = true;

                    }

                }


            }

            #endregion

            #region Modify Bin Size

            if(txtBinSize.Text.Length > 0) {

                int myTempValue = 0;

                if (int.TryParse(txtBinSize.Text, out myTempValue)) {

                    //Only if there is update
                    if (myTempValue != gParenetWindow.gBinSize) {

                        gParenetWindow.gBinSize = myTempValue;

                        gParenetWindow.AddorModifyConfigParameter( cFactoryConfigParameter.gkBinSize, myTempValue.ToString( ) );

                        gIsNeedSaveBack = true;

                    }

                }


            }

            #endregion

            #region Save Report Include Resolution Config

            if( gParenetWindow.gIsIncludeResolutionInReport ) {

                gParenetWindow.AddorModifyConfigParameter( cFactoryConfigParameter.gkIfIncludeResolutionInReport, "yes" );

                gIsNeedSaveBack = true;


            } else {

                gParenetWindow.AddorModifyConfigParameter( cFactoryConfigParameter.gkIfIncludeResolutionInReport, "no" );

                gIsNeedSaveBack = true;

            
            }

            #endregion

            #region Save Report Include Energy Spectrum Config

            if( gParenetWindow.gIsIncludeEnergySpectrumInReport ) {

                gParenetWindow.AddorModifyConfigParameter( cFactoryConfigParameter.gkIfIncludeEnergySpectrumInReport, "yes" );

                gIsNeedSaveBack = true;


            } else {

                gParenetWindow.AddorModifyConfigParameter( cFactoryConfigParameter.gkIfIncludeEnergySpectrumInReport, "no" );

                gIsNeedSaveBack = true;


            }

            #endregion

            #region Save Report Include Energy Count Config

            if( gParenetWindow.gIsIncludeEnergyCountInReport ) {

                gParenetWindow.AddorModifyConfigParameter( cFactoryConfigParameter.gkIfIncludeCountInReport, "yes" );

                gIsNeedSaveBack = true;


            } else {

                gParenetWindow.AddorModifyConfigParameter( cFactoryConfigParameter.gkIfIncludeCountInReport, "no" );

                gIsNeedSaveBack = true;


            }

            #endregion

            #region Save Report Include Energy Count Config

            if( gParenetWindow.gIsAutoAdjustVbias ) {

                gParenetWindow.AddorModifyConfigParameter( cFactoryConfigParameter.gkIfAutoAdjustAbias, "yes" );

                gIsNeedSaveBack = true;


            } else {

                gParenetWindow.AddorModifyConfigParameter( cFactoryConfigParameter.gkIfAutoAdjustAbias, "no" );

                gIsNeedSaveBack = true;


            }

            #endregion

            #region Save Report Include Energy Count Config

            if (gParenetWindow.gIsUseDifferentRangesForPixels) {

                gParenetWindow.AddorModifyConfigParameter ( cFactoryConfigParameter.gkUseDifferentRangesForPixels, "yes" );

                gIsNeedSaveBack = true;


            } else {

                gParenetWindow.AddorModifyConfigParameter ( cFactoryConfigParameter.gkUseDifferentRangesForPixels, "no" );

                gIsNeedSaveBack = true;


            }

            #endregion

            #region Save IncludeResolutionGreyPic

            if (gParenetWindow.gIsIncludeResolutionGreyPic) {

                gParenetWindow.AddorModifyConfigParameter(cFactoryConfigParameter.gkIncludeResolutionGreyPic, "yes");

                gIsNeedSaveBack = true;


            } else {

                gParenetWindow.AddorModifyConfigParameter(cFactoryConfigParameter.gkIncludeResolutionGreyPic, "no");

                gIsNeedSaveBack = true;


            }

            #endregion 

            #region Save IncludeEnergyGreyPic

            if (gParenetWindow.gIsIncludeEnergyGreyPic) {

                gParenetWindow.AddorModifyConfigParameter(cFactoryConfigParameter.gkIncludeEnergyGreyPic, "yes");

                gIsNeedSaveBack = true;


            } else {

                gParenetWindow.AddorModifyConfigParameter(cFactoryConfigParameter.gkIncludeEnergyGreyPic, "no");

                gIsNeedSaveBack = true;


            }

            #endregion 

            #region Save IncludeCountGreyPic

            if (gParenetWindow.gIsIncludeCountGreyPic) {

                gParenetWindow.AddorModifyConfigParameter(cFactoryConfigParameter.gkIncludeCountGreyPic, "yes");

                gIsNeedSaveBack = true;


            } else {

                gParenetWindow.AddorModifyConfigParameter(cFactoryConfigParameter.gkIncludeCountGreyPic, "no");

                gIsNeedSaveBack = true;


            }

            #endregion 

            #region Save Language

            if (cbLanguage.SelectedItem != null) {

                mySelectedValue = cbLanguage.SelectedItem.ToString();

                if(mySelectedValue.Length > 0) {

                    if (gParenetWindow.gDefaultLanguage != mySelectedValue) {

                        gParenetWindow.AddorModifyConfigParameter( cFactoryConfigParameter.gkSelectedLanguage, mySelectedValue );

                        gIsNeedSaveBack = true;

                        gIsNeedRestart = true;

                    }

                } else {
                
                    MessageBox.Show("Nothing need to save");
                
                }

            }

            #endregion

            #region Save Qualified Type

            if(cbDefaultQualifiedType.SelectedIndex >= 0 ) {

                mySelectedValue = cbDefaultQualifiedType.SelectedIndex.ToString();

                if (mySelectedValue.Length > 0) {

                    if (gParenetWindow.gSelectedQualifiedType != cbDefaultQualifiedType.SelectedIndex) {

                        gParenetWindow.AddorModifyConfigParameter(cFactoryConfigParameter.gkDefaultQualifiedType, mySelectedValue);

                        gIsNeedSaveBack = true;

                        //gIsNeedRestart = true;

                    }

                } else {

                    MessageBox.Show("Nothing need to save");

                }

            }

            #endregion

            #region Modify gkQualifiedPeakRangeFile

            if (txtQualifiedPeakRangeFile.Text.Length > 0) {

                if (gParenetWindow.gQualifiedCountFileName != txtQualifiedPeakRangeFile.Text) {

                    gParenetWindow.AddorModifyConfigParameter(cFactoryConfigParameter.gkQualifiedPeakRangeFile, txtQualifiedPeakRangeFile.Text.ToString());

                    gIsNeedSaveBack = true;

                    gIsNeedRestart = true;

                }

            }

            #endregion

            #region Modify gkQualifiedPeakRangeFile

            if (txtQualifiedResolutionRangeFile.Text.Length > 0) {

                if (gParenetWindow.gQualifiedResolutionFileName != txtQualifiedResolutionRangeFile.Text) {

                    gParenetWindow.AddorModifyConfigParameter(cFactoryConfigParameter.gkQualifiedResolutionRangeFile, txtQualifiedResolutionRangeFile.Text.ToString());

                    gIsNeedSaveBack = true;

                    gIsNeedRestart = true;

                }

            }

            #endregion

            #region Modify gEnablePixelReverse

            if (cbEnablePixelReverse.SelectedIndex == 0) {

                gParenetWindow.gEnablePixelReverse = true;

                gParenetWindow.AddorModifyConfigParameter(cFactoryConfigParameter.gkEnablePixelReverse, "YES");

                gIsNeedSaveBack = true;

            } else {

                gParenetWindow.AddorModifyConfigParameter(cFactoryConfigParameter.gkEnablePixelReverse, "NO");

                gIsNeedSaveBack = true;

                gParenetWindow.gEnablePixelReverse = false;

            }

            #endregion

            #region Save back file

            if (gIsNeedSaveBack) {

                gParenetWindow.SaveConfigParameter(gParenetWindow.gOriginConfigDocument, gParenetWindow.gkConfigFilesPath);

            }

            #endregion

            #region Restart Program

            if (gIsNeedRestart) {

                DialogResult myDialogResult = MessageBox.Show("Restart application to apply changes(重启软件来应用设置)?", "Waring(警示)", MessageBoxButtons.YesNo);

                if (myDialogResult == DialogResult.Yes) {

                    System.Diagnostics.Process.Start(Application.ExecutablePath);

                    Environment.Exit(0);

                }

            }

            #endregion


        }

        #region rdEnergyResolution_Click

        private void rdEnergyResolution_Click( object sender, EventArgs e ) {

            if( gParenetWindow.gIsIncludeResolutionInReport ) {

                if( ( gParenetWindow.gIsIncludeEnergyCountInReport == false ) && ( gParenetWindow.gIsIncludeEnergySpectrumInReport == false ) ) {

                    gParenetWindow.gErrorOutput.OutPutErrorMessage( cErrorCode.gkecConfiSubError_CannotDisableAll, "" );

                } else {
                    
                    gParenetWindow.gIsIncludeResolutionInReport = false;
                    rdEnergyResolution.Image = Resources.GreenOff;

                }

            } else {

                gParenetWindow.gIsIncludeResolutionInReport = true;
                rdEnergyResolution.Image = Resources.GreenOn;

            }

        }

        #endregion

        #region rdIncludeEnergySpectrum_Click

        private void rdIncludeEnergySpectrum_Click( object sender, EventArgs e ) {

            if( gParenetWindow.gIsIncludeEnergySpectrumInReport ) {

                if( ( gParenetWindow.gIsIncludeEnergyCountInReport == false ) && ( gParenetWindow.gIsIncludeResolutionInReport == false ) ) {

                    gParenetWindow.gErrorOutput.OutPutErrorMessage( cErrorCode.gkecConfiSubError_CannotDisableAll, "" );

                } else {

                    gParenetWindow.gIsIncludeEnergySpectrumInReport = false;
                    rdIncludeEnergySpectrum.Image = Resources.GreenOff;

                }

            } else {

                gParenetWindow.gIsIncludeEnergySpectrumInReport = true;
                rdIncludeEnergySpectrum.Image = Resources.GreenOn;

            }
            

        }

        #endregion

        #region rdIncludeEnergyCount_Click

        private void rdIncludeEnergyCount_Click( object sender, EventArgs e ) {

            if( gParenetWindow.gIsIncludeEnergyCountInReport ) {

                if( ( gParenetWindow.gIsIncludeResolutionInReport == false ) && ( gParenetWindow.gIsIncludeEnergySpectrumInReport == false ) ) {

                    gParenetWindow.gErrorOutput.OutPutErrorMessage( cErrorCode.gkecConfiSubError_CannotDisableAll, "" );

                } else {
                    gParenetWindow.gIsIncludeEnergyCountInReport = false;
                    rdIncludeEnergyCount.Image = Resources.GreenOff;

                }

            } else {

                gParenetWindow.gIsIncludeEnergyCountInReport = true;
                rdIncludeEnergyCount.Image = Resources.GreenOn;

            }
        }

        #endregion

        #region rdEnableAutoVbiasAdjust_Click

        private void rdEnableAutoVbiasAdjust_Click( object sender, EventArgs e ) {

            if( gParenetWindow.gIsAutoAdjustVbias ) {

                gParenetWindow.gIsAutoAdjustVbias = false;
                rdEnableAutoVbiasAdjust.Image = Resources.GreenOff;

            } else {

                gParenetWindow.gIsAutoAdjustVbias = true;
                rdEnableAutoVbiasAdjust.Image = Resources.GreenOn;

            }

        }

        #endregion

        #region rdIncludeResolutionGreyPic_Click

        private void rdIncludeResolutionGreyPic_Click(object sender, EventArgs e) {

            if (gParenetWindow.gIsIncludeResolutionGreyPic) {

                gParenetWindow.gIsIncludeResolutionGreyPic = false;
                rdIncludeResolutionGreyPic.Image = Resources.GreenOff;

            } else {

                gParenetWindow.gIsIncludeResolutionGreyPic = true;
                rdIncludeResolutionGreyPic.Image = Resources.GreenOn;

            }

        }

        #endregion 

        #region rdIncludeEnergyGreyPic_Click

        private void rdIncludeEnergyGreyPic_Click(object sender, EventArgs e) {

            if (gParenetWindow.gIsIncludeEnergyGreyPic) {

                gParenetWindow.gIsIncludeEnergyGreyPic = false;
                rdIncludeEnergyGreyPic.Image = Resources.GreenOff;

            } else {

                gParenetWindow.gIsIncludeEnergyGreyPic = true;
                rdIncludeEnergyGreyPic.Image = Resources.GreenOn;

            }

        }

        #endregion 

        #region rdIncludeCountGreyPic_Click

        private void rdIncludeCountGreyPic_Click(object sender, EventArgs e) {

            if (gParenetWindow.gIsIncludeCountGreyPic) {

                gParenetWindow.gIsIncludeCountGreyPic = false;
                rdIncludeCountGreyPic.Image = Resources.GreenOff;

            } else {

                gParenetWindow.gIsIncludeCountGreyPic = true;
                rdIncludeCountGreyPic.Image = Resources.GreenOn;

            }

        }

        #endregion

        private void rdUseDifferentRanges_Click(object sender, EventArgs e) {

            if (gParenetWindow.gIsUseDifferentRangesForPixels) {

                gParenetWindow.gIsUseDifferentRangesForPixels = false;
                rdUseDifferentRanges.Image = Resources.GreenOff;

            } else {

                gParenetWindow.gIsUseDifferentRangesForPixels = true;
                rdUseDifferentRanges.Image = Resources.GreenOn;

                #region Reload the files

                if (System.IO.File.Exists(cProgramDirectory.gkQualifiedLevelFolder + cProgramDirectory.gkQualifiedResolutionFileName)) {

                    gParenetWindow.LoadQualifiedEnergyResolutionFile(cProgramDirectory.gkQualifiedLevelFolder + cProgramDirectory.gkQualifiedResolutionFileName);

                } else {

                    gParenetWindow.gErrorOutput.OutPutErrorMessage(cErrorCode.gkecFileSubError_MissSystemFileQuailified, "");

                }

                if (System.IO.File.Exists(cProgramDirectory.gkQualifiedLevelFolder + cProgramDirectory.gkQualifiedCountFileName)) {

                    gParenetWindow.LoadQualifiedEnergyCountFile(cProgramDirectory.gkQualifiedLevelFolder + cProgramDirectory.gkQualifiedCountFileName);

                } else {

                    gParenetWindow.gErrorOutput.OutPutErrorMessage(cErrorCode.gkecFileSubError_MissSystemFileQuailified, "");

                }

                #endregion

            }

        }

        private void cbEnablePixelReverse_SelectionChangeCommitted(object sender, EventArgs e) {

        }

    }
}
