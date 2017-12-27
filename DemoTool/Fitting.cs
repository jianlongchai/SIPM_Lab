using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;



namespace DemoTool {
    
    public class Fitting {

        private Demo gParenetWindows;
        //private const int gkLuLYSOFieldCount = 21;
        private const int gkLuLYSOFieldCount = 11;
        private const int gkCsBGOFiledCount = 14;
        private const int gkLightShareFiledCount = 14;

        private const string gkFittingStartString = "version";
        private const string gkStartString = "vars";
        private const string gkIdString = "id";
        private const string gkValueString = "value";
        private const string gkExceptionString = "exception";
        private const string gkErrorString = "error";

        private const string gkLuLYSOHeader = "lulyso";
        private const string gkCsBGOHeader = "csbgo";
        private const string gkCsLysoHeader = "cslyso";
        private const string gkLightShareHeader = "angerlogic";

        public const string gkFittingCsBGOFile = "csBgoEnFitBin";
        public const string gkFittingLuLyso = "luLysoEnFitBin";
        public const string gkFittingLuSinglePixel = "luLysoSingleEnFitBin";
        public const string gkFittingCsLyso = "csLysoFitBin";
        public const string gkFittingMaxPeak = "maxIdxEnFit";
        public const string gkFittingLightShare = "lightShare";
        public const string gkFittingLightShareSinglePixel = "lightShareSinglePixel";
        public const string gkFittingLightShare196Version = "lightShare196Version";

        public Dictionary<int, cFittingParameters> gDictFittingParameters = new Dictionary<int, cFittingParameters>();

        public float[] gPixelRawDataPeak = new float[256];

        public int[] gCountCnt = new int[256];

        private string gPythonTerminalOutputString = "";

        private string gPythonTerminalErrorString = "";

        private int gSourceType = (int)cScanParameters.eSourceType.Unknown;

        public static int gCurrentProcessPixel = 0;

        public static int gCurrentRecvLineNo = 0;

        private UInt32 gFittingLowBand = 2550;

        private UInt32 gFittingUpBand = 5000;

        private UInt32 gLightSharePixelNo = 12;

        private Process gLoadProcess;
        private StreamWriter gPythonRunningTerminal;
        private ProcessStartInfo gProcessInfo;

        private string gReceivedDataString = "";

        public int gErrorCode = 0;

        public int gCountMin = 0;
        public int gCountMax = 0;
        public int gCountAvg = 0;

        #region Construct 

        public Fitting(Demo pParentWindows) {

            gParenetWindows = pParentWindows;
        
        }

        #endregion

        #region OutputDataReceived

        /// <summary>
        /// Name: OutputDataReceived
        /// Description: This is used to received the normal message which redirected from terminal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        
        void OutputDataReceived(object sender, DataReceivedEventArgs e) {

            if (e.Data != null) {

                gPythonTerminalOutputString += e.Data;

                gReceivedDataString = e.Data;

                gCurrentRecvLineNo++;

            }

        }

        #endregion

        #region ErrorDataReceived

        /// <summary>
        /// Name: ErrorDataReceived
        /// Description: This is used to received the error message which redirected from terminal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        
        void ErrorDataReceived(object sender, DataReceivedEventArgs e) {

            if (e.Data != null) {

                gPythonTerminalErrorString += e.Data;

            }

        }

        #endregion

        #region RunPython

        /// <summary>
        /// Name: RunPython
        /// Description: This is used to run python code
        /// </summary>
        /// <param name="pPath"></param>
    
        public void RunPython(string pPath) {

            Process myLoadProcess = new Process();
            StreamWriter myPythonRunningTerminal;
            ProcessStartInfo myProcessInfo = new ProcessStartInfo("cmd");

            string cmd = "\"D:\\Development\\Python Code\\luEnFit.py\"";

            if (pPath.Length != 0) { 
            
                //Have parameter 
                cmd += " " +  "\""+ pPath + "\"";
            
            }

            myProcessInfo.UseShellExecute = false;
            myProcessInfo.RedirectStandardInput = true;
            myProcessInfo.RedirectStandardOutput = true;
            myProcessInfo.RedirectStandardError = true;
            myProcessInfo.CreateNoWindow = true;
            myLoadProcess.StartInfo = myProcessInfo;

            myLoadProcess.Start();
            myLoadProcess.OutputDataReceived += OutputDataReceived;
            myLoadProcess.ErrorDataReceived += ErrorDataReceived;
            myLoadProcess.BeginOutputReadLine();
            myLoadProcess.BeginErrorReadLine();

            myPythonRunningTerminal = myLoadProcess.StandardInput;
            myPythonRunningTerminal.AutoFlush = true;


            myPythonRunningTerminal.WriteLine(cmd);
            myPythonRunningTerminal.Close();

            myLoadProcess.WaitForExit();

            FillFittingResult();

        }

        public void RunPython(string pPythonFile, string pDataFile) {

            gLoadProcess = new Process();
            gProcessInfo = new ProcessStartInfo("cmd");

            gFittingLowBand = gParenetWindows.gFittingLowBand;
            gFittingUpBand = gParenetWindows.gFittingUpBand;
            gLightSharePixelNo = gParenetWindows.gLightSharePixelNo;

            string cmd = " " + "\"" + pPythonFile + "\"";

            if( pDataFile.Length != 0 ) {

                //Have parameter 
                if( ( int )gParenetWindows.gSelectedScanParametersForDataAcq.gSourceType >= ( int )cScanParameters.eSourceType.LightShare ) {

                    cmd += " " + "\"" + pDataFile + "\"";

                } else {

                    cmd += " " + "\"" + pDataFile + "\"" + " " + gFittingLowBand.ToString( ) + " " + gFittingUpBand.ToString( );
                
                }


            }

            gProcessInfo.UseShellExecute = false;
            gProcessInfo.RedirectStandardInput = true;
            gProcessInfo.RedirectStandardOutput = true;
            gProcessInfo.RedirectStandardError = true;
            gProcessInfo.CreateNoWindow = true;
            gLoadProcess.StartInfo = gProcessInfo;

            gLoadProcess.Start();
            gLoadProcess.OutputDataReceived += OutputDataReceived;
            gLoadProcess.ErrorDataReceived += ErrorDataReceived;
            gLoadProcess.BeginOutputReadLine();
            gLoadProcess.BeginErrorReadLine();

            gPythonRunningTerminal = gLoadProcess.StandardInput;
            gPythonRunningTerminal.AutoFlush = true;


            gPythonRunningTerminal.WriteLine(cmd);
            gPythonRunningTerminal.Close();

            //gLoadProcess.WaitForExit( );

            //FillFittingResult( );


        }
 
        public bool WaitforComplete(out string pStatusString) {

            bool myStatus = false;

            pStatusString = "";

            myStatus = gLoadProcess.WaitForExit( 100 );

            pStatusString = gReceivedDataString;
            
            return myStatus;

        }

        #region RunCountMap
        /**
         * This is special for light share 
         **/
        public void RunCountMap( string pPythonFile, string pDataFile ) {

            //Process myLoadProcess = new Process( );
            //StreamWriter myPythonRunningTerminal;
            //ProcessStartInfo myProcessInfo = new ProcessStartInfo( "cmd" );
            gLoadProcess = new Process( );
            gProcessInfo = new ProcessStartInfo( "cmd" );

            gFittingLowBand = gParenetWindows.gFittingLowBand;
            gFittingUpBand = gParenetWindows.gFittingUpBand;
            gLightSharePixelNo = gParenetWindows.gLightSharePixelNo;

            string cmd = " " + "\"" + pPythonFile + "\"";

            if( pDataFile.Length != 0 ) {

                //Have parameter 
                if( ( int )gParenetWindows.gSelectedScanParametersForDataAcq.gSourceType == ( int )cScanParameters.eSourceType.LightShare  ) {

                    cmd += " " + "\"" + pDataFile + "\"" + " " + gLightSharePixelNo;

                } else if( ( int )gParenetWindows.gSelectedScanParametersForDataAcq.gSourceType == ( int )cScanParameters.eSourceType.LightShare196Version  ) {

                    gLightSharePixelNo = 196;

                    cmd += " " + "\"" + pDataFile + "\"" ;

                } else if(( int )gParenetWindows.gSelectedScanParametersForDataAcq.gSourceType == ( int )cScanParameters.eSourceType.LightShareSiglePixel) {

                    cmd += " " + "\"" + pDataFile + "\"" + " " + gLightSharePixelNo;

                } else {

                    cmd += " " + "\"" + pDataFile + "\"" + " " + gFittingLowBand.ToString( ) + " " + gFittingUpBand.ToString( );
                
                }

            }

            gProcessInfo.UseShellExecute = false;
            gProcessInfo.RedirectStandardInput = true;
            gProcessInfo.RedirectStandardOutput = true;
            gProcessInfo.RedirectStandardError = true;
            gProcessInfo.CreateNoWindow = true;
            gLoadProcess.StartInfo = gProcessInfo;

            gLoadProcess.Start( );
            gLoadProcess.OutputDataReceived += OutputDataReceived;
            gLoadProcess.ErrorDataReceived += ErrorDataReceived;
            gLoadProcess.BeginOutputReadLine( );
            gLoadProcess.BeginErrorReadLine( );

            gPythonRunningTerminal = gLoadProcess.StandardInput;
            gPythonRunningTerminal.AutoFlush = true;


            gPythonRunningTerminal.WriteLine( cmd );
            gPythonRunningTerminal.Close( );

            //gLoadProcess.WaitForExit( );

        }

        #endregion


        #endregion

        #region GetRedirectedOutputString

        /// <summary>
        /// Name: GetRedirectedOutputString
        /// Description: Get the output string from terminal
        /// </summary>
        /// <returns></returns>

        string GetRedirectedOutputString() {

            return gPythonTerminalOutputString;        
        }

        #endregion

        #region GetRedirectedErrorString

        /// <summary>
        /// Name:GetRedirectedErrorString
        /// Description: Get the output string from terminal
        /// </summary>
        /// <returns></returns>
        string GetRedirectedErrorString() {

            return gPythonTerminalErrorString;

        }

        #endregion

        #region FillFittingResult

        public int FillFittingResult(string pResult) {

            int myStatus = 0;

            int myStartLineIndex = 0;

            if (pResult.Contains(gkStartString)) { 
            
                //Yes, this is a complete output message

                myStartLineIndex = pResult.IndexOf(gkStartString);

            
            }


            return myStatus;
        
        }

        public int FillFittingResult() {

            int myStatus = 0;

            int myStartLineIndex = 0;

            int myEndOfLineIndex = 0;

            string myLineContent = "";

            int myPixelIndexX = 0;

            int myNoLightShareMaxPixelPerLine = 0;

            int myPreviousPixelNo = 0;

            float myCalibratedSlope = 1.0f;
            float myCalibratedOffset = 0.0f;
            int myPixelPosition = 0;

            if (gPythonTerminalOutputString.Contains(gkFittingStartString)) {

                //Get the start of fitting line
                myStartLineIndex = gPythonTerminalOutputString.IndexOf(gkFittingStartString);

                //Get all line before the fitting line 
                gPythonTerminalOutputString = gPythonTerminalOutputString.Substring(myStartLineIndex);

                //Find the end line character of first line
                myEndOfLineIndex = gPythonTerminalOutputString.IndexOf('>');

                //Get the line content 
                myLineContent = gPythonTerminalOutputString.Substring(0, myEndOfLineIndex + 1);

                //Get rid of read section
                gPythonTerminalOutputString = gPythonTerminalOutputString.Substring(myEndOfLineIndex + 1);


                if (myLineContent.ToLower().Contains(gkLuLYSOHeader)) {

                    gSourceType = (int)cScanParameters.eSourceType.Lu_307kev;

                } else if (myLineContent.ToLower().Contains(gkCsBGOHeader)) {

                    gSourceType = (int)cScanParameters.eSourceType.CsBgo;

                } else if( myLineContent.ToLower( ).Contains( gkLightShareHeader ) ) {

                    //For light share, the single pixel, random and array are all output same format
                    gSourceType = ( int )cScanParameters.eSourceType.LightShare;
                
                }

            
            }


            if (gPythonTerminalOutputString.Contains(gkStartString)) {

                //Yes, this is a complete output message

                //Get the start of the main content 
                myStartLineIndex = gPythonTerminalOutputString.IndexOf(gkStartString);

                gPythonTerminalOutputString = gPythonTerminalOutputString.Substring(myStartLineIndex);

                //After here, the captured output string is the real data section 

                //Reset start index
                myStartLineIndex = 0;

                //Find the end line character of first line
                myEndOfLineIndex = gPythonTerminalOutputString.IndexOf('>');

                //Get the first line 
                myLineContent = gPythonTerminalOutputString.Substring(0, myEndOfLineIndex + 1);

                //Get rid of read section
                gPythonTerminalOutputString = gPythonTerminalOutputString.Substring(myEndOfLineIndex + 1);

                //End of print line is "}>"

                int myTrackId = 0;

                while ( !myLineContent.StartsWith("}") && myLineContent.Length > 0 ) {
                    
                    //Now start to read the real data
                    myEndOfLineIndex = gPythonTerminalOutputString.IndexOf('>');

                    myLineContent = gPythonTerminalOutputString.Substring(0, myEndOfLineIndex + 1);

                    gPythonTerminalOutputString = gPythonTerminalOutputString.Substring(myEndOfLineIndex + 1);

                    #region Had the Id for it

                    //Parse the ID Line
                    if (myLineContent.Contains(gkIdString)) {

                        string[] myIdString = myLineContent.Split(',');

                        if (myIdString.Length == 2) { 
                            
                            myIdString[1] = myIdString[1].Replace(">", "");

                            myTrackId = Convert.ToInt16(myIdString[1]);

                            gCurrentProcessPixel = myTrackId;
                        
                        }

                    }

                    #endregion

                    #region Has the value for it

                    if (myLineContent.Contains(gkValueString)) {

                        string[] myValueString = myLineContent.Split(',');


                        if (gSourceType == (int)cScanParameters.eSourceType.Lu_307kev) {

                            //There lulyso are 21 fileds
                            #region lulyso

                            if (myValueString.Length == gkLuLYSOFieldCount) {

                                cFittingParameters myNewFittingParameter = new cFittingParameters();

                                myValueString[gkLuLYSOFieldCount - 1] = myValueString[gkLuLYSOFieldCount-1].Replace(">", "");

                                myNewFittingParameter.mAddress = myTrackId;
                                myNewFittingParameter.mSourceType = gSourceType;

                                myNewFittingParameter.mRange = (int)Convert.ToDecimal(myValueString[1]);

                                myNewFittingParameter.mBinSize = (int)myNewFittingParameter.mRange / 4096;

                                myNewFittingParameter.mChiSqr = (float)Convert.ToDecimal(myValueString[2]);
                                myNewFittingParameter.mLineIntercept = (float)Convert.ToDecimal(myValueString[3]);
                                myNewFittingParameter.mLineSlope = (float)Convert.ToDecimal(myValueString[4]);

                                /*
                                myNewFittingParameter.mG1Amplitude = (float)Convert.ToDecimal(myValueString[5]);
                                myNewFittingParameter.mG1Sigma = (float)Convert.ToDecimal(myValueString[6]);
                                myNewFittingParameter.mG1Center = (float)Convert.ToDecimal(myValueString[7]);
                                myNewFittingParameter.mG1Fwhm = (float)Convert.ToDecimal(myValueString[8]);
                                myNewFittingParameter.mG1Height = (float)Convert.ToDecimal(myValueString[9]);
                                myNewFittingParameter.mG2Amplitude = (float)Convert.ToDecimal(myValueString[10]);
                                myNewFittingParameter.mG2Sigma = (float)Convert.ToDecimal(myValueString[11]);
                                myNewFittingParameter.mG2Center = (float)Convert.ToDecimal(myValueString[12]);
                                myNewFittingParameter.mG2Fwhm = (float)Convert.ToDecimal(myValueString[13]);
                                myNewFittingParameter.mG2Height = (float)Convert.ToDecimal(myValueString[14]);
                                myNewFittingParameter.mG3Amplitude = (float)Convert.ToDecimal(myValueString[15]);
                                myNewFittingParameter.mG3Sigma = (float)Convert.ToDecimal(myValueString[16]);
                                myNewFittingParameter.mG3Center = (float)Convert.ToDecimal(myValueString[17]);
                                myNewFittingParameter.mG3Fwhm = (float)Convert.ToDecimal(myValueString[18]);
                                myNewFittingParameter.mG3Height = (float)Convert.ToDecimal(myValueString[19]);
                                myNewFittingParameter.mG2Error = (float)Convert.ToDecimal(myValueString[20]);
                                */


                                myNewFittingParameter.mG2Amplitude = (float)Convert.ToDecimal(myValueString[5]);
                                myNewFittingParameter.mG2Sigma = (float)Convert.ToDecimal(myValueString[6]);
                                myNewFittingParameter.mG2Center = (float)Convert.ToDecimal(myValueString[7]);
                                myNewFittingParameter.mG2Fwhm = (float)Convert.ToDecimal(myValueString[8]);
                                myNewFittingParameter.mG2Height = (float)Convert.ToDecimal(myValueString[9]);
                                myNewFittingParameter.mG2Error = (float)Convert.ToDecimal(myValueString[10]);

                                myNewFittingParameter.mEnergyResolution = (float)(1.0 * 100 * myNewFittingParameter.mG2Fwhm / myNewFittingParameter.mG2Center);

                                myNewFittingParameter.mStatus = (int)cFittingParameters.eStatus.OK;

                
                                if (!gDictFittingParameters.ContainsKey(myNewFittingParameter.mAddress)) {

                                    myNoLightShareMaxPixelPerLine++;

                                    if( myNewFittingParameter.mAddress / 16 != myPixelIndexX ) {

                                        myPixelIndexX = myNewFittingParameter.mAddress / 16;

                                        if( myNoLightShareMaxPixelPerLine > gParenetWindows.gMaxPixelPerLine ) {

                                            gParenetWindows.gMaxPixelPerLine = myNoLightShareMaxPixelPerLine;

                                            myNoLightShareMaxPixelPerLine = 0;

                                        }

                                        if( gDictFittingParameters.Count == 0 ) {

                                            //gParenetWindows.gNewLinePixel.Add( myPreviousPixelNo, 0 );

                                        } else {

                                            if( !gParenetWindows.gNewLinePixel.ContainsKey( myPreviousPixelNo ) ) {

                                                gParenetWindows.gNewLinePixel.Add( myPreviousPixelNo, 0 );

                                            }

                                        }

                                    }

                                    gDictFittingParameters.Add( myNewFittingParameter.mAddress, myNewFittingParameter );

                                    myPreviousPixelNo = myNewFittingParameter.mAddress;
                                
                                }

                            }

                            #endregion 

                        } else if (gSourceType == (int)cScanParameters.eSourceType.CsBgo) {

                            //The CS are 14 fileds
                            #region CS/BGO 

                            if (myValueString.Length == gkCsBGOFiledCount ) {

                                cFittingParameters myNewFittingParameter = new cFittingParameters();

                                myValueString[gkCsBGOFiledCount - 1] = myValueString[gkCsBGOFiledCount-1].Replace(">", "");

                                myNewFittingParameter.mAddress = myTrackId;
                                myNewFittingParameter.mSourceType = gSourceType;

                                myNewFittingParameter.mRange = (int)Convert.ToDecimal(myValueString[1]);

                                myNewFittingParameter.mBinSize = (int)myNewFittingParameter.mRange / 4096;

                                myNewFittingParameter.mChiSqr = (float)Convert.ToDecimal(myValueString[2]);
                                myNewFittingParameter.mG1Amplitude = (float)Convert.ToDecimal(myValueString[3]);
                                myNewFittingParameter.mG1Sigma = (float)Convert.ToDecimal(myValueString[4]);
                                myNewFittingParameter.mG1Center = (float)Convert.ToDecimal(myValueString[5]);
                                myNewFittingParameter.mG1Fwhm = (float)Convert.ToDecimal(myValueString[6]);
                                myNewFittingParameter.mG1Height = (float)Convert.ToDecimal(myValueString[7]);
                                myNewFittingParameter.mG2Amplitude = (float)Convert.ToDecimal(myValueString[8]);
                                myNewFittingParameter.mG2Sigma = (float)Convert.ToDecimal(myValueString[9]);
                                myNewFittingParameter.mG2Center = (float)Convert.ToDecimal(myValueString[10]);
                                myNewFittingParameter.mG2Fwhm = (float)Convert.ToDecimal(myValueString[11]);
                                myNewFittingParameter.mG2Height = (float)Convert.ToDecimal(myValueString[12]);
                                myNewFittingParameter.mG2Error = (float)Convert.ToDecimal(myValueString[13]);

                                myNewFittingParameter.mEnergyResolution = (float)(1.0 * 100 * myNewFittingParameter.mG2Fwhm / myNewFittingParameter.mG2Center);

                                myNewFittingParameter.mStatus = (int)cFittingParameters.eStatus.OK;

      							if(!gDictFittingParameters.ContainsKey(myNewFittingParameter.mAddress)) {

                                    gDictFittingParameters.Add(myNewFittingParameter.mAddress, myNewFittingParameter);

                                    myNoLightShareMaxPixelPerLine++;

                                    if( myNewFittingParameter.mAddress % 16 == 0 ) {

                                        if( myNoLightShareMaxPixelPerLine > gParenetWindows.gMaxPixelPerLine ) {

                                            gParenetWindows.gMaxPixelPerLine = myNoLightShareMaxPixelPerLine;

                                        }

                                    }
                                
                                }

                            }

                            #endregion


                        } else if( gSourceType == ( int )cScanParameters.eSourceType.LightShare ) { 
                        
                            //The light share has 14 fileds
                            #region Light Share

                            if( myValueString.Length == gkLightShareFiledCount ) {

                                cFittingParameters myNewFittingParameter = new cFittingParameters( );

                                myValueString[gkLightShareFiledCount - 1] = myValueString[gkLightShareFiledCount - 1].Replace( ">", "" );

                                myNewFittingParameter.mAddress = myTrackId;

                                if (gParenetWindows.gSelectedScanParametersForDataAcq.gSourceType == (int)cScanParameters.eSourceType.LightShareSiglePixel) {

                                    //Because fitting reult will order it
                                    myPixelPosition = myNewFittingParameter.mAddress;//gParenetWindows.gPixelNumToPositionMap[myNewFittingParameter.mAddress];

                                    myCalibratedSlope = gParenetWindows.gSinglePixelCalibrationBuffer.mCalibrationBuffer[myPixelPosition, 0];

                                    myCalibratedOffset = gParenetWindows.gSinglePixelCalibrationBuffer.mCalibrationBuffer[myPixelPosition, 1];
                                
                                }

                                myNewFittingParameter.mSourceType = gSourceType;

                                myNewFittingParameter.mRange = ( int )Convert.ToDecimal( myValueString[gkLightShareFiledCount - 13] );

                                myNewFittingParameter.mBinSize = ( int )myNewFittingParameter.mRange / 256;

                                myNewFittingParameter.mChiSqr = ( float )Convert.ToDecimal( myValueString[gkLightShareFiledCount - 12] );


                                myNewFittingParameter.mG1Amplitude = ( float )Convert.ToDecimal( myValueString[gkLightShareFiledCount - 11] );
                                myNewFittingParameter.mG1Sigma = ( float )Convert.ToDecimal( myValueString[gkLightShareFiledCount - 10] );
                                myNewFittingParameter.mG1Center = ( float )Convert.ToDecimal( myValueString[gkLightShareFiledCount - 9] );
                                myNewFittingParameter.mG1Fwhm = ( float )Convert.ToDecimal( myValueString[gkLightShareFiledCount - 8] );
                                myNewFittingParameter.mG1Height = ( float )Convert.ToDecimal( myValueString[gkLightShareFiledCount - 7] );

                                myNewFittingParameter.mG2Amplitude = ( float )Convert.ToDecimal( myValueString[gkLightShareFiledCount - 6] );
                                myNewFittingParameter.mG2Sigma = ( float )Convert.ToDecimal( myValueString[gkLightShareFiledCount - 5] );
                                myNewFittingParameter.mG2Center = ( float )Convert.ToDecimal( myValueString[gkLightShareFiledCount - 4] );

                                if (gParenetWindows.gSelectedScanParametersForDataAcq.gSourceType == (int)cScanParameters.eSourceType.LightShareSiglePixel) {

                                    myNewFittingParameter.mG2Center = myNewFittingParameter.mG2Center * myCalibratedSlope + myCalibratedSlope;
                                
                                }

                                myNewFittingParameter.mG2Fwhm = ( float )Convert.ToDecimal( myValueString[gkLightShareFiledCount - 3] );
                                myNewFittingParameter.mG2Height = ( float )Convert.ToDecimal( myValueString[gkLightShareFiledCount - 2] );

                                //myNewFittingParameter.mG3Amplitude = ( float )Convert.ToDecimal( myValueString[gkLightShareFiledCount - 6] );
                                //myNewFittingParameter.mG3Sigma = ( float )Convert.ToDecimal( myValueString[gkLightShareFiledCount - 5] );
                                //myNewFittingParameter.mG3Center = ( float )Convert.ToDecimal( myValueString[gkLightShareFiledCount - 4] );
                                //myNewFittingParameter.mG3Fwhm = ( float )Convert.ToDecimal( myValueString[gkLightShareFiledCount - 3] );
                                //myNewFittingParameter.mG3Height = ( float )Convert.ToDecimal( myValueString[gkLightShareFiledCount - 2] );
                                myNewFittingParameter.mG2Error = ( float )Convert.ToDecimal( myValueString[gkLightShareFiledCount - 1] );

                                myNewFittingParameter.mEnergyResolution = ( float )( 1.0 * 100 * myNewFittingParameter.mG2Fwhm / myNewFittingParameter.mG2Center );

                                myNewFittingParameter.mStatus = ( int )cFittingParameters.eStatus.OK;


                                if( gParenetWindows.gLightSharePixelNoArray.TryDequeue( out myNewFittingParameter.mAddress ) ) {

                                    if( !gDictFittingParameters.ContainsKey( myNewFittingParameter.mAddress ) ) {

                                        gDictFittingParameters.Add( myNewFittingParameter.mAddress, myNewFittingParameter );
                                    
                                    }
                                
                                }

                            }

                            #endregion 
                        
                        
                        }



                    }

                    #endregion

                    #region Exception for it

                    if (myLineContent.Contains(gkExceptionString)) {

                        cFittingParameters myNewFittingParameter = new cFittingParameters();

                        myNewFittingParameter.mAddress = myTrackId;

                        myNewFittingParameter.mStatus = (int)cFittingParameters.eStatus.Exception;

                        if( gParenetWindows.gLightSharePixelNoArray.TryDequeue( out myNewFittingParameter.mAddress ) ) {

                            if( !gDictFittingParameters.ContainsKey( myNewFittingParameter.mAddress ) ) {

                                gDictFittingParameters.Add( myNewFittingParameter.mAddress, myNewFittingParameter );

                            }

                        }

                    }

                    #endregion

                    #region Error for it

                    if( myLineContent.Contains( gkErrorString ) ) {

                        if( myLineContent.Contains( "error, aa! less number of pixels" ) ) {
                         
                            gErrorCode = cErrorCode.gkecFitting_PixelNoMatch;
                        
                        }
                    
                    
                    }


                    #endregion

                }

            }


            return myStatus;

        }

        public int FillErroCode( ) {

            int myStatus = 0;

            int myStartLineIndex = 0;

            int myEndOfLineIndex = 0;

            string myLineContent = "";

            gErrorCode = 0;

            if( gPythonTerminalOutputString.Contains( gkFittingStartString ) ) {

                //Get the start of fitting line
                myStartLineIndex = gPythonTerminalOutputString.IndexOf( gkFittingStartString );

                //Get all line before the fitting line 
                gPythonTerminalOutputString = gPythonTerminalOutputString.Substring( myStartLineIndex );

                //Find the end line character of first line
                myEndOfLineIndex = gPythonTerminalOutputString.IndexOf( '>' );

                //Get the line content 
                myLineContent = gPythonTerminalOutputString.Substring( 0, myEndOfLineIndex + 1 );

                //Get rid of read section
                gPythonTerminalOutputString = gPythonTerminalOutputString.Substring( myEndOfLineIndex + 1 );


                if( myLineContent.ToLower( ).Contains( gkLuLYSOHeader ) ) {

                    gSourceType = ( int )cScanParameters.eSourceType.Lu_307kev;

                } else if( myLineContent.ToLower( ).Contains( gkCsBGOHeader ) ) {

                    gSourceType = ( int )cScanParameters.eSourceType.CsBgo;

                } else if( myLineContent.ToLower( ).Contains( gkLightShareHeader ) ) {

                    gSourceType = ( int )cScanParameters.eSourceType.LightShare;

                }

                while( !myLineContent.StartsWith( "}" ) && myLineContent.Length > 0 ) {

                    //Now start to read the real data
                    myEndOfLineIndex = gPythonTerminalOutputString.IndexOf( '>' );

                    myLineContent = gPythonTerminalOutputString.Substring( 0, myEndOfLineIndex + 1 );

                    gPythonTerminalOutputString = gPythonTerminalOutputString.Substring( myEndOfLineIndex + 1 );

                    #region Error for it

                    if( myLineContent.Contains( gkErrorString ) ) {

                        if( myLineContent.Contains( "error, aa! less number of pixels" ) ) {

                            gErrorCode = cErrorCode.gkecFitting_PixelNoMatch;

                        }


                    }


                    #endregion

                }


            }



            return myStatus;

        }


        public int FillErroCode2( ) {

            int myStatus = 0;

            int myStartLineIndex = 0;

            int myEndOfLineIndex = 0;

            string myLineContent = "";

            if( gPythonTerminalOutputString.Contains( gkFittingStartString ) ) {

                //Yes, this is a complete output message

                //Get the start of the main content 
                myStartLineIndex = gPythonTerminalOutputString.IndexOf( gkFittingStartString );

                gPythonTerminalOutputString = gPythonTerminalOutputString.Substring( myStartLineIndex );

                //After here, the captured output string is the real data section 

                //Reset start index
                myStartLineIndex = 0;

                //Find the end line character of first line
                myEndOfLineIndex = gPythonTerminalOutputString.IndexOf( '>' );

                //Get the first line 
                myLineContent = gPythonTerminalOutputString.Substring( 0, myEndOfLineIndex + 1 );

                //Get rid of read section
                gPythonTerminalOutputString = gPythonTerminalOutputString.Substring( myEndOfLineIndex + 1 );

                //End of print line is "}>"

                int myTrackId = 0;

                while( !myLineContent.StartsWith( "}" ) && myLineContent.Length > 0 ) {

                    //Now start to read the real data
                    myEndOfLineIndex = gPythonTerminalOutputString.IndexOf( '>' );

                    myLineContent = gPythonTerminalOutputString.Substring( 0, myEndOfLineIndex + 1 );

                    gPythonTerminalOutputString = gPythonTerminalOutputString.Substring( myEndOfLineIndex + 1 );

                    #region Exception for it

                    if( myLineContent.Contains( gkExceptionString ) ) {

                        cFittingParameters myNewFittingParameter = new cFittingParameters( );

                        myNewFittingParameter.mAddress = myTrackId;

                        myNewFittingParameter.mStatus = ( int )cFittingParameters.eStatus.Exception;

                        if( !gDictFittingParameters.ContainsKey( myNewFittingParameter.mAddress ) ) {

                            gDictFittingParameters.Add( myNewFittingParameter.mAddress, myNewFittingParameter );

                        }

                    }

                    #endregion

                    #region Error for it

                    if( myLineContent.Contains( gkErrorString ) ) {

                        if( myLineContent.Contains( "error, aa! less number of pixels" ) ) {

                            gErrorCode = cErrorCode.gkecFitting_PixelNoMatch;

                        }


                    }


                    #endregion

                }

            }

            gPythonTerminalOutputString = "";

            return myStatus;

        }


        #endregion

        #region FillFittingFormula

        public float FillFittingFormula(cFittingParameters pFittingParameters, float pX) {

            float myReult = 0f;
            float myG1Result = 0f;
            float myG2Result = 0f;
            float myG3Result = 0f;
            float myLResult = 0f;

            //myReult = G1 + G2 + G3 + L

            /*
            //[-(x-u)*(x-u)/(2*sigma*sigma)]
            myReult = (float)(1.0 * ( -(pX-pFittingParameters.mG1Center) * (pX - pFittingParameters.mG1Center) ) / ( 2 * pFittingParameters.mG1Sigma * pFittingParameters.mG1Sigma ) );

            myReult = (float)(1.0 * pFittingParameters.mG1Amplitude / ((float)(pFittingParameters.mG1Sigma * Math.Sqrt((double)(2 * Math.PI)))) * Math.Pow((double)Math.E, (double)myReult) ); 

            //G1 is done
            myG1Result = myReult;

            myReult = (float)(1.0 * (-(pX - pFittingParameters.mG2Center) * (pX - pFittingParameters.mG2Center)) / (2 * pFittingParameters.mG2Sigma * pFittingParameters.mG2Sigma));

            myReult = (float)(1.0 * pFittingParameters.mG2Amplitude / ((float)(pFittingParameters.mG2Sigma * Math.Sqrt((double)(2 * Math.PI)))) * Math.Pow((double)Math.E, (double)myReult));

            //G2 is done
            myG2Result = myReult;

            myReult = (float)(1.0 * (-(pX - pFittingParameters.mG3Center) * (pX - pFittingParameters.mG3Center)) / (2 * pFittingParameters.mG3Sigma * pFittingParameters.mG3Sigma));

            myReult = (float)(1.0 * pFittingParameters.mG3Amplitude / ((float)(pFittingParameters.mG3Sigma * Math.Sqrt((double)(2 * Math.PI)))) * Math.Pow((double)Math.E, (double)myReult));

            //G3 is done
            myG3Result = myReult;

            myReult = (float)(1.0 * pFittingParameters.mLineSlope*pX+ pFittingParameters.mLineIntercept);

            myLResult = myReult;
            */

            myG1Result = 0f;

            myReult = (float)(1.0 * (-(pX - pFittingParameters.mG2Center) * (pX - pFittingParameters.mG2Center)) / (2 * pFittingParameters.mG2Sigma * pFittingParameters.mG2Sigma));

            myReult = (float)(1.0 * pFittingParameters.mG2Amplitude / ((float)(pFittingParameters.mG2Sigma * Math.Sqrt((double)(2 * Math.PI)))) * Math.Pow((double)Math.E, (double)myReult));

            //G2 is done
            myG2Result = myReult;

            //G3 is done
            myG3Result = 0;

            myReult = (float)(1.0 * pFittingParameters.mLineSlope * pX + pFittingParameters.mLineIntercept);

            myLResult = myReult;


            myReult = myG1Result + myG2Result + myG3Result + myLResult;

            return myReult;

        }

        public float FillFittingFormula(cFittingParameters pFittingParameters, float pX, out float pY, out float pG1, out float pG2, out float pG3, out float pLinear, out float pG2Peak, out float pG2PeakXValue) {

            pY = 0f;
            pG1 = 0f;
            pG2 = 0f;
            pG3 = 0f;
            pLinear = 0f;
            pG2Peak = 0f;
            pG2PeakXValue = 0f;

            float myReult = 0f;
            float myG1Result = 0f;
            float myG2Result = 0f;
            float myG3Result = 0f;
            float myLResult = 0f;

            //myReult = G1 + G2 + G3 + L

            /*
            //[-(x-u)*(x-u)/(2*sigma*sigma)]
            myReult = (float)(1.0 * (-(pX - pFittingParameters.mG1Center) * (pX - pFittingParameters.mG1Center)) / (2 * pFittingParameters.mG1Sigma * pFittingParameters.mG1Sigma));

            myReult = (float)(1.0 * pFittingParameters.mG1Amplitude / ((float)(pFittingParameters.mG1Sigma * Math.Sqrt((double)(2 * Math.PI)))) * Math.Pow((double)Math.E, (double)myReult));

            //G1 is done
            myG1Result = myReult;
             * */

            myG1Result = 0f;

            myReult = (float)(1.0 * (-(pX - pFittingParameters.mG2Center) * (pX - pFittingParameters.mG2Center)) / (2 * pFittingParameters.mG2Sigma * pFittingParameters.mG2Sigma));

            myReult = (float)(1.0 * pFittingParameters.mG2Amplitude / ((float)(pFittingParameters.mG2Sigma * Math.Sqrt((double)(2 * Math.PI)))) * Math.Pow((double)Math.E, (double)myReult));

            pG2Peak = 0.3989f * pFittingParameters.mG2Amplitude / pFittingParameters.mG2Sigma;

            //G2 is done
            myG2Result = myReult;

            if (pFittingParameters.mSourceType == (int)cScanParameters.eSourceType.Lu_307kev) {

                #region LYSO G1+G2+G2+L

                myReult = (float)(1.0 * (-(pX - pFittingParameters.mG3Center) * (pX - pFittingParameters.mG3Center)) / (2 * pFittingParameters.mG3Sigma * pFittingParameters.mG3Sigma));

                myReult = (float)(1.0 * pFittingParameters.mG3Amplitude / ((float)(pFittingParameters.mG3Sigma * Math.Sqrt((double)(2 * Math.PI)))) * Math.Pow((double)Math.E, (double)myReult));

                //G3 is done
                myG3Result = 0f;

                myReult = (float)(1.0 * pFittingParameters.mLineSlope * pX + pFittingParameters.mLineIntercept);

                myLResult = myReult;

                myReult = myG1Result + myG2Result + myG3Result + myLResult;

                #endregion

            } else if ( (pFittingParameters.mSourceType == (int)cScanParameters.eSourceType.CsBgo) ||
                        (pFittingParameters.mSourceType == (int)cScanParameters.eSourceType.CsLyso) ) {

                #region Cs_BGO G1+G2

                myReult = myG1Result + myG2Result;

                #endregion

            } else if( pFittingParameters.mSourceType == ( int )cScanParameters.eSourceType.LightShare ) {

                #region Light Share G1+G2+G2
                
                //Start G1
                //[-(x-u)*(x-u)/(2*sigma*sigma)]
                myReult = (float)(1.0 * (-(pX - pFittingParameters.mG1Center) * (pX - pFittingParameters.mG1Center)) / (2 * pFittingParameters.mG1Sigma * pFittingParameters.mG1Sigma));

                myReult = (float)(1.0 * pFittingParameters.mG1Amplitude / ((float)(pFittingParameters.mG1Sigma * Math.Sqrt((double)(2 * Math.PI)))) * Math.Pow((double)Math.E, (double)myReult));

                //G1 is done
                myG1Result = myReult;

                //Start G3
                myReult = ( float )( 1.0 * ( -( pX - pFittingParameters.mG3Center ) * ( pX - pFittingParameters.mG3Center ) ) / ( 2 * pFittingParameters.mG3Sigma * pFittingParameters.mG3Sigma ) );

                myReult = ( float )( 1.0 * pFittingParameters.mG3Amplitude / ( ( float )( pFittingParameters.mG3Sigma * Math.Sqrt( ( double )( 2 * Math.PI ) ) ) ) * Math.Pow( ( double )Math.E, ( double )myReult ) );

                //G3 is done
                myG3Result = 0f;

                myReult = ( float )( 1.0 * pFittingParameters.mLineSlope * pX + pFittingParameters.mLineIntercept );

                myLResult = myReult;

                myReult = myG1Result + myG2Result + myG3Result;

                #endregion
            
            }

            pY = myReult * gParenetWindows.gBinSize / pFittingParameters.mBinSize;
            pG1 = myG1Result * gParenetWindows.gBinSize / pFittingParameters.mBinSize;
            pG2 = myG2Result * gParenetWindows.gBinSize / pFittingParameters.mBinSize;
            pG3 = myG3Result * gParenetWindows.gBinSize / pFittingParameters.mBinSize;
            pLinear = myLResult * gParenetWindows.gBinSize / pFittingParameters.mBinSize;



            return myReult;

        }

        public float FillFirstGauss(cFittingParameters pFittingParameters, float pX) {

            float myReult = 0f;

            //[-(x-u)*(x-u)/(2*sigma*sigma)]
            myReult = (float)(1.0 * (-(pX - pFittingParameters.mG1Center) * (pX - pFittingParameters.mG1Center)) / (2 * pFittingParameters.mG1Sigma * pFittingParameters.mG1Sigma));

            myReult = (float)(1.0 * pFittingParameters.mG1Amplitude / ((float)(pFittingParameters.mG1Sigma * Math.Sqrt((double)(2 * Math.PI)))) * Math.Pow((double)Math.E, (double)myReult));

            return myReult;
        
        }

        public float FillSecondGauss(cFittingParameters pFittingParameters, float pX) {

            float myReult = 0f;

            //[-(x-u)*(x-u)/(2*sigma*sigma)]
            myReult = (float)(1.0 * (-(pX - pFittingParameters.mG2Center) * (pX - pFittingParameters.mG2Center)) / (2 * pFittingParameters.mG2Sigma * pFittingParameters.mG2Sigma));

            myReult = (float)(1.0 * pFittingParameters.mG2Amplitude / ((float)(pFittingParameters.mG2Sigma * Math.Sqrt((double)(2 * Math.PI)))) * Math.Pow((double)Math.E, (double)myReult));

            return myReult;

        }

        public float FillThirdGauss(cFittingParameters pFittingParameters, float pX) {

            float myReult = 0f;

            //[-(x-u)*(x-u)/(2*sigma*sigma)]
            myReult = (float)(1.0 * (-(pX - pFittingParameters.mG3Center) * (pX - pFittingParameters.mG3Center)) / (2 * pFittingParameters.mG3Sigma * pFittingParameters.mG3Sigma));

            myReult = (float)(1.0 * pFittingParameters.mG3Amplitude / ((float)(pFittingParameters.mG3Sigma * Math.Sqrt((double)(2 * Math.PI)))) * Math.Pow((double)Math.E, (double)myReult));

            return myReult;

        }

        public float FillLinear(cFittingParameters pFittingParameters, float pX) {

            float myReult = 0f;

            myReult = (float)(1.0 * pFittingParameters.mLineSlope * pX + pFittingParameters.mLineIntercept);

            return myReult;

        }

        #endregion

        #region GetEnergyResolution

        public int GetEnergyResolution(cFittingParameters pFittingParameters, out float pEnergyResolution) {

            int myStatus = 0;

            pEnergyResolution = 0;

            //energy resolution = 100*g2_fwhm/g2_center
            if (pFittingParameters.mStatus == (int)cFittingParameters.eStatus.OK) {

                pEnergyResolution = (float)(1.0 * 100 * pFittingParameters.mG2Fwhm / pFittingParameters.mG2Center);

            } else {

                myStatus = cErrorCode.gkecFitting_NoFittingResult;
            
            }

            return myStatus;

        }

        #endregion

        #region LoadFittingPython

        public static int LoadFittingPython(int pSourceType, out string  pPythonFile, out string pFittingFileUsed, out string pCountMapGenerator) { 
        
            int myStatus = 0;

            pPythonFile = "";

            pFittingFileUsed = "";

            pCountMapGenerator = "";

            switch (pSourceType) { 
            
                case (int)cScanParameters.eSourceType.Lu_307kev:

                    pPythonFile = cScanParameters.gkFittingPythonLuLysoUsingBin;

                    pFittingFileUsed = gkFittingLuLyso;

                    break;

                case (int)cScanParameters.eSourceType.CsBgo:

                    pPythonFile = cScanParameters.gkFittingPythonCsBGOUsingBin;

                    pFittingFileUsed = gkFittingCsBGOFile;

                    break;

                case (int)cScanParameters.eSourceType.Lu_SinglePixel:

                    pPythonFile = cScanParameters.gkFittingPythonLuSinglePixelUsingBin;

                    pFittingFileUsed = gkFittingLuSinglePixel;

                    break;

                case (int)cScanParameters.eSourceType.CsLyso:

                    pPythonFile = cScanParameters.gkFittingPythonCsLysoUsingBin;

                    pFittingFileUsed = gkFittingCsLyso;

                    break;

                case (int)cScanParameters.eSourceType.UseMaxPeak:

                    pPythonFile = cScanParameters.gkFittingPythonUsingMaxPeakUsingBin;

                    pFittingFileUsed = gkFittingMaxPeak;

                    break;

                case (int)cScanParameters.eSourceType.LightShare:

                    pPythonFile = cScanParameters.gkFittingPythonUsingLightShareUsingBin;

                    pCountMapGenerator = cScanParameters.gkCountMapGenetor;

                    pFittingFileUsed = gkFittingLightShare;

                    break;

                case (int)cScanParameters.eSourceType.LightShare196Version:

                    pPythonFile = cScanParameters.gkFittingPythonUsingLightShare196VersionUsingBin;

                    pCountMapGenerator = cScanParameters.gkCountMapGenetor196Version;

                    pFittingFileUsed = gkFittingLightShare196Version;

                    break;

                case (int)cScanParameters.eSourceType.LightShareSiglePixel:

                    pPythonFile = cScanParameters.gkFittingPythonUsingLightShareSinglePixelUsingBin;

                    pCountMapGenerator = cScanParameters.gkCountMapGenetorSinglePixel;

                    pFittingFileUsed = gkFittingLightShareSinglePixel;

                    break;

            
            
            }
            
            
            return myStatus;

        }

        #endregion

    }

    public class cFittingParameters {

        public enum eStatus { OK = 0, Unknown = -1, Exception = -2, NoResult = -3, ManualFit = 1};

        public int mStatus;
        public float mEnergyResolution;

        public int mAddress;
        public int mSourceType;
        public int mBinSize;

        public int mRange;
        public float mChiSqr;
        public float mLineIntercept;
        public float mLineSlope;
        public float mG1Amplitude;
        public float mG1Sigma;
        public float mG1Center;
        public float mG1Fwhm;
        public float mG1Height;
        public float mG2Amplitude;
        public float mG2Sigma;
        public float mG2Center;
        public float mG2Fwhm;
        public float mG2Height;
        public float mG3Amplitude;
        public float mG3Sigma;
        public float mG3Center;
        public float mG3Fwhm;
        public float mG3Height;
        public float mG2Error;

    }

    public class cFittingSourceOption {

        //Which type of source does this fitting using
        public cScanParameters.eSourceType mSourceType = cScanParameters.eSourceType.Unknown;

        public string mFittingFileName = "";
        public string mFittingFileHeader = "";
        public int mFittingParameterFieldCount = 0;

    }

}
