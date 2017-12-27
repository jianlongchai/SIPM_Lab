using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace DemoTool {

    public class cScanParameters {

        #region Error Code

        public const int gkecSubSystem = cErrorCode.gkecScanParameterSubError;

        public const int gkecUnknownError = gkecSubSystem + 1;
        public const int gkecUndefiedParameter = gkecSubSystem + 2;
        public const int gkecInvalidValue = gkecSubSystem + 3;

        #endregion

        #region Global Constant

        private const string gkXmlParameterNodePath = "/ScanParamContainer/ParameterList/ScanParamPair";
        private const string gkXmlProtocolNamePath = "/ScanParamContainer/ProtocolName";
        private const string gkXmlProtocolFilePath = "/ScanParamContainer/ProtocolPath";
        private const string gkXmlProtocolFileDatePath = "/ScanParamContainer/ProtocolDate";

        public const string gkFittingPythonUnknown = "";
        public const string gkFittingPythonGe = "";
        public const string gkFittingPythonNa = "";
        public const string gkFittingPythonF18 = "";
        public const string gkFittingPythonCsBGO = "C:\\TOFTEK\\Configuration\\Fitting\\csBgoEnFit.py";
        public const string gkFittingPythonLuLyso = "C:\\TOFTEK\\Configuration\\Fitting\\luLysoEnFit.py";
        public const string gkFittingPythonLuSinglePixel = "C:\\TOFTEK\\Configuration\\Fitting\\luLysoSingleEnFit.py";

        public const string gkFittingPythonCsBGOUsingBin = "C:\\TOFTEK\\Configuration\\Fitting\\csBgoFitBin.py";
        public const string gkFittingPythonCsLysoUsingBin = "C:\\TOFTEK\\Configuration\\Fitting\\csLysoFitBin.py";
        public const string gkFittingPythonLuLysoUsingBin = "C:\\TOFTEK\\Configuration\\Fitting\\luLysoFitBin.py";
        public const string gkFittingPythonLuSinglePixelUsingBin = "C:\\TOFTEK\\Configuration\\Fitting\\luLysoSingleFitBin.py";
        public const string gkFittingPythonUsingMaxPeakUsingBin = "C:\\TOFTEK\\Configuration\\Fitting\\maxIdxEnFit.py";
        //public const string gkFittingPythonUsingLightShareUsingBin = "C:\\TOFTEK\\Configuration\\Fitting\\lightShare.py";
        public const string gkFittingPythonUsingLightShareUsingBin = "C:\\TOFTEK\\Configuration\\Fitting\\enFitting.py";
        public const string gkCountMapGenetor = "C:\\TOFTEK\\Configuration\\Fitting\\callAnger.py";

        public const string gkFittingPythonUsingLightShare196VersionUsingBin = "C:\\TOFTEK\\Configuration\\Fitting\\enFitting.py";
        public const string gkCountMapGenetor196Version = "C:\\TOFTEK\\Configuration\\Fitting\\callAnger196.py";

        public const string gkFittingPythonUsingLightShareSinglePixelUsingBin = "C:\\TOFTEK\\Configuration\\Fitting\\enFittingSinglePixel.py";
        public const string gkCountMapGenetorSinglePixel = "C:\\TOFTEK\\Configuration\\Fitting\\callAnger.py";
        

        public enum eScanParameterFileState {Nothing = 0, Modified = 1, Added = 2, Deleted = 3};

        public enum eScanTriggerType {Unknown = -1, TimeTrigger = 0, TotalEventCountTrigger = 1, SinglePixelEventCountTrigger = 2, AnalysisResultTrigger = 3};

        public enum eCorrectionType { Unknown = -1, None = 0, PixelCorrection = 1 };

        public enum eIntegralTimeType { Unknown = -1, Option_500ns = 0, Option_1000ns = 1, Option_1500ns = 2, Option_2000ns = 3};

        public enum eEncodingMode {Unknown = -1, Option_SignalPxiel = 0, Option_XDirection = 1, Option_YDirection = 2, Option_NoEncoding = 3};

        public enum eADThreshold { Unknown = -1, Option_40 = 0, Option_80 = 1, Option_160 = 2, Option_320 = 3 };

        public enum eADRange { Unknown = -1, Option_LSBP49 = 0, Option_LSBP56 = 1, Option_LSBP65 = 2, Option_LSBP78 = 3, Option_LSBP98 = 4 };

        public enum eEnergyAnalysisType {Unknown = -1,  None = 0 };

        public enum eSourceType { Unknown = -1, Ge_68 = 0, Na_22 = 1, F_18_511kev = 2, CsBgo = 3, Lu_307kev = 4, Lu_SinglePixel = 5, CsLyso = 6, UseMaxPeak = 7, LightShare = 8, LightShare196Version = 9, LightShareSiglePixel =10, };


        #endregion

        #region Global Variables

        public Dictionary<string, string> gDictScanProtocolParameters = new Dictionary<string, string> ( );

        public XmlDocument gOriginalXmlFile = new XmlDocument();

        public int gScanParameterFileState = (int)eScanParameterFileState.Nothing;

        Dictionary<string, string> gAddedKeys = new Dictionary<string, string>( );

        Dictionary<string, string> gModifiedKeys = new Dictionary<string, string>();

        #region Parameters in protocol files related

        public string gScanProtocolName = "";

        public string gScanProtocolPath = "";

        public int gScanTriggerType = (int)eScanTriggerType.TotalEventCountTrigger;

        public int gScanTriggerTimePeriod = 0;

        public int gScanTriggerTotalEventCount = 0;

        public int gScanTriggerSinglePixelEventCount = 0;

        public int gPixelNumsPerRow = 0;

        public int gPixelNumsPerCol = 0;

        public int gCorrectionOption = (int)eCorrectionType.None;

        public int gIntegralTime = (int)eIntegralTimeType.Option_500ns;

        public int gEncodingMode = (int)eEncodingMode.Option_SignalPxiel;

        public int gADCTriggerThreshold = (int)eADThreshold.Option_80;

        public int gADCRange = (int)eADRange.Option_LSBP49;

        public int gEnergyAnalysisType = (int)eEnergyAnalysisType.None;

        public float gVbias = 0;

        public float gVref = 0;

        public int gXAxis = 0;

        public int gYAxis = 0;

        public int gSourceType = (int)eSourceType.Unknown;

        public int gFittingLowBand = 2550;

        public int gFittingUpBand = 5500;

        #endregion

        #endregion

        #region Construct

        public cScanParameters() {




        }

        #endregion

        #region LoadProtocolFile
        
        int LoadProtocolFile(string pProtocolFile) {

            int myStatus = 0;

            XmlDocument myDocument = new XmlDocument();

            try {

                using (Stream myStream = File.OpenRead ( pProtocolFile )) {

                    myDocument.Load(myStream);

                    gOriginalXmlFile = myDocument;

                }

                XmlNode myProtocolName = myDocument.SelectSingleNode(gkXmlProtocolNamePath);
                gDictScanProtocolParameters.Add(myProtocolName.Name, myProtocolName.InnerText);

                XmlNode myProtocolFilePath = myDocument.SelectSingleNode(gkXmlProtocolFilePath);
                gDictScanProtocolParameters.Add(myProtocolFilePath.Name, myProtocolFilePath.InnerText);

                XmlNode myProtocolFileDate = myDocument.SelectSingleNode(gkXmlProtocolFileDatePath);
                gDictScanProtocolParameters.Add(myProtocolFileDate.Name, myProtocolFileDate.InnerText);


                #region Get All Scan Parameters

                XmlNodeList myPnodes = myDocument.SelectNodes(gkXmlParameterNodePath);

                foreach (XmlNode myNode in myPnodes) {

                    string myKey = myNode.ChildNodes.Item ( 0 ).InnerText;

                    string myValue = myNode.ChildNodes.Item ( 1 ).InnerText;

                    try {

                        gDictScanProtocolParameters.Add ( myKey, myValue );

                    } catch (Exception) {

                        //Todo:
                        //Log this error
                        

                    }

                }

                #endregion


            } catch (Exception) {

                //Todo: log this error

            }

            return myStatus;

        }

        int LoadProtocolFromXmlString(XmlDocument pXmlDoc) {

            int myStatus = 0;

            XmlDocument myDocument = pXmlDoc;

            try {

                XmlNode myProtocolName = myDocument.SelectSingleNode(gkXmlProtocolNamePath);
                gDictScanProtocolParameters.Add(myProtocolName.Name, myProtocolName.InnerText);

                XmlNode myProtocolFilePath = myDocument.SelectSingleNode(gkXmlProtocolFilePath);
                gDictScanProtocolParameters.Add(myProtocolFilePath.Name, myProtocolFilePath.InnerText);

                XmlNode myProtocolFileDate = myDocument.SelectSingleNode(gkXmlProtocolFileDatePath);
                gDictScanProtocolParameters.Add(myProtocolFileDate.Name, myProtocolFileDate.InnerText);


                #region Get All Scan Parameters

                XmlNodeList myPnodes = myDocument.SelectNodes(gkXmlParameterNodePath);

                foreach (XmlNode myNode in myPnodes) {

                    string myKey = myNode.ChildNodes.Item(0).InnerText;

                    string myValue = myNode.ChildNodes.Item(1).InnerText;

                    try {

                        gDictScanProtocolParameters.Add(myKey, myValue);

                    } catch (Exception) {

                    }

                }

                #endregion


            } catch { 
            
            
            
            }


            return myStatus;
        }

        #endregion

        #region Parse Value From Dictionary

        int GetStringValue(Dictionary<string, string> pDictionary, string pKey, out string pValue) {

            int myStatus = 0;

            pValue = "";

            if (pDictionary.TryGetValue ( pKey, out pValue )) {

                myStatus = 0;

            } else {

                myStatus = -1;

            }

            return myStatus;

        }

        int GetIntValue(Dictionary<string, string> pDictionary, string pKey, out int pValue) {

            int myStatus = 0;

            pValue = 0;

            string myValue = "";

            if (pDictionary.TryGetValue ( pKey, out myValue )) {

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

        int GetFloatValue(Dictionary<string, string> pDictionary, string pKey, out float pValue) {

            int myStatus = 0;

            pValue = (float)0.0;

            string myValue = "";

            if (pDictionary.TryGetValue ( pKey, out myValue )) {

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

        #endregion

        #region Check if value is valid and get value 

        bool GetValidTriggerType(string pTriggerType, out int pTriggerTypeValue) {

            bool myStatus = false;

            eScanTriggerType myTriggerType;

            pTriggerTypeValue = (int)eScanTriggerType.Unknown;


            //Case-Insensitive
            myStatus = Enum.TryParse<eScanTriggerType> ( pTriggerType, true, out myTriggerType );

            if (myStatus) {

                pTriggerTypeValue = (int)myTriggerType;

            }

            return myStatus;

        }

        bool GetValidCorrectionType(string pCorrectionType, out int pCorrectionTypeValue) {

            bool myStatus = false;

            eCorrectionType myCorrectionType;

            pCorrectionTypeValue = (int)eCorrectionType.None;

            //Case-Insensitive
            myStatus = Enum.TryParse<eCorrectionType> ( pCorrectionType, true, out myCorrectionType );

            if (myStatus) {

                pCorrectionTypeValue = (int)myCorrectionType;

            }

            return myStatus;

        }

        bool GetValidIntegralTimeType(string pIntegralTimeType, out int pIntegralTimeTypeValue) {

            bool myStatus = false;

            eIntegralTimeType myIntegralTimeType;

            pIntegralTimeTypeValue = (int)eIntegralTimeType.Unknown;

            //Case-Insensitive
            myStatus = Enum.TryParse<eIntegralTimeType> ( pIntegralTimeType, true, out myIntegralTimeType );

            if (myStatus) {

                pIntegralTimeTypeValue = (int)myIntegralTimeType;

            }

            return myStatus;

        }

        bool GetValidEcodingMode(string pEcodingMode, out int pEcodingModeValue) {

            bool myStatus = false;

            eEncodingMode myEcodingMode;

            pEcodingModeValue = (int)eEncodingMode.Unknown;

            //Case-Insensitive
            myStatus = Enum.TryParse<eEncodingMode> ( pEcodingMode, true, out myEcodingMode );

            if (myStatus) {

                pEcodingModeValue = (int)myEcodingMode;

            }

            return myStatus;

        }

        bool GetValidADCTriggerThreshold(string pADCTriggerThreshold, out int pADCTriggerThresholdValue) {

            bool myStatus = false;

            eADThreshold myADCTriggerThreshold;

            pADCTriggerThresholdValue = ( int )eADThreshold.Unknown;

            //Case-Insensitive
            myStatus = Enum.TryParse<eADThreshold> ( pADCTriggerThreshold, true, out myADCTriggerThreshold );

            if( myStatus ) {

                pADCTriggerThresholdValue = ( UInt16 )myADCTriggerThreshold;

            } else {

                int myContinuousADCThreshold = 65535;

                if( int.TryParse( pADCTriggerThreshold, out myContinuousADCThreshold ) ) {

                    myStatus = true;

                    pADCTriggerThresholdValue = myContinuousADCThreshold;
                
                }
            
            
            }

            return myStatus;

        }

        bool GetValidADCRange(string pADCRange, out int pADCRangeValue) {

            bool myStatus = false;

            eADRange myADCRange;

            pADCRangeValue = (int)eADRange.Unknown;

            //Case-Insensitive
            myStatus = Enum.TryParse<eADRange> ( pADCRange, true, out myADCRange );

            if (myStatus) {

                pADCRangeValue = (int)myADCRange;

            }

            return myStatus;

        }

        bool GetValidSourceType(string pSourceType, out int pSourceTypeValue) {

            bool myStatus = false;

            eSourceType mySourceType;

            pSourceTypeValue = (int)eSourceType.Unknown;

            //Case-Insensitive
            myStatus = Enum.TryParse<eSourceType>(pSourceType, true, out mySourceType);

            if (myStatus) {

                pSourceTypeValue = (int)mySourceType;

            }

            return myStatus;

        }


        #endregion

        #region FillParametersFromDictionary

        public int FillParametersFromDictionary(Dictionary<string, string> pDictionary) {

            int myStatus = 0;

            string myStringValue = "";

            #region Trigger Type

            myStatus = GetStringValue ( gDictScanProtocolParameters, cProtocolParameter.gkTriggerType, out myStringValue );

            if (myStatus == 0) {

                if (!GetValidTriggerType ( myStringValue, out gScanTriggerType )) {

                    gScanTriggerType = (int)eScanTriggerType.Unknown;

                }

            } else {

                gScanTriggerType = (int)eScanTriggerType.Unknown;

            }

            #endregion

            #region Correction Type

            myStatus = GetStringValue ( gDictScanProtocolParameters, cProtocolParameter.gkCorrectionType, out myStringValue );

            if (myStatus == 0) {

                if (!GetValidCorrectionType ( myStringValue, out gCorrectionOption )) {

                    gCorrectionOption = (int)eCorrectionType.None;

                }


            } else {

                gCorrectionOption = (int)eCorrectionType.None;

            }

            #endregion

            #region Integral Time

            myStatus = GetStringValue ( gDictScanProtocolParameters, cProtocolParameter.gkIntegralTime, out myStringValue );

            if (myStatus == 0) {

                if ( !GetValidIntegralTimeType( myStringValue, out gIntegralTime )) {

                    gIntegralTime = (int)eIntegralTimeType.Unknown;

                }


            } else {

                gIntegralTime = (int)eIntegralTimeType.Unknown;

            }

            #endregion

            #region Encoding Mode

            myStatus = GetStringValue ( gDictScanProtocolParameters, cProtocolParameter.gkEncodingMode, out myStringValue );

            if (myStatus == 0) {

                if (!GetValidEcodingMode ( myStringValue, out gEncodingMode )) {

                    gEncodingMode = (int)eEncodingMode.Unknown;

                }


            } else {

                gEncodingMode = (int)eEncodingMode.Unknown;

            }

            #endregion

            #region ADC Threshold

            myStatus = GetStringValue ( gDictScanProtocolParameters, cProtocolParameter.gkADCTriggerThreshold, out myStringValue );

            if (myStatus == 0) {

                if (!GetValidADCTriggerThreshold ( myStringValue, out gADCTriggerThreshold )) {

                    gADCTriggerThreshold = (int)eADThreshold.Unknown;

                }


            } else {

                gADCTriggerThreshold = (int)eADThreshold.Unknown;

            }

            #endregion

            #region ADC Range

            myStatus = GetStringValue ( gDictScanProtocolParameters, cProtocolParameter.gkADCRange, out myStringValue );

            if (myStatus == 0) {

                if (!GetValidADCRange ( myStringValue, out gADCRange )) {

                    gADCRange = (int)eADRange.Unknown;

                }


            } else {

                gADCRange = (int)eADRange.Unknown;

            }


            #endregion

            #region Total Event Count

            myStatus = GetIntValue ( gDictScanProtocolParameters, cProtocolParameter.gkTotalEventCount, out gScanTriggerTotalEventCount );

            if (myStatus != 0) {

                gScanTriggerTotalEventCount = -1;

            }

            #endregion

            #region Total Single Pixel Event Count

            myStatus = GetIntValue ( gDictScanProtocolParameters, cProtocolParameter.gkSinglePixelEventCount, out gScanTriggerSinglePixelEventCount );

            if (myStatus != 0) {

                gScanTriggerSinglePixelEventCount = -1;

            }

            #endregion

            #region Trigger Time Period

            myStatus = GetIntValue ( gDictScanProtocolParameters, cProtocolParameter.gkTriggerTimePeriod, out gScanTriggerTimePeriod );

            if (myStatus != 0) {

                gScanTriggerTimePeriod = -1;

            }

            #endregion

            #region Pixel No Per Row

            myStatus = GetIntValue ( gDictScanProtocolParameters, cProtocolParameter.gkPixelNoPerRow, out gPixelNumsPerRow );

            if (myStatus != 0) {

                gPixelNumsPerRow = 16;

            }

            #endregion

            #region Pixel No Per Col

            myStatus = GetIntValue ( gDictScanProtocolParameters, cProtocolParameter.gkPixelNoPerCol, out gPixelNumsPerCol );

            if (myStatus != 0) {

                gPixelNumsPerCol = 16;

            }

            #endregion

            #region Vbias

            myStatus = GetFloatValue(gDictScanProtocolParameters, cProtocolParameter.gkVbias, out gVbias);

            if (myStatus != 0) {

                gVbias = -1f;
            
            }


            #endregion

            #region Vref 

            myStatus = GetFloatValue(gDictScanProtocolParameters, cProtocolParameter.gkVref, out gVref);

            if (myStatus != 0) {

                gVref = -1f;
            
            }

            #endregion

            #region FixedXAxis

            myStatus = GetIntValue(gDictScanProtocolParameters, cProtocolParameter.gkFixedXAxis, out gXAxis);

            if (myStatus != 0) {

                gXAxis = 16384;

            }

            #endregion

            #region FixedYAxis

            myStatus = GetIntValue(gDictScanProtocolParameters, cProtocolParameter.gkFixedYAxis, out gYAxis);

            if (myStatus != 0) {

                gYAxis = 16384;

            }

            #endregion

            #region Source Type

            myStatus = GetStringValue(gDictScanProtocolParameters, cProtocolParameter.gkSourceType, out myStringValue);

            if (myStatus == 0) {

                if (!GetValidSourceType(myStringValue, out gSourceType)) {

                    gSourceType = (int)eSourceType.Lu_307kev;

                }


            } else {

                gSourceType = (int)eSourceType.Lu_307kev;

            }

            #endregion

            #region Fitting Low Band

            myStatus = GetIntValue( gDictScanProtocolParameters, cProtocolParameter.gkFittingLowBand, out gFittingLowBand );

            if( myStatus != 0 ) {

                gFittingLowBand = 2550;

            }

            #endregion

            #region Fitting Up Band

            myStatus = GetIntValue( gDictScanProtocolParameters, cProtocolParameter.gkFittingUpBand, out gFittingUpBand );

            if( myStatus != 0 ) {

                gFittingUpBand = 5500;

            }

            #endregion

            return myStatus;

        }

        #endregion

        #region FillScanParameterFromFile

        public int FillScanParameterFromFile(string pFilePath) {

            int myStatus = 0;

            myStatus = LoadProtocolFile ( pFilePath );

            if (myStatus == 0) {


                myStatus = FillParametersFromDictionary ( gDictScanProtocolParameters );
            

            }


            return myStatus;
        
        
        }

        #endregion

        #region FillScanParameterFromXmlDoc

        public int FillScanParameterFromXmlDoc(XmlDocument pXmlDoc) {

            int myStatus = 0;

            myStatus = LoadProtocolFromXmlString (pXmlDoc);

            if (myStatus == 0) {


                myStatus = FillParametersFromDictionary(gDictScanProtocolParameters);


            }


            return myStatus;


        }

        #endregion

        #region Modify Protocol Name

        public int ModifyProtocolName(string pValue) {

            int myStatus = 0;

            //If updated ok
            //At same time need update the xml file
            string myNodePath = gkXmlProtocolNamePath;

            XmlNode myModifiedNode = gOriginalXmlFile.SelectSingleNode(myNodePath);

            if (myModifiedNode != null) {

                myModifiedNode.InnerText = pValue;

            }

            gScanParameterFileState = (int)eScanParameterFileState.Modified;

            return myStatus;

        } 


        #endregion

        #region Modify Protocol File Path

        public int ModifyProtocolPath(string pPath) {

            int myStatus = 0;

            //If updated ok
            //At same time need update the xml file
            string myNodePath = gkXmlProtocolFilePath;

            XmlNode myModifiedNode = gOriginalXmlFile.SelectSingleNode(myNodePath);

            if (myModifiedNode != null) {

                myModifiedNode.InnerText = pPath;

            }

            gScanParameterFileState = (int)eScanParameterFileState.Modified;

            return myStatus;

        }


        #endregion

        #region Modify Protocol File Date

        public int ModifyProtocolDate(string pData) {

            int myStatus = 0;

            //If updated ok
            //At same time need update the xml file
            string myNodePath = gkXmlProtocolFileDatePath;

            XmlNode myModifiedNode = gOriginalXmlFile.SelectSingleNode(myNodePath);

            if (myModifiedNode != null) {

                myModifiedNode.InnerText = pData;

            }

            gScanParameterFileState = (int)eScanParameterFileState.Modified;

            return myStatus;

        }


        #endregion
        
        #region Modify Parameter

        public int ModifyParameter(string pKey, string pValue) {

            int myStatus = 0;

            string myLowerCaseKey = pKey.ToLower();

            if (myLowerCaseKey == cProtocolParameter.gkTriggerType.ToLower()) {

                #region Modify Trigger Type

                if (!GetValidTriggerType(pValue, out gScanTriggerType)) {

                    gScanTriggerType = (int)eScanTriggerType.Unknown;

                    myStatus = gkecInvalidValue;

                } else { 
                    
                    //Only need update xml

                }

                #endregion

            } else if (myLowerCaseKey == cProtocolParameter.gkCorrectionType.ToLower()) {

                #region Modify Correction Type

                if (!GetValidCorrectionType(pValue, out gCorrectionOption)) {

                    gCorrectionOption = (int)eCorrectionType.None;

                    myStatus = gkecInvalidValue;

                } else {

                    //Only need update xml

                }

                #endregion

            } else if (myLowerCaseKey == cProtocolParameter.gkIntegralTime.ToLower()) {

                #region Modify Integral time

                if (!GetValidIntegralTimeType(pValue, out gIntegralTime)) {

                    gIntegralTime = (int)eIntegralTimeType.Unknown;

                    myStatus = gkecInvalidValue;

                } else {
                    
                    //Only need upadate xml

                }

                #endregion

            } else if (myLowerCaseKey == cProtocolParameter.gkEncodingMode.ToLower()) {

                #region Modify Encoding Type

                if (!GetValidEcodingMode(pValue, out gEncodingMode)) {

                    gEncodingMode = (int)eEncodingMode.Unknown;

                    myStatus = gkecInvalidValue;

                } else { 
                
                    //Only need update xml
                
                }


                #endregion

            } else if (myLowerCaseKey == cProtocolParameter.gkADCTriggerThreshold.ToLower()) {

                #region Modify Threshhold

                if (!GetValidADCTriggerThreshold(pValue, out gADCTriggerThreshold)) {

                    gADCTriggerThreshold = (int)eADThreshold.Unknown;

                    myStatus = gkecInvalidValue;

                } else { 
                
                    //Only need to update xml
                
                }

                #endregion

            } else if (myLowerCaseKey == cProtocolParameter.gkADCRange.ToLower()) {

                #region Modify ADC Range

                if (!GetValidADCRange(pValue, out gADCRange)) {

                    gADCRange = (int)eADRange.Unknown;

                } else { 
                
                    //Only update xml

                }

                #endregion

            } else if (myLowerCaseKey == cProtocolParameter.gkTotalEventCount.ToLower()) {

                #region Modify Total Event 

                if (!int.TryParse(pValue, out gScanTriggerTotalEventCount)) {

                    gScanTriggerTotalEventCount = -1;

                    myStatus = gkecInvalidValue;

                } else { 
                
                    //Only need update xml

                }

                #endregion

            } else if (myLowerCaseKey == cProtocolParameter.gkSinglePixelEventCount.ToLower()) {

                #region Modify Single Pixel Total Event Count

                if (!int.TryParse(pValue, out gScanTriggerSinglePixelEventCount)) {

                    gScanTriggerSinglePixelEventCount = -1;

                    myStatus = gkecInvalidValue;

                } else { 
                
                    //Only need update xml
                
                }


                #endregion

            } else if (myLowerCaseKey == cProtocolParameter.gkTriggerTimePeriod.ToLower()) {

                #region Modify Trigger Time Period

                if (!int.TryParse(pValue, out gScanTriggerTimePeriod)) {

                    gScanTriggerTimePeriod = -1;

                    myStatus = gkecInvalidValue;

                } else { 
                
                    //Only need update xml
                
                }


                #endregion

            } else if (myLowerCaseKey == cProtocolParameter.gkPixelNoPerRow.ToLower()) {

                #region Modify Pxiel Per Row

                if (!int.TryParse(pValue, out gPixelNumsPerRow)) {

                    gPixelNumsPerRow = 16;

                    myStatus = gkecInvalidValue;

                } else { 
                
                    //Only need update xml
                
                }

                #endregion

            } else if (myLowerCaseKey == cProtocolParameter.gkPixelNoPerCol.ToLower()) {

                #region Modfify Pixel Per Col

                if (!int.TryParse(pValue, out gPixelNumsPerCol)) {

                    gPixelNumsPerCol = 16;

                    myStatus = gkecInvalidValue;

                } else { 
                    
                    //Only need update xml
                
                }

                #endregion

            } else if (myLowerCaseKey == cProtocolParameter.gkVbias.ToLower()) {

                #region Modify Vbias

                if (!float.TryParse(pValue, out gVbias)) {

                    gVbias = -1f;

                    myStatus = gkecInvalidValue;

                } else { 
                
                    //Only need update xml
                
                }

                #endregion

            } else if (myLowerCaseKey == cProtocolParameter.gkVref.ToLower()) {

                #region Modify Vref

                if (!float.TryParse(pValue, out gVref)) {

                    gVref = -1f;

                    myStatus = gkecInvalidValue;

                } else { 
                    
                    //Only need update xml
                
                }

                #endregion

            } else if (myLowerCaseKey == cProtocolParameter.gkFixedXAxis.ToLower()) {

                #region Modify XAxis

                if (!int.TryParse(pValue, out gXAxis)) {

                    gXAxis = 16384;

                    myStatus = gkecInvalidValue;

                } else {

                    //Only need update xml

                }

                #endregion

            } else if (myLowerCaseKey == cProtocolParameter.gkFixedYAxis.ToLower()) {

                #region Modify YAxis

                if (!int.TryParse(pValue, out gYAxis)) {

                    gYAxis = 16384;

                    myStatus = gkecInvalidValue;

                } else {

                    //Only need update xml

                }

                #endregion

            } else { 
            
                //Undefined parameters
                myStatus = gkecUndefiedParameter;

                return myStatus;
            
            }


            if (myStatus == 0) {
                
                //If updated ok
                //At same time need update the xml file
                string myNodePath = gkXmlParameterNodePath + "[key='" + pKey + "']";

                XmlNode myModifiedNode = gOriginalXmlFile.SelectSingleNode(myNodePath);

                if (myModifiedNode != null) {

                    myModifiedNode["value"].InnerText = pValue;

                }

                gScanParameterFileState = (int)eScanParameterFileState.Modified;
            
            }


            return myStatus;
        
        }
        #endregion

        #region Add Parameter

        public int AddParameter(string pKey, string pValue) {

            int myStatus = 0;


            return myStatus;
        
        }


        #endregion

        #region SaveScanParametersFiles

        public int SaveScanParametersFiles(XmlDocument pXmlDocument, string pFilePath) {

            int myStatus = 0;

            pXmlDocument.Save(pFilePath);

            return myStatus;

        }

        #endregion

        #region ExportScanParameterFiles

        public int ExportScanParameterFiles(string pOldFileName, string pNewFileName) {

            int myStatus = 0;


            return myStatus;

        }

        #endregion


    }


}
