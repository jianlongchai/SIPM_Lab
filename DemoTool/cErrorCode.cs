using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DemoTool {
    
    public class cErrorCode {

        #region Gloabl Constants

        public const int gkecDataAcqSubError = 0x01000000;
        public const int gkecScanParameterSubError = 0x02000000;
        public const int gkecNewProtocolSubError = 0x03000000;
        public const int gkecDataExportSubError = 0x04000000;
        public const int gkecFittingSubError = 0x05000000;
        public const int gkecHardwareError = 0x06000000;
        public const int gkecFileSubError = 0x07000000;
        public const int gkecConfiSubError = 0x08000000;
        public const int gkecReportSububError = 0x09000000;

        public enum gkecErrorCode {OK = 0, InvalidParameters = 1, };

        #region SubSystem Error Code

        #region DAQ SubSystem Error Code

        public const int gkecDataAcq_InvalidParameter = gkecDataAcqSubError + 1;
        public const int gkecDataAcq_InvalidProtocol = gkecDataAcqSubError + 2;
        public const int gkecDataAcq_BoxOpen = gkecDataAcqSubError + 3;
        public const int gkecDataAcq_ADCPLLError = gkecDataAcqSubError + 4;

        #endregion

        #region Data Export SubSystem Error Code

        public const int gkecDataExport_FileSavedOK = gkecDataExportSubError;
        //public const int gkecDataExport_FileUsedByOthers = gkecDataExportSubError + 1;
        public const int gkecDataExport_NoFileSelected = gkecDataExportSubError + 2;

        #endregion

        #region Fitting Subsystem Error Code

        public const int gkecFitting_NoFittingResult = gkecFittingSubError + 1;
        public const int gkecFitting_NoEnoughCorrectData = gkecFittingSubError + 2;
        public const int gkecFitting_ExceptionWhenFitting = gkecFittingSubError + 3;
        public const int gkedFitting_PixelCountMissing = gkecFittingSubError + 4;
        public const int gkecFitting_NoCountmap = gkecFittingSubError + 5;
        public const int gkecFitting_PixelNoMatch = gkecFittingSubError + 6;
        
        #endregion

        #region Protocol Error Code


        #endregion

        #region Hardware Error Code

        public const int gkecHardwareConnectionError = gkecHardwareError + 1;

        #endregion

        #region File Sub Error

        public const int gkecFileSubError_FileUsedByOthers = gkecFileSubError + 1;
        public const int gkecFileSubError_MissSystemFile = gkecFileSubError + 2;
        public const int gkecFileSubError_MissSystemFileQuailified = gkecFileSubError + 3;
        public const int gkecFileSubError_MissSystemFileCalibration = gkecFileSubError + 4;

        #endregion

        #region Config Sub Error

        public const int gkecConfiSubError_CannotDisableAll = gkecConfiSubError + 1;

        #endregion

        #region Report Sub Error

        public const int gkecReportNoteToolong =  gkecReportSububError+1;

        #endregion

        #endregion


        #endregion

        #region Global Variables

        public int gSelectedLanguage = (int)Demo.gLanguageVersion.English;

        #endregion

        #region Error Message

        #region Chinese Version

        public const string gkInvalidSelectedProtocol_CHA = "请选择一个正确的扫描协议";
        public const string gkBoxOpen_CHA = "测试箱被打开！";
        public const string gkADCPLLError_CHA = "锁相环信号错误！";
        public const string gkecFileSubError_FileUsedByOthers_CHA = "文件被其他程序占用";
        public const string gkecDataExport_FileSavedOK_CHA = "保存文件成功";
        public const string gkecDataExport_NoFileSelected_CHA = "请选择正确格式的文件";
        public const string gkecHardwareConnectionError_CHA = "请确认硬件连接";
        public const string gkecFitting_NoEnoughCorrectData_CHA = "没有足够的数据供拟合";
        public const string gkecFitting_ExceptionWhenFitting_CHA = "拟合时出现异常";
        public const string gkecFitting_NoFittingResult_CHA = "没有拟合结果";
        public const string gkedFitting_PixelCountMissing_CHA = "错误的晶体数量";
        public const string gkecFitting_NoCountmap_CHA = "未生产正确的计数表";
        public const string gkecFitting_PixelNoMatch_CHA = "晶体数量不符";
        public const string gkecConfiSubError_CannotDisableAll_CHA = "不可禁止所有数据";
        public const string gkecFileSubError_MissSystemFile_CHA = "系统文件缺失";
        public const string gkecFileSubError_MissSystemFileQuailified_CHA = "系统合格范围文件缺失";
        public const string gkecFileSubError_MissSystemFileCalibration_CHA = "系统校准文件缺失";
        public const string gkecReportNoteToolong_CHA = "报告备注过长(每个备注请保持在10个字母内)";

        #endregion

        #region  English Version

        public const string gkInvalidSelectedProtocol_ENG = "Please select a correct protocol";
        public const string gkBoxOpen_ENG = "Detector box is open!";
        public const string gkADCPLLError_ENG = "ADC PLL Error";
        public const string gkecFileSubError_FileUsedByOthers_ENG = "File is openned by other process";
        public const string gkecDataExport_FileSavedOK_ENG = "Save file successfully";
        public const string gkecDataExport_NoFileSelected_ENG= "Please select correct format file";
        public const string gkecHardwareConnectionError_ENG = "Please check hardware connection";
        public const string gkecFitting_NoEnoughCorrectData_ENG = "No enough correct data for fitting";
        public const string gkecFitting_ExceptionWhenFitting_ENG = "Exception when fitting this pixel";
        public const string gkecFitting_NoFittingResult_ENG = "No fitting result";
        public const string gkedFitting_PixelCountMissing_ENG = "Wrong Pixel Count";
        public const string gkecFitting_NoCountmap_ENG = "Not generated count map";
        public const string gkecFitting_PixelNoMatch_ENG = "Pixel number does not match";
        public const string gkecConfiSubError_CannotDisableAll_ENG = "Can not disable all options";
        public const string gkecFileSubError_MissSystemFile_ENG = "System file missing";
        public const string gkecFileSubError_MissSystemFileQuailified_ENG = "System qualified file missing";
        public const string gkecFileSubError_MissSystemFileCalibration_ENG = "System calibration file missing";
        public const string gkecReportNoteToolong_ENG = "Report notes too long(Keep 10 characters for each notes)";

        #endregion

        #endregion

        #region Construct 

        public cErrorCode(int pLanguage) {

            gSelectedLanguage = pLanguage;
        
        }

        #endregion

        #region SearchErrorMessage

        void SearchErrorMessage(int pErrorCode, out string pErrorMessage) {
        
            pErrorMessage = "";

            switch (pErrorCode) { 
            
                case gkecDataAcq_InvalidParameter:

                    pErrorMessage = "";

                    break;

                case gkecDataAcq_InvalidProtocol:

                    if (gSelectedLanguage == (int)Demo.gLanguageVersion.Chinese) {

                        pErrorMessage = gkInvalidSelectedProtocol_CHA;

                    } else if (gSelectedLanguage == (int)Demo.gLanguageVersion.English) {

                        pErrorMessage = gkInvalidSelectedProtocol_ENG;
                    
                    }

                    break;
                case gkecDataAcq_BoxOpen:

                    if (gSelectedLanguage == (int)Demo.gLanguageVersion.Chinese) {

                        pErrorMessage = gkBoxOpen_CHA;

                    } else if (gSelectedLanguage == (int)Demo.gLanguageVersion.English) {

                        pErrorMessage = gkBoxOpen_ENG;

                    }

                    break;
                case gkecDataAcq_ADCPLLError:

                    if (gSelectedLanguage == (int)Demo.gLanguageVersion.Chinese) {

                        pErrorMessage = gkADCPLLError_CHA;

                    } else if (gSelectedLanguage == (int)Demo.gLanguageVersion.English) {

                        pErrorMessage = gkADCPLLError_ENG;

                    }

                    break;

                case gkecFileSubError_FileUsedByOthers:

                    if (gSelectedLanguage == (int)Demo.gLanguageVersion.Chinese) {

                        pErrorMessage = gkecFileSubError_FileUsedByOthers_CHA;

                    } else if (gSelectedLanguage == (int)Demo.gLanguageVersion.English) {

                        pErrorMessage = gkecFileSubError_FileUsedByOthers_ENG;

                    }

                    break;

                case gkecDataExport_FileSavedOK:

                    if (gSelectedLanguage == (int)Demo.gLanguageVersion.Chinese) {

                        pErrorMessage = gkecDataExport_FileSavedOK_CHA;

                    } else if (gSelectedLanguage == (int)Demo.gLanguageVersion.English) {

                        pErrorMessage = gkecDataExport_FileSavedOK_ENG;

                    }

                    break;

                case gkecDataExport_NoFileSelected:

                    if (gSelectedLanguage == (int)Demo.gLanguageVersion.Chinese) {

                        pErrorMessage = gkecDataExport_NoFileSelected_CHA;

                    } else if (gSelectedLanguage == (int)Demo.gLanguageVersion.English) {

                        pErrorMessage = gkecDataExport_NoFileSelected_ENG;

                    }

                    break;

                case gkecHardwareConnectionError:

                    if (gSelectedLanguage == (int)Demo.gLanguageVersion.Chinese) {

                        pErrorMessage = gkecHardwareConnectionError_CHA;

                    } else if (gSelectedLanguage == (int)Demo.gLanguageVersion.English) {

                        pErrorMessage = gkecHardwareConnectionError_ENG;

                    }
                    break;

                case gkecFitting_NoEnoughCorrectData:

                    if (gSelectedLanguage == (int)Demo.gLanguageVersion.Chinese) {

                        pErrorMessage = gkecFitting_NoEnoughCorrectData_CHA;

                    } else if (gSelectedLanguage == (int)Demo.gLanguageVersion.English) {

                        pErrorMessage = gkecFitting_NoEnoughCorrectData_ENG;

                    }
                    break;

                case gkecFitting_ExceptionWhenFitting:

                    if( gSelectedLanguage == ( int )Demo.gLanguageVersion.Chinese ) {

                        pErrorMessage = gkecFitting_ExceptionWhenFitting_CHA;

                    } else if( gSelectedLanguage == ( int )Demo.gLanguageVersion.English ) {

                        pErrorMessage = gkecFitting_ExceptionWhenFitting_ENG;

                    }
                    break;

                case gkecFitting_NoFittingResult:

                    if( gSelectedLanguage == ( int )Demo.gLanguageVersion.Chinese ) {

                        pErrorMessage = gkecFitting_NoFittingResult_CHA;

                    } else if( gSelectedLanguage == ( int )Demo.gLanguageVersion.English ) {

                        pErrorMessage = gkecFitting_NoFittingResult_ENG;

                    }
                    break;

                case gkedFitting_PixelCountMissing:

                    if( gSelectedLanguage == ( int )Demo.gLanguageVersion.Chinese ) {

                        pErrorMessage = gkedFitting_PixelCountMissing_CHA;

                    } else if( gSelectedLanguage == ( int )Demo.gLanguageVersion.English ) {

                        pErrorMessage = gkedFitting_PixelCountMissing_ENG;

                    }
                    break;

                case gkecFitting_NoCountmap:

                    if( gSelectedLanguage == ( int )Demo.gLanguageVersion.Chinese ) {

                        pErrorMessage = gkecFitting_NoCountmap_CHA;

                    } else if( gSelectedLanguage == ( int )Demo.gLanguageVersion.English ) {

                        pErrorMessage = gkecFitting_NoCountmap_ENG;

                    }
                    break;

                case gkecFitting_PixelNoMatch:

                    if( gSelectedLanguage == ( int )Demo.gLanguageVersion.Chinese ) {

                        pErrorMessage = gkecFitting_PixelNoMatch_CHA;

                    } else if( gSelectedLanguage == ( int )Demo.gLanguageVersion.English ) {

                        pErrorMessage = gkecFitting_PixelNoMatch_ENG;

                    }
                    break;

                case gkecConfiSubError_CannotDisableAll:

                    if( gSelectedLanguage == ( int )Demo.gLanguageVersion.Chinese ) {

                        pErrorMessage = gkecConfiSubError_CannotDisableAll_CHA;

                    } else if( gSelectedLanguage == ( int )Demo.gLanguageVersion.English ) {

                        pErrorMessage = gkecConfiSubError_CannotDisableAll_ENG;

                    }
                    break;

                case gkecFileSubError_MissSystemFile:

                    if (gSelectedLanguage == (int)Demo.gLanguageVersion.Chinese) {

                        pErrorMessage = gkecFileSubError_MissSystemFile_CHA;

                    } else if (gSelectedLanguage == (int)Demo.gLanguageVersion.English) {

                        pErrorMessage = gkecFileSubError_MissSystemFile_ENG;

                    }
                    break;
                case gkecFileSubError_MissSystemFileCalibration:

                    if (gSelectedLanguage == (int)Demo.gLanguageVersion.Chinese) {

                        pErrorMessage = gkecFileSubError_MissSystemFileCalibration_CHA;

                    } else if (gSelectedLanguage == (int)Demo.gLanguageVersion.English) {

                        pErrorMessage = gkecFileSubError_MissSystemFileCalibration_ENG;

                    }
                    break;

                case gkecFileSubError_MissSystemFileQuailified:

                    if (gSelectedLanguage == (int)Demo.gLanguageVersion.Chinese) {

                        pErrorMessage = gkecFileSubError_MissSystemFileQuailified_CHA;

                    } else if (gSelectedLanguage == (int)Demo.gLanguageVersion.English) {

                        pErrorMessage = gkecFileSubError_MissSystemFileQuailified_ENG;

                    }
                    break;
                case gkecReportNoteToolong:

                    if (gSelectedLanguage == (int)Demo.gLanguageVersion.Chinese) {

                        pErrorMessage = gkecReportNoteToolong_CHA;

                    } else if (gSelectedLanguage == (int)Demo.gLanguageVersion.English) {

                        pErrorMessage = gkecReportNoteToolong_ENG;

                    }
                    break;
                default:
                    if (gSelectedLanguage == (int)Demo.gLanguageVersion.Chinese) {

                        pErrorMessage = "未知错误";

                    } else if (gSelectedLanguage == (int)Demo.gLanguageVersion.English) {

                        pErrorMessage = "Unknown Error";

                    }
                    break;
            
            }

        
        }

        #endregion

        #region OutPutErrorMessage

        public void OutPutErrorMessage(int pErrorCode, string pMessage) {

            if (pMessage.Length > 0) { 
            
                MessageBox.Show(pMessage);
            
            } else {
            
                string myErrorMessage = "";

                SearchErrorMessage(pErrorCode, out myErrorMessage);

                MessageBox.Show(myErrorMessage);
            
            }
        
        
        
        }



        #endregion


    }


}
