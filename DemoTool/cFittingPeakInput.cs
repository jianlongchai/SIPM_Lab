using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DemoTool {
    public partial class cFittingPeakInput :Form {

        Demo gMainWindows;

        int gPixelNo;

        public cFittingPeakInput( ) {
            InitializeComponent ( );
        }

        public cFittingPeakInput( Demo pMainWindows, int pPixelNo ) {

            gMainWindows = pMainWindows;

            gPixelNo = pPixelNo;

            InitializeComponent ( );

            if ( gMainWindows.gSelectedLanguage == (int)Demo.gLanguageVersion.English ) {
            
            } else {

                btnOK.Text = "确认";
                btnCancel.Text = "取消";

                lblEnergyPeak.Text = "峰值";
            
            }


        }

        private void btnOK_Click( object sender, EventArgs e ) {

            string myPeakValue = txtPeakValue.Text;

            float myPixelPeakValue = 0.0f;

            string myWarning = "";
            string myTitle = ""; 
            string myInputWarning = "";

            if ( gMainWindows.gSelectedLanguage == (int)Demo.gLanguageVersion.English ) {

                myInputWarning = "Please enter a valid value";
                myWarning = "Manual set energy peak value will overwrite this pixel's auto fitting result, please confirm";
                myTitle = "Confirm Peak Modify";

            } else {

                myInputWarning = "请输入正确的峰值";
                myWarning = "手动赋值能谱峰值将覆盖当前像素自动fitting结果，请确认";
                myTitle = "峰值确认";

            }
            
            if ( !float.TryParse ( myPeakValue, out myPixelPeakValue ) ) {

                #region Warning User

                MessageBox.Show ( myInputWarning );

                #endregion

            } else {

                #region Check and input 

                cFittingParameters myFittingParameters = new cFittingParameters ( );

                if ( gMainWindows.gFitting.gDictFittingParameters.TryGetValue ( gPixelNo, out myFittingParameters ) ) {

                    if ( myFittingParameters.mStatus == (int)cFittingParameters.eStatus.OK ) {

                        if ( MessageBox.Show ( myWarning, myTitle, MessageBoxButtons.YesNo ) == DialogResult.Yes ) {

                            gMainWindows.gFitting.gDictFittingParameters[gPixelNo].mStatus = (int)cFittingParameters.eStatus.ManualFit;

                            gMainWindows.gFitting.gDictFittingParameters[gPixelNo].mG2Center = myPixelPeakValue;

                            gMainWindows.gFitting.gDictFittingParameters[gPixelNo].mEnergyResolution = 0f; ;

                        }

                    } else {

                        gMainWindows.gFitting.gDictFittingParameters[gPixelNo].mStatus = (int)cFittingParameters.eStatus.ManualFit;

                        gMainWindows.gFitting.gDictFittingParameters[gPixelNo].mG2Center = myPixelPeakValue;

                        gMainWindows.gFitting.gDictFittingParameters[gPixelNo].mEnergyResolution = 0f;
                    
                    }


                }

                #endregion

                this.Close ( );
            
            }

            

        }

        private void btnCancel_Click( object sender, EventArgs e ) {

            this.Close ( );

        }
    }
}
