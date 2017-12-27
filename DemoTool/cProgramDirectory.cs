using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemoTool {
    class cProgramDirectory {

        public const string gkConfigFolderName = "Configuration";
        public const string gkProtocolFolderName = "Protocols";
        public const string gkFittingFolderName = "Fitting";
        public const string gkRunTimeFolderName = "RunTime";
        public const string gkImagesFolderName = "Images";
        public const string gkPythonLibFolderName = "PythonLib";
        public const string gkRawDataFolderName = "RawData";
        public const string gkCalibrationFolderName = "Calibration";
        public const string gkCalibrationFileName = "SPMLAB_CALIBRATION.csv";
        public const string gkQualifiedResolutionFileName = "ResoltionQualified.csv";
        public const string gkQualifiedCountFileName = "CountQualified.csv";
        public const string gkBasePixelPositionFile = "BasePositioning.csv";
        public const string gkSinglePixelCalibrationFile = "SinglePixelCalibration.csv";

        public const string gkTOFTECKMainFolder = "C:\\TOFTEK\\";
        public const string gkTOFTEKConfigFolder = gkTOFTECKMainFolder + gkConfigFolderName + "\\";         //"C:\\TOFTEK\\Configuration\\";
        public const string gkTOFTEKRawDataFolder = gkTOFTECKMainFolder + gkRawDataFolderName + "\\";       //"C:\\TOFTEK\\RawData\\";
        public const string gkTOFTEKFittingFolder = gkTOFTEKConfigFolder + gkFittingFolderName + "\\";      //"C:\\TOFTEK\\Configuration\\Fitting\\";
        public const string gkTOFTEKProtocolFolder = gkTOFTEKConfigFolder + gkProtocolFolderName + "\\";    //"C:\\TOFTEK\\Configuration\\Protocols\\";
        public const string gkRunTimeFolder = gkTOFTECKMainFolder + gkRunTimeFolderName + "\\";                 //"C:\\TOFTEK\\RunTime\\";
        public const string gkImagesFolder = gkTOFTECKMainFolder + gkImagesFolderName + "\\";                   //"C:\\TOFTEK\\Images\\";
        public const string gkPythonLibFolder = gkTOFTECKMainFolder + gkPythonLibFolderName + "\\";             //"C:\\TOFTEK\\PythonLib\\";
        public const string gkCalibrationFolder = gkTOFTECKMainFolder + gkCalibrationFolderName + "\\";     //"C:\\TOFTEK\\Calibration\\";
        public const string gkQualifiedLevelFolder = gkTOFTECKMainFolder + gkCalibrationFolderName + "\\";     //"C:\\TOFTEK\\Calibration\\";

        public const string gkInstallationConfigFolder = "\\Configuration\\";
        public const string gkInstallationProtocolFolder = "\\Protocol\\";
        public const string gkInstallationFittingFolder = "\\Fitting\\";
        public const string gkInstallationRunTimeFolder = "\\RunTime\\";
        public const string gkInstallationImagesFolder = "\\Images\\";
        public const string gkInstallationPythonLibFolder = "\\PythonLib\\";

    }
}
