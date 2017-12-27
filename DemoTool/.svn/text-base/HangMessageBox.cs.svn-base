using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DemoTool {

    public partial class HangMessageBox : Form {

        Demo gParentWindows;

        public System.Windows.Forms.Timer gUpdateTimer = new System.Windows.Forms.Timer();

        public HangMessageBox() {

            InitializeComponent();
        
        }

        public HangMessageBox(Demo pParentWindows, int pMaxBar) {

            InitializeComponent();

            gParentWindows = pParentWindows;

            prgBar.Minimum = 0;
            prgBar.Maximum = pMaxBar;
            prgBar.Value = 0;

            if (gParentWindows.gSelectedLanguage == (int)Demo.gLanguageVersion.Chinese) {

                this.Text = "注意!";
                txtMessageBox.Text = "请等待，正在保存文件......";


            } else if (gParentWindows.gSelectedLanguage == (int)Demo.gLanguageVersion.English) {

                this.Text = "Warning!";
                txtMessageBox.Text = "Please wait when saving files...";

            }

            gUpdateTimer.Tick += new EventHandler(UpdateTimerEventProcess);
            gUpdateTimer.Interval = 1000;
            gUpdateTimer.Start();

        }

        public HangMessageBox(Demo pParentWindows, string myMessage) {

            InitializeComponent();

            gParentWindows = pParentWindows;

            if (gParentWindows.gSelectedLanguage == (int)Demo.gLanguageVersion.Chinese) {

                this.Text = "注意!";

            } else if (gParentWindows.gSelectedLanguage == (int)Demo.gLanguageVersion.English) {

                this.Text = "Warning!";

            }

            txtMessageBox.Text = myMessage;

            gUpdateTimer.Interval = 1000;
            gUpdateTimer.Tick += new EventHandler(UpdateTimerEventProcess);
            gUpdateTimer.Start();

        }

        private void UpdateTimerEventProcess(Object pObject, EventArgs pEventArgs) {

            gUpdateTimer.Stop();

            if (gParentWindows.gIsInFittingProcess) {

                txtMessageBox.Text = "Prcessing Pixel " + Fitting.gCurrentProcessPixel.ToString() + " ...";

            }
            gUpdateTimer.Start();
        
        
        }

        public void UpdateProgressBar(int pMax, int pValue) {

            //prgBar.Maximum = pMax;
            //gProgressValue = pValue;

            txtMessageBox.Text = pMax.ToString() + "---" + pValue.ToString();
            
        
        }

        public void ShowMessage(string pMessage) {

            if (gParentWindows.gSelectedLanguage == (int)Demo.gLanguageVersion.Chinese) {

                txtMessageBox.Text = "正在保存文件......";


            } else if (gParentWindows.gSelectedLanguage == (int)Demo.gLanguageVersion.English) {

                txtMessageBox.Text = "Saving files...";
            
            }

        }

        private void HangMessageBox_FormClosing(object sender, FormClosingEventArgs e) {

            gUpdateTimer.Stop();

        }

    }
}
