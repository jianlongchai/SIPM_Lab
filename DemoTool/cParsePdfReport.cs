using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace DemoTool {

    class cParsePdfReport {

        public const string gkFirstLineDataLineKeywordChinese = "分辨率 X0 X1 X2 X3 X4 X5 X6 X7 X8 X9 X10 X11 X12 X13 X14 X15";
        public const string gkFirstLineDataLineKeywordEnglish = "Resolution X0 X1 X2 X3 X4 X5 X6 X7 X8 X9 X10 X11 X12 X13 X14 X15";
        public const string gkRangeKeywordChinese = "绿色 属于范围：";
        public const string gkRangeKeywordEnglish = "Green In range:";
        public const string gkTemperatureKeywordChinese = "温度:";
        public const string gkTemperatureKeywordEnglish = "Temperature:";

        public const string gkXheaderKeyword = "X0 X1 X2 X3 X4 X5 X6 X7 X8 X9 X10 X11 X12 X13 X14 X15";

        private const float gkDefaultNanFloat = -512.0f;
        private const int gkDefaultNanInt = -512;

        public bool gIsGetMinMax = false;
        public float gEnergyMin = 0.0f;
        public float gEnergyMax = 16384.0f;

        public float gTemperature = 0.0f;

        public int gFieldCount = 2;

        public bool gFoundFirstDataLine = false;

        public int gStartFiled = 0;

        public bool gPassedHeaderPart = false;

        public int gDataFiledMask = 0;


        public float gMinEnergyCountValue = -512.0f;
        public float gMaxEnergyCountValue = -512.0f;
        public float gMinEnergyResolutionValue = -512.0f;
        public float gMaxEnergyResolutionValue = -512.0f;

        public List<cMaxMinValue> gMaxMinArray = new List<cMaxMinValue> ( ); 

        public string[] gPDFLines;

        public cReportPixelInfo[] gPixelInforForPdf = new cReportPixelInfo[256];

        public string ExtractTextFromPdf( string pFilePath ) {

            string myString = "";

            using( PdfReader myPdfReader = new PdfReader( pFilePath ) ) {

                StringBuilder myTextBuilder = new StringBuilder( );

                for( int myPageNo = 1; myPageNo <= myPdfReader.NumberOfPages; myPageNo++ ) {

                    myTextBuilder.Append( PdfTextExtractor.GetTextFromPage( myPdfReader, myPageNo ) );

                }

                myString = myTextBuilder.ToString( );

                gPDFLines = myString.Split( '\n' );

                gStartFiled = 0;

                gPassedHeaderPart = false;

                gStartFiled = 0;

                gDataFiledMask = 0;

                gFoundFirstDataLine = false;

                gMinEnergyCountValue = -512.0f;
                gMaxEnergyCountValue = -512.0f;
                gMinEnergyResolutionValue = -512.0f;
                gMaxEnergyResolutionValue = -512.0f;

                return myString;

            }

        }

        public float ExtractTemperatureFromString( string pStringContent, string pKeyword ) {

            float myTemperatureValue = -512.0f;

            int myStartIndex = 0;

            int myEndIndex = 0;

            myStartIndex = pStringContent.IndexOf( pKeyword );

            myEndIndex = pStringContent.IndexOf( "C" );

            if( myEndIndex != myStartIndex ) {

                string myTemp = pStringContent.Substring( myStartIndex, myEndIndex );

                if( !float.TryParse( myTemp, out myTemperatureValue ) ) {

                    myTemperatureValue = -512.0f;

                }

            }

            return myTemperatureValue;

        }

        public int ExtractPixelInfoFromOneLine( string pStringContent, ref cReportPixelInfo[] pParsedPixelInfo ) {

            int myCount = 0;

            int myIndex = 0;

            string[] myDataPairArray = pStringContent.Split( ' ' );

            myCount = myDataPairArray.Count( );

            if( myCount > 0 ) {

                foreach( string myDataPair in myDataPairArray ) {

                    string[] myDataValue = myDataPair.Split( '/' );

                    if( myDataValue.Count( ) == gFieldCount ) {

                        #region Parse Field

                        float myTemp = 0.0f;

                        if( float.TryParse( myDataValue[0], out myTemp ) ) {

                            pParsedPixelInfo[myIndex].mEnergyValue = myTemp;

                        }

                        if( float.TryParse( myDataValue[0].Remove( '%' ), out myTemp ) ) {

                            pParsedPixelInfo[myIndex].mResolution = myTemp;

                        }

                        myIndex++;

                        #endregion

                    }

                }

            }


            return myIndex;

        }

        public int ExtractPixelInfoFromFile( ) {

            int myLineCount = gPDFLines.Count( );

            int myLineIndex = 0;

            int myDataCount = 0;

            for( myLineIndex = 0; myLineIndex < myLineCount; myLineIndex++ ) {

                //These are header of data line
                if( gPDFLines[myLineIndex] == gkFirstLineDataLineKeywordChinese ||
                    gPDFLines[myLineIndex] == gkFirstLineDataLineKeywordEnglish || gPDFLines[myLineIndex] == "Y0" ||
                    gPDFLines[myLineIndex] == "Y1" || gPDFLines[myLineIndex] == "Y2" || gPDFLines[myLineIndex] == "Y3" ||
                    gPDFLines[myLineIndex] == "Y4" || gPDFLines[myLineIndex] == "Y5" || gPDFLines[myLineIndex] == "Y6" ||
                    gPDFLines[myLineIndex] == "Y7" || gPDFLines[myLineIndex] == "Y8" || gPDFLines[myLineIndex] == "Y9" ||
                    gPDFLines[myLineIndex] == "Y10" || gPDFLines[myLineIndex] == "Y11" || gPDFLines[myLineIndex] == "Y12" ||
                    gPDFLines[myLineIndex] == "Y13" || gPDFLines[myLineIndex] == "Y14" || gPDFLines[myLineIndex] == "Y15" ) {

                    //Should not use myLindexIndex++ here because if there is case 
                    //Y1
                    //Y2
                    //DATA
                    //This format will has some error
                    //myLineIndex++;

                    if( ( (myLineIndex+1) < myLineCount ) && !( gPDFLines[myLineIndex+1] == gkFirstLineDataLineKeywordChinese ||
                     gPDFLines[myLineIndex+1].Contains(gkFirstLineDataLineKeywordEnglish) || gPDFLines[myLineIndex+1].Contains("Y0") ||
                     gPDFLines[myLineIndex+1].Contains("Y1") || gPDFLines[myLineIndex+1].Contains("Y2") || gPDFLines[myLineIndex+1].Contains("Y3") ||
                     gPDFLines[myLineIndex+1].Contains("Y4") || gPDFLines[myLineIndex+1].Contains("Y5") || gPDFLines[myLineIndex+1].Contains("Y6") ||
                     gPDFLines[myLineIndex+1].Contains("Y7") || gPDFLines[myLineIndex+1].Contains("Y8") || gPDFLines[myLineIndex+1].Contains("Y9") ||
                     gPDFLines[myLineIndex+1].Contains("Y10") || gPDFLines[myLineIndex+1].Contains("Y11") || gPDFLines[myLineIndex+1].Contains("Y12") ||
                     gPDFLines[myLineIndex+1].Contains("Y13") || gPDFLines[myLineIndex+1].Contains("Y14") || gPDFLines[myLineIndex+1].Contains("Y15") ) ) {

                        //Here really need a ++, because next line is the real data
                        myLineIndex++;

                        //string mySplitFilter = " ";
                        char[] mySplitFilter;
                        
                        bool myIsIncludeResolution = false;

                        if( gPDFLines[myLineIndex].Contains( "%" ) ) {

                            //mySplitFilter = "%";
                            mySplitFilter = new char[] { '%', ' ' };

                            myIsIncludeResolution = true;

                        } else {

                            //mySplitFilter = " ";
                            mySplitFilter = new char[] { '%', ' ' };
                            myIsIncludeResolution = false;
                        
                        }

                        //If next line still header, then it is empty
                        #region Parse Line

                        int myCount = 0;

                        //string[] myDataPairArray = Regex.Split( gPDFLines[myLineIndex], mySplitFilter );
                        string[] myDataPairArray = gPDFLines[myLineIndex].Split( mySplitFilter );

                        myCount = myDataPairArray.Count( );

                        if( myCount > 0 ) {

                            foreach( string myDataPair in myDataPairArray ) {

                                if( myIsIncludeResolution ) {

                                    string[] myDataValue = myDataPair.Split( '/' );

                                    if( myDataValue.Count( ) == gFieldCount ) {

                                        #region Parse Field

                                        float myTemp = 0.0f;

                                        string myStrTemp = "";

                                        gPixelInforForPdf[myDataCount] = new cReportPixelInfo( );

                                        if( float.TryParse( myDataValue[0], out myTemp ) ) {

                                            gPixelInforForPdf[myDataCount].mEnergyValue = myTemp;

                                        }

                                        myStrTemp = myDataValue[1].Replace( '%', ' ' );
                                        if( float.TryParse( myStrTemp, out myTemp ) ) {

                                            gPixelInforForPdf[myDataCount].mResolution = myTemp;

                                        }

                                        gPixelInforForPdf[myDataCount].mTemperature = gTemperature;



                                        #endregion

                                        myDataCount++;

                                    }

                                } else {

                                    //Does not have resolution
                                    #region No Resolution

                                    float myTemp = 0.0f;

                                    gPixelInforForPdf[myDataCount] = new cReportPixelInfo( );

                                    if( float.TryParse( myDataPair, out myTemp ) ) {

                                        gPixelInforForPdf[myDataCount].mEnergyValue = myTemp;

                                    }

                                    gPixelInforForPdf[myDataCount].mResolution = gkDefaultNanFloat;

                                    gPixelInforForPdf[myDataCount].mTemperature = gTemperature;

                                    myDataCount++;

                                    #endregion

                                }

                            }

                        }

                        #endregion

                    }

                } else if( ( gPDFLines[myLineIndex].Contains( gkRangeKeywordChinese ) || gPDFLines[myLineIndex].Contains( gkRangeKeywordEnglish ) )
                              && ( gIsGetMinMax == false ) ) {

                    #region Get Min and Max
                    int myCount = 0;

                    string myTemp = "";

                    string[] myDataPairArray = gPDFLines[myLineIndex].Split( '-' );

                    myCount = myDataPairArray.Count( );

                    if( myCount >= 2 ) {

                        if( gPDFLines[myLineIndex].Contains( gkRangeKeywordChinese ) ) {

                            myTemp = myDataPairArray[0].Replace( gkRangeKeywordChinese, "" );

                        } else if( gPDFLines[myLineIndex].Contains( gkRangeKeywordEnglish ) ) {

                            myTemp = myDataPairArray[0].Replace( gkRangeKeywordEnglish, "" );

                        }

                        if( !float.TryParse( myTemp, out gEnergyMin ) ) {

                            gEnergyMin = 0.0f;

                        }

                        if( !float.TryParse( myDataPairArray[1], out gEnergyMax ) ) {

                            gEnergyMax = 16384.0f;

                        }

                        gIsGetMinMax = true;


                    }

                    #endregion

                } else if( gPDFLines[myLineIndex].Contains( gkTemperatureKeywordChinese ) || gPDFLines[myLineIndex].Contains( gkTemperatureKeywordEnglish ) ) {

                    #region Temperature Parse

                    int myCount = 0;

                    string myTemp = "";

                    if( gPDFLines[myLineIndex].Contains( gkTemperatureKeywordChinese ) ) {

                        myTemp = gkTemperatureKeywordChinese;

                    } else if( gPDFLines[myLineIndex].Contains( gkTemperatureKeywordEnglish ) ) {

                        myTemp = gkTemperatureKeywordEnglish;

                    }

                    string[] myDataPairArray = Regex.Split( gPDFLines[myLineIndex], myTemp );

                    myCount = myDataPairArray.Count( );

                    if( myCount >= 2 ) {

                        if( !float.TryParse( myDataPairArray[1].Replace( "C", "" ), out gTemperature ) ) {

                            gTemperature = 0.0f;

                        }

                    }

                    #endregion

                }

            }

            return myDataCount;

        }

        public int ExtractPixelInfoFromFileWithRandomFormat( ) {

            int myLineCount = gPDFLines.Count( );

            int myLineIndex = 0;

            int myRwoIndex = -1;

            int myTotalColumn = 0;

            int myColumnIndex = 0;

            int myDataIndex = 0;

            for (myLineIndex = 0; myLineIndex < myLineCount; myLineIndex++) {


                if (gPDFLines[myLineIndex] == gkXheaderKeyword) {

                    gPassedHeaderPart = true;

                } else if ((gPassedHeaderPart == true) && gPDFLines[myLineIndex].Contains ( "/" )) {

                    //It means it is a data filed

                    if (gFoundFirstDataLine == false) {

                        gFoundFirstDataLine = true;

                        #region First data line need check the start data filed
                        //This is the start part
                        if (gPDFLines[myLineIndex].Contains ( "Cnt" )) {
                            //4
                            //It is count data
                            gStartFiled = 4;
                            gDataFiledMask |= 4;

                        } else if (gPDFLines[myLineIndex].Contains ( "%" )) {
                            //2
                            //It is resolution data
                            gStartFiled = 2;
                            gDataFiledMask |= 2;

                        } else {
                            //1
                            //It is energy data
                            gStartFiled = 1;
                            gDataFiledMask |= 1;

                        }

                        #endregion

                    }


                    if (gPDFLines[myLineIndex].Contains ( "Cnt" )) {
                        //4
                        //It is count data
                        #region Count Data

                        if (gStartFiled == 4) {

                            myRwoIndex++; 

                        }

                        char[] mySplitFilter = new char[] { '/', ' ' };

                        #region Parse Line

                        int myCount = 0;

                        //string[] myDataPairArray = Regex.Split( gPDFLines[myLineIndex], mySplitFilter );
                        string[] myDataPairArray = gPDFLines[myLineIndex].Split ( mySplitFilter );

                        myCount = myDataPairArray.Count ( );

                        if (myCount > 0) {

                            float myTemp = 0;

                            string myDataString = "";

                            myColumnIndex = 0;

                            foreach (string myDataPair in myDataPairArray) {

                                //In case that there is some empty filed
                                if (myDataPair.Length > 0) {

                                    myDataIndex = myRwoIndex * myTotalColumn + myColumnIndex;

                                    #region Only Parse Data Parts

                                    //Only create new when it is first time
                                    if (gPixelInforForPdf[myDataIndex] == null) {

                                        gPixelInforForPdf[myDataIndex] = new cReportPixelInfo ( );

                                    }

                                    myDataString = myDataPair.Replace ( "Cnt", "" );

                                    if (float.TryParse ( myDataString, out myTemp )) {

                                        gPixelInforForPdf[myDataIndex].mCount = (int)myTemp;

                                    }

                                    if (gPixelInforForPdf[myDataIndex].mResolution < 0) {

                                        gPixelInforForPdf[myDataIndex].mResolution = gkDefaultNanFloat;

                                    }

                                    if (gPixelInforForPdf[myDataIndex].mResolution < 0) {

                                        gPixelInforForPdf[myDataIndex].mEnergyValue = gkDefaultNanFloat;

                                    }

                                    gPixelInforForPdf[myDataIndex].mTemperature = gTemperature;

                                    myColumnIndex++;

                                    #endregion

                                }

                            }

                            myTotalColumn = myColumnIndex;

                        }

                        #endregion

                        #endregion

                    } else if (gPDFLines[myLineIndex].Contains ( "%" )) {
                        //2
                        //It is resolution data

                        #region Resolution Data

                        if (gStartFiled == 2) {

                            myRwoIndex++; 

                        }

                        char[] mySplitFilter = new char[] { '/', '%' };

                        #region Parse Line

                        int myCount = 0;

                        //string[] myDataPairArray = Regex.Split( gPDFLines[myLineIndex], mySplitFilter );
                        string[] myDataPairArray = gPDFLines[myLineIndex].Split ( mySplitFilter );

                        myCount = myDataPairArray.Count ( );

                        if (myCount > 0) {

                            float myTemp = 0;

                            string myDataString = "";

                            myColumnIndex = 0;

                            foreach (string myDataPair in myDataPairArray) {

                                //In case that there is some empty filed
                                if (myDataPair.Length > 0) {

                                    myDataIndex = myRwoIndex * myTotalColumn + myColumnIndex;

                                    #region Only Parse Data Parts

                                    //Only create new when it is first time
                                    if (gPixelInforForPdf[myDataIndex] == null) {

                                        gPixelInforForPdf[myDataIndex] = new cReportPixelInfo ( );

                                    }

                                    myDataString = myDataPair;

                                    if (float.TryParse ( myDataString, out myTemp )) {

                                        gPixelInforForPdf[myDataIndex].mResolution = myTemp;

                                        if ( (gMinEnergyResolutionValue < 0) || (myTemp < gMinEnergyResolutionValue)) {

                                            gMinEnergyResolutionValue = myTemp;

                                        }

                                        if ( (gMaxEnergyResolutionValue < 0) || (myTemp > gMaxEnergyResolutionValue) ) {

                                            gMaxEnergyResolutionValue = myTemp;
                                        
                                        }


                                    }


                                    gPixelInforForPdf[myDataIndex].mTemperature = gTemperature;

                                    myColumnIndex++;

                                    #endregion

                                }

                            }

                            myTotalColumn = myColumnIndex;

                        }

                        #endregion

                        #endregion

                    } else {
                        //1
                        //It is energy data

                        #region Energy Data

                        if (gStartFiled == 1) {

                            myRwoIndex++; 

                        }

                        char[] mySplitFilter = new char[] { '/', ' ' };

                        #region Parse Line

                        int myCount = 0;

                        //string[] myDataPairArray = Regex.Split( gPDFLines[myLineIndex], mySplitFilter );
                        string[] myDataPairArray = gPDFLines[myLineIndex].Split ( mySplitFilter );

                        myCount = myDataPairArray.Count ( );

                        if (myCount > 0) {

                            float myTemp = 0;

                            string myDataString = "";

                            myColumnIndex = 0;

                            foreach (string myDataPair in myDataPairArray) {

                                //In case that there is some empty filed
                                if (myDataPair.Length > 0) {

                                    myDataIndex = myRwoIndex * myTotalColumn + myColumnIndex;

                                    #region Only Parse Data Parts

                                    //Only create new when it is first time
                                    if (gPixelInforForPdf[myDataIndex] == null) {

                                        gPixelInforForPdf[myDataIndex] = new cReportPixelInfo ( );

                                    }

                                    myDataString = myDataPair;

                                    if (float.TryParse ( myDataString, out myTemp )) {

                                        gPixelInforForPdf[myDataIndex].mEnergyValue = myTemp;

                                        if ( (gMinEnergyCountValue < 0) || (myTemp < gMinEnergyCountValue)) {

                                            gMinEnergyCountValue = myTemp;
                                        
                                        }

                                        if ((gMaxEnergyCountValue < 0) || (myTemp > gMaxEnergyCountValue)) {

                                            gMaxEnergyCountValue = myTemp;

                                        }


                                    }

                                    gPixelInforForPdf[myDataIndex].mTemperature = gTemperature;

                                    myColumnIndex++;

                                    #endregion

                                }

                            }

                            myTotalColumn = myColumnIndex;

                        }

                        #endregion

                        #endregion

                    }




                } else if ((gPDFLines[myLineIndex].Contains ( gkRangeKeywordChinese ) || gPDFLines[myLineIndex].Contains ( gkRangeKeywordEnglish ))
                               && (gIsGetMinMax == false)) {

                    #region Get Min and Max
                    int myCount = 0;

                    string myTemp = "";

                    string[] myDataPairArray = gPDFLines[myLineIndex].Split ( '-' );

                    myCount = myDataPairArray.Count ( );

                    if (myCount >= 2) {

                        if (gPDFLines[myLineIndex].Contains ( gkRangeKeywordChinese )) {

                            myTemp = myDataPairArray[0].Replace ( gkRangeKeywordChinese, "" );

                        } else if (gPDFLines[myLineIndex].Contains ( gkRangeKeywordEnglish )) {

                            myTemp = myDataPairArray[0].Replace ( gkRangeKeywordEnglish, "" );

                        }

                        if (!float.TryParse ( myTemp, out gEnergyMin )) {

                            gEnergyMin = 0.0f;

                        }

                        if (!float.TryParse ( myDataPairArray[1], out gEnergyMax )) {

                            gEnergyMax = 16384.0f;

                        }

                        gIsGetMinMax = true;


                    }

                    #endregion

                } else if (gPDFLines[myLineIndex].Contains ( gkTemperatureKeywordChinese ) || gPDFLines[myLineIndex].Contains ( gkTemperatureKeywordEnglish )) {

                    #region Temperature Parse

                    int myCount = 0;

                    string myTemp = "";

                    if (gPDFLines[myLineIndex].Contains ( gkTemperatureKeywordChinese )) {

                        myTemp = gkTemperatureKeywordChinese;

                    } else if (gPDFLines[myLineIndex].Contains ( gkTemperatureKeywordEnglish )) {

                        myTemp = gkTemperatureKeywordEnglish;

                    }

                    string[] myDataPairArray = Regex.Split ( gPDFLines[myLineIndex], myTemp );

                    myCount = myDataPairArray.Count ( );

                    if (myCount >= 2) {

                        if (!float.TryParse ( myDataPairArray[1].Replace ( "C", "" ), out gTemperature )) {

                            gTemperature = 0.0f;

                        }

                    }

                    #endregion

                }

            }

            //Convert row index into number of rows
            myRwoIndex++;

            cMaxMinValue myReportMaxMinValue = new cMaxMinValue ( );

            myReportMaxMinValue.mMaxEnergyValue = gMaxEnergyCountValue;
            myReportMaxMinValue.mMinEnergyValue = gMinEnergyCountValue;
            myReportMaxMinValue.mMaxResolutionValue = gMaxEnergyResolutionValue;
            myReportMaxMinValue.mMinResolutionValue = gMinEnergyResolutionValue;

            gMaxMinArray.Add ( myReportMaxMinValue );

            return (myRwoIndex*myColumnIndex);

        }


    }

    public class cMaxMinValue { 
    
        public float mMinEnergyValue = 0.0f;
        public float mMaxEnergyValue = 0.0f;
        public float mMinResolutionValue = 0.0f;
        public float mMaxResolutionValue = 0.0f;
    
    
    }


}
