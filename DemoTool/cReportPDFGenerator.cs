using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;

using System.Windows.Forms.DataVisualization.Charting;

using DemoTool;


namespace DemoTool {

    class cReportPDFGenerator {

        /// <summary>
        /// Notes: For the final merged report, there are two situations:
        /// 1. Users selected no resolution in report, then in final report, no matter if the original report has the resolution or not
        /// then don't include the resolution in final report.
        /// 2. Users selected has resolution in report, then in final repost, if there is any files don't have resolution, then mardked in
        /// final resport and don't calculate the resolution Min, Max and Avg
        /// </summary>

        #region Constants

        static public int gkEnergyResolution = 1;
        static public int gkEnergyPeak = 2;
        static public int gkEnergyCount = 3;

        private const float gkDefaultCellHeight = 20f;

        private const float gkDefaultNanFloat = -512.0f;

        private const string gkEnglishYellow = "Greater than up limit: ";
        private const string gkChineseYellow = "高于上限：";

        private const string gkEnglishRed = "Less than low limit: ";
        private const string gkChineseRed = "低于下限：";

        private const string gkEnglishInRange = "In range: ";
        private const string gkChineseInRange = "属于范围：";

        private const string gkTableHeaderENG = "TOFTEK Results Report";
        private const string gkTableHeaderCHA = "无锡通透光电科技有限公司测试报告";

        private const string gkRedENG = "Red";
        private const string gkRedCHA = "红色";

        private const string gkGreenENG = "Green";
        private const string gkGreenCHA = "绿色";

        private const string gkYellowENG = "Yellow";
        private const string gkYellowCHA = "黄色";

        private const string gkEnergyNotesENG = "Energy Min, Max, Avg";
        private const string gkEnergyNotesCHA = "能量值: 最小, 最大, 平均值";

        private const string gkEnergyResolutionNotesENG = "Resolution Min, Max, Avg";
        private const string gkEnergyResolutionNotesCHA = "能量分辨率: 最小, 最大, 平均值";

        private const string gkDataCellNotesENG = "Value/" + "\n" + "Resolution/" + "\n" + "Count";
        private const string gkDataCellNotesCHA = "能量值/" + "\n" + "分辨率" + "\n" + "能量计数";

        private const string gkDataCellNotesValueENG = "Value";
        private const string gkDataCellNotesValueCHA = "能量值";

        private const string gkDataCellNotesResolutionENG = "Resolution";
        private const string gkDataCellNotesResolutionCHA ="分辨率";

        private const string gkDataCellNotesCountENG = "Count";
        private const string gkDataCellNotesCountCHA = "能量计数";

        private const string gkEnergyCountSummaryNotesENG = "Count Min, Max, Avg";
        private const string gkEnergyCountSummaryNotesCHA = "能量计数: 最小, 最大, 平均值";

        private const string gkCountSummaryNotesENG = "Count of Pixels: In Range, Exceed up limit, Less than low limit";
        private const string gkCountSummaryNotesCHA = "晶体数量：在范围，高于上限，低于下限";

        private const string gkPixelIndexInfoENG = "Index";
        private const string gkPixelIndexInfoCHA = "序号";

        private const string gkPixelEnergyInfoENG = "Energy";
        private const string gkPixelEnergyInfoCHA = "能量值";

        private const string gkPixelResolutionInfoENG = "Resolution";
        private const string gkPixelResolutionInfoCHA = "分辨率";

        private const string gkPixelCountInfoENG = "Count";
        private const string gkPixelCountInfoCHA = "能量计数";

        private const string gkCanNotCalculateResolutionENG = "Can not summarize resolution（Some reports don't contain resolution）";
        private const string gkCanNotCalculateResolutionCHA = "无法统计能量分辨率（有报告不包含能量分辨率）";

        private const string gkLookupTableEnergyMinInfoENG = "能量最小值";
        private const string gkLookupTableEnergyMinInfoCHA = "Energy Min";

        private const string gkLookupTableEnergyMaxInfoENG = "能量最大值";
        private const string gkLookupTableEnergyMaxInfoCHA = "Energy Max";

        private const string gkLookupTableResolutionMinInfoENG = "分辨率最小值";
        private const string gkLookupTableResolutionMinInfoCHA = "Resolution Min";

        private const string gkLookupTableResolutionMaxInfoENG = "分辨率最大值";
        private const string gkLookupTableResolutionMaxInfoCHA = "Resolution Max";

        private const string gkQualifiedEnergyCountNoteENG = "Qualified Energy Range";
        private const string gkQualifiedEnergyCountNoteCHA = "能量合格标准";

        private const string gkQualifedEnergyResolutionNoteENG = "Qualifed Energy Resolution Range";
        private const string gkQualifedEnergyResolutionNoteCHA = "能量分辨率合格标准";

        private const string gkResolutionGreyPicHeaderENG = "Resolution Level Table";
        private const string gkResolutionGreyPicHeaderCHA = "能量分辨率灰度表";
        private const string gkEnergyGreyPicHeaderENG = "Energy Peak Level Table";
        private const string gkEnergyGreyPicHeaderCHA = "能量灰度表";
        private const string gkCountGreyPicHeaderENG = "Energy Count Level Table";
        private const string gkCountGreyPicHeaderCHA = "能量计数灰度表";
        #endregion

        public string gTableHeader = "";
        public string gRed = "";
        public string gGreen = "";
        public string gYellow = "";
        private string gRedNoteMessage = "";
        private string gYellowNoteMessage = "";
        private string gInRangeMessage = "";
        private string gEnergyNotes = "";
        private string gEnergyResolutionNotes = "";
        private string gDataCellNotes = "";
        private string gCountSummaryNotes = "";
        private string gEnergyCountSummaryNotes = "";
        private string gPixelIndexInfo = gkPixelEnergyInfoENG;
        private string gPixelEnergyInfo = gkPixelEnergyInfoENG;
        private string gPixelResolutionInfo = gkPixelResolutionInfoENG;
        private string gPixelCountInfo = gkPixelCountInfoENG;

        private string gLookupTableEnergyMinInfo = gkLookupTableEnergyMinInfoENG;
        private string gLookupTableEnergyMaxInfo = gkLookupTableEnergyMaxInfoENG;
        private string gLookupTableResolutionMinInfo = gkLookupTableResolutionMinInfoENG;
        private string gLookupTableResolutionMaxInfo = gkLookupTableResolutionMaxInfoENG;

        private string gQualifiedEnergyCountNote = gkQualifiedEnergyCountNoteENG;
        private string gQualifedEnergyResolutionNote = gkQualifedEnergyResolutionNoteENG;

        private string gResolutionGreyPicHeader = gkResolutionGreyPicHeaderENG;
        private string gEnergyGreyPicHeader = gkEnergyGreyPicHeaderENG;
        private string gCountGreyPicHeader = gkCountGreyPicHeaderENG;

        private string gPeakLowLimit = "";

        private string gPeakHighLimit = "";

        public float gfPeakLowLimit = 0f;

        public float gfPeakHighLimit = 16384f;

        private System.IO.FileStream gReportFileStream;

        private Document gPDFDocument = new Document( PageSize.A4, 25, 25, 10, 10 );

        private PdfWriter gPDFWriter;

        private Demo gParentWindow;

        private Paragraph gPngParagraph;

        private int gTrackImagePerLine = 0;

        private int gMinimumDisplayEnergyCount = 10;

        private int gTrackPixelNo = -1;

        private int gQualifiedType = 0;

        BaseFont gBFTimeNewROMAN;

        public int gSourceType = ( int )cScanParameters.eSourceType.Unknown;

        #region For Final Result

        //The max bin size for the histogram is 20
        public int[] gHistogramDataCount = new int[20];

        public float gDelta = 0.0f;

        public float gMinTemperature = 0.0f;
        public float gMaxTempetature = 0.0f;

        public int gInRangeCount = 0;
        public int gUpRangeCount = 0;
        public int gDownRangeCount = 0;

        //Give min a very small value to get over write for first time
        public float gFinalResultEnergyMin = 65536.0f;
        public float gFinalResultEnergyMax = 0.0f;
        public float gFinalResultEnergyAvg = 0.0f;

        public float gFinalResultResolutionMin = 100.0f;
        public float gFinalResultResolutionMax = 0.0f;
        public float gFinalResultResolutionAvg = 0.0f;

        private int gTotalPixelCount = 0;

        private int gPdfGroupCount = 0;

        private PdfPTable gFinalResultTable;

        private PdfPTable gLookupTable;

        private int gLookupTableColumn;

        private int gFinalResultTableColumn;



        #endregion

        public cReportPDFGenerator( ) {

            //This is default construct

        }

        void InitializeLanguage( int pLanguage ) {

            if( pLanguage == ( int )Demo.gLanguageVersion.English ) {

                gRedNoteMessage = gkEnglishRed;
                gYellowNoteMessage = gkEnglishYellow;
                gInRangeMessage = gkEnglishInRange;
                gTableHeader = gParentWindow.gReportHeaderENG;
                gRed = gkRedENG;
                gGreen = gkGreenENG;
                gYellow = gkYellowENG;
                gEnergyNotes = gkEnergyNotesENG;
                gEnergyResolutionNotes = gkEnergyResolutionNotesENG;
                gDataCellNotes = gkDataCellNotesENG;
                gCountSummaryNotes = gkCountSummaryNotesENG;
                gPixelIndexInfo = gkPixelIndexInfoENG;
                gPixelEnergyInfo = gkPixelEnergyInfoENG;
                gPixelResolutionInfo = gkPixelResolutionInfoENG;
                gEnergyCountSummaryNotes = gkEnergyCountSummaryNotesENG;
                gPixelCountInfo = gkPixelCountInfoENG;
                gLookupTableEnergyMinInfo = gkLookupTableEnergyMinInfoENG;
                gLookupTableEnergyMaxInfo = gkLookupTableEnergyMaxInfoENG;
                gLookupTableResolutionMinInfo = gkLookupTableResolutionMinInfoENG;
                gLookupTableResolutionMaxInfo = gkLookupTableResolutionMaxInfoENG;
                gQualifiedEnergyCountNote = gkQualifiedEnergyCountNoteENG;
                gQualifedEnergyResolutionNote = gkQualifedEnergyResolutionNoteENG;
                gResolutionGreyPicHeader = gkResolutionGreyPicHeaderENG;
                gEnergyGreyPicHeader = gkEnergyGreyPicHeaderENG;
                gCountGreyPicHeader = gkCountGreyPicHeaderENG;

            } else if( pLanguage == ( int )Demo.gLanguageVersion.Chinese ) {

                gRedNoteMessage = gkChineseRed;
                gYellowNoteMessage = gkChineseYellow;
                gInRangeMessage = gkChineseInRange;
                gTableHeader = gParentWindow.gReportHeaderCHA;
                gRed = gkRedCHA;
                gGreen = gkGreenCHA;
                gYellow = gkYellowCHA;
                gEnergyNotes = gkEnergyNotesCHA;
                gEnergyResolutionNotes = gkEnergyResolutionNotesCHA;
                gDataCellNotes = gkDataCellNotesCHA;
                gCountSummaryNotes = gkCountSummaryNotesCHA;
                gPixelIndexInfo = gkPixelIndexInfoCHA;
                gPixelEnergyInfo = gkPixelEnergyInfoCHA;
                gPixelResolutionInfo = gkPixelResolutionInfoCHA;
                gEnergyCountSummaryNotes = gkEnergyCountSummaryNotesCHA;
                gPixelCountInfo = gkPixelCountInfoCHA;
                gLookupTableEnergyMinInfo = gkLookupTableEnergyMinInfoCHA;
                gLookupTableEnergyMaxInfo = gkLookupTableEnergyMaxInfoCHA;
                gLookupTableResolutionMinInfo = gkLookupTableResolutionMinInfoCHA;
                gLookupTableResolutionMaxInfo = gkLookupTableResolutionMaxInfoCHA;
                gQualifiedEnergyCountNote = gkQualifiedEnergyCountNoteCHA;
                gQualifedEnergyResolutionNote = gkQualifedEnergyResolutionNoteCHA;
                gResolutionGreyPicHeader = gkResolutionGreyPicHeaderCHA;
                gEnergyGreyPicHeader = gkEnergyGreyPicHeaderCHA;
                gCountGreyPicHeader = gkCountGreyPicHeaderCHA;


            }


        }

        #region cReportPDFGenerator

        public cReportPDFGenerator( Demo pParentWindow, string pFileName, string pAuthor, string pCreator, string pKeywords, string pSubject, string pTitle ) {

            try {

                gTrackImagePerLine = 0;

                gBFTimeNewROMAN = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);

                gParentWindow = pParentWindow;

                gMinimumDisplayEnergyCount = gParentWindow.gMinimumDisplayEnergyCount;

                gfPeakLowLimit = gParentWindow.gPeakLowLimit;

                gfPeakHighLimit = gParentWindow.gPeakHighLimit;

                gQualifiedType = gParentWindow.gSelectedQualifiedType;

                gPeakLowLimit = gfPeakLowLimit.ToString("0.0#");

                gPeakHighLimit = gfPeakHighLimit.ToString("0.0#");

                InitializeLanguage(gParentWindow.gSelectedLanguage);

                //Create PDF file
                gReportFileStream = new FileStream(pFileName, FileMode.Create);

                //Fill the PDF writer use file and document
                gPDFWriter = PdfWriter.GetInstance(gPDFDocument, gReportFileStream);

                gPDFDocument.AddAuthor(pAuthor);
                gPDFDocument.AddCreator(pCreator);
                gPDFDocument.AddKeywords(pKeywords);
                gPDFDocument.AddSubject(pSubject);
                gPDFDocument.AddTitle(pTitle);

                gPDFDocument.Open();

            } catch (Exception pException) { 
            
                
            
            }

        }

        #endregion

        #region AddGraph

        public void AddGraph( string pGraphFile, string pGraphTitle ) {

            iTextSharp.text.Image myPicture = iTextSharp.text.Image.GetInstance( pGraphFile );

            PdfPTable myResultTable = new PdfPTable( 1 );

            myResultTable.HorizontalAlignment = Element.ALIGN_LEFT;

            myResultTable.WidthPercentage = 100f;

            //PdfPCell myImageCell = new PdfPCell( myPicture, true );

            //100% fit the cell
            PdfPCell myImageCell = new PdfPCell( );

            myImageCell.AddElement( myPicture );

            myResultTable.AddCell( myImageCell );

            gPDFDocument.Add( myResultTable );

        }

        #endregion

        #region AddResultTable

        public void AddResultTable( string pTableName, int pColumn, Fitting pFittingData, byte[] pEnergyCount ) {

            float myEnergyMin = 0f;
            float myEnergyMax = 0f;
            float myEnergyAvg = 0f;

            float myEnergyResolutionMin = 0f;
            float myEnergyResolutionMax = 0f;
            float myEnergyResolutionAvg = 0f;

            int myAvgSampleCount = 0;

            int myInRangeCount = 0;
            int myGreaterCount = 0;
            int myLessCount = 0;

            float myQualityTypeValue = 0.0f;
            float myQualifiedMinValue = 0.0f;
            float myQualifiedMaxValue = 16384.0f;
            cEnergyCountQualified myEnergyCountQualified = new cEnergyCountQualified ( );
            cEnergyResolutionQualified myEnergyResolutionQualified = new cEnergyResolutionQualified ( );

            string myTimestamp = "";

            BaseFont myBFChinese;

            BaseFont myBFTimeNewROMAN;

            Font myFontEnglish;

            Font myFontChinese;

            Font myUseFont;

            iTextSharp.text.BaseColor myGreaterPixelColor = BaseColor.MAGENTA;
            iTextSharp.text.BaseColor myLessPixerlColor = BaseColor.ORANGE;

            //BaseFont.AddToResourceSearch("iTextAsian.dll");

            //BaseFont.AddToResourceSearch("iTextAsianCmaps.dll");

            myBFTimeNewROMAN = BaseFont.CreateFont( BaseFont.TIMES_ROMAN, BaseFont.CP1252, false );

            //myBFChinese = BaseFont.CreateFont("STSong-Light", "UniGB-UCS2-H", BaseFont.EMBEDDED);

            string ARIALUNI_TFF = Path.Combine( Environment.GetFolderPath( Environment.SpecialFolder.Fonts ), "simhei.ttf" );
           
            myBFChinese = BaseFont.CreateFont(ARIALUNI_TFF, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            
            BaseColor myGoldColor = new BaseColor( 255, 215, 0 );

            //16x16 Table
            PdfPTable myResultTable = new PdfPTable( pColumn );

            myResultTable.SpacingBefore = 10f;
            myResultTable.SpacingAfter = 10f;

            myResultTable.HorizontalAlignment = Element.ALIGN_LEFT;

            myResultTable.WidthPercentage = 100f;

            #region Add the table name

            //Use larger size for header
            myFontChinese = new Font( myBFChinese, 18, iTextSharp.text.Font.BOLD );

            myFontEnglish = new Font( myBFTimeNewROMAN, 18, iTextSharp.text.Font.BOLD );

            if( gParentWindow.gSelectedLanguage == ( int )Demo.gLanguageVersion.Chinese ) {

                myUseFont = myFontChinese;

            } else {

                myUseFont = myFontEnglish;

            }


            PdfPCell myTableNameCell = null;

            if( gParentWindow.gSelectedLanguage == ( int )Demo.gLanguageVersion.Chinese ) {

                if (gParentWindow.gReportNotes.Count() > 0) {

                    Phrase myPhrase = new Phrase();
                    myPhrase.Add(new Chunk(gTableHeader, myFontChinese));

                    Font mySubtiltFont = new Font(myBFChinese, 10, iTextSharp.text.Font.NORMAL);
                    myPhrase.Add(new Chunk("(" + gParentWindow.gReportNotes + ")", mySubtiltFont));

                    myTableNameCell = new PdfPCell(myPhrase);

                } else {

                    myTableNameCell = new PdfPCell( new Phrase( gTableHeader, myFontChinese ) );

                }

            } else {

                if (gParentWindow.gReportNotes.Count() > 0) {

                    myTableNameCell = new PdfPCell();

                    Phrase myPhrase = new Phrase();
                    myPhrase.Add(new Chunk(gTableHeader, myFontEnglish));

                    Font mySubtiltFont = new Font(myBFTimeNewROMAN, 10, iTextSharp.text.Font.NORMAL);
                    myPhrase.Add(new Chunk("(" + gParentWindow.gReportNotes + ")", mySubtiltFont));

                    myTableNameCell = new PdfPCell(myPhrase);

                } else {

                    myTableNameCell = new PdfPCell( new Phrase( gTableHeader, myFontEnglish ) );

                }
            }

            myTableNameCell.FixedHeight = 2 * gkDefaultCellHeight;

            myTableNameCell.Colspan = pColumn;

            //0 is Left, 1 is middle, 2 is right
            myTableNameCell.HorizontalAlignment = 1;

            myResultTable.AddCell( myTableNameCell );

            #endregion

            //Use small size for notes
            myFontChinese = new Font( myBFChinese, 8, iTextSharp.text.Font.BOLD );

            myFontEnglish = new Font( myBFTimeNewROMAN, 8, iTextSharp.text.Font.NORMAL );

            #region Add Timestamp and temperature

            PdfPCell myTimestampCell = null;

            myTimestamp = DateTime.Now.ToShortDateString( ) + " " + DateTime.Now.ToShortTimeString( );

            if( gParentWindow.gSelectedLanguage == ( int )Demo.gLanguageVersion.Chinese ) {

                myTimestampCell = new PdfPCell( new Phrase( "时间：" + myTimestamp + "    " + "温度： " + gParentWindow.gAverageTemperature.ToString("0.0") + "C", myFontChinese ) );

            } else {

                myTimestampCell = new PdfPCell( new Phrase( "Timestamp: " + myTimestamp + "    " + "Temperature: " + gParentWindow.gAverageTemperature.ToString("0.0") + "C", myFontEnglish ) );

            }

            myTimestampCell.FixedHeight = gkDefaultCellHeight;

            myTimestampCell.Colspan = pColumn;

            //0 is Left, 1 is middle, 2 is right
            myTimestampCell.HorizontalAlignment = 0;

            myResultTable.AddCell( myTimestampCell );

            #endregion

            //Add Red Notes Part
            //If it use different qualified range, then can not add this section to mislead people
            if (gParentWindow.gIsUseDifferentRangesForPixels == false) {
             
                #region Green Notes

                PdfPCell myGreenCell = null;

                if (gParentWindow.gSelectedLanguage == (int)Demo.gLanguageVersion.Chinese) {

                    myGreenCell = new PdfPCell(new Phrase(gGreen, myFontChinese));

                } else {

                    myGreenCell = new PdfPCell(new Phrase(gGreen, myFontEnglish));

                }

                myGreenCell.FixedHeight = gkDefaultCellHeight;

                myGreenCell.Colspan = 4;

                myGreenCell.BackgroundColor = BaseColor.GREEN;

                myResultTable.AddCell(myGreenCell);

                PdfPCell myGreenNotesCell = null;

                if (gParentWindow.gSelectedLanguage == (int)Demo.gLanguageVersion.Chinese) {

                    myGreenNotesCell = new PdfPCell(new Phrase(gInRangeMessage + gPeakLowLimit + " - " + gPeakHighLimit, myFontChinese));

                } else {

                    myGreenNotesCell = new PdfPCell(new Phrase(gInRangeMessage + gPeakLowLimit + " - " + gPeakHighLimit, myFontEnglish));

                }
                myGreenNotesCell.FixedHeight = gkDefaultCellHeight;

                myGreenNotesCell.Colspan = pColumn - 4;

                //Middle Allignment
                myGreenNotesCell.HorizontalAlignment = 1;

                myGreenNotesCell.BackgroundColor = BaseColor.WHITE;

                myResultTable.AddCell(myGreenNotesCell);

                #endregion

                #region Red Notes

                PdfPCell myRedCell = null;

                if (gParentWindow.gSelectedLanguage == (int)Demo.gLanguageVersion.Chinese) {

                    myRedCell = new PdfPCell(new Phrase(gRed, myFontChinese));

                } else {

                    myRedCell = new PdfPCell(new Phrase(gRed, myFontEnglish));

                }

                myRedCell.FixedHeight = gkDefaultCellHeight;

                myRedCell.Colspan = 4;

                myRedCell.BackgroundColor = BaseColor.RED;

                myResultTable.AddCell(myRedCell);

                PdfPCell myRedNotesCell = null;

                if (gParentWindow.gSelectedLanguage == (int)Demo.gLanguageVersion.Chinese) {

                    myRedNotesCell = new PdfPCell(new Phrase(gRedNoteMessage + gPeakLowLimit, myFontChinese));

                } else {

                    myRedNotesCell = new PdfPCell(new Phrase(gRedNoteMessage + gPeakLowLimit, myFontEnglish));

                }
                myRedNotesCell.FixedHeight = gkDefaultCellHeight;

                myRedNotesCell.Colspan = pColumn - 4;

                //Middle Allignment
                myRedNotesCell.HorizontalAlignment = 1;

                myRedNotesCell.BackgroundColor = BaseColor.WHITE;


                myResultTable.AddCell(myRedNotesCell);

                #endregion

                #region Yellow Notes

                PdfPCell myYellowCell = null;

                if (gParentWindow.gSelectedLanguage == (int)Demo.gLanguageVersion.Chinese) {

                    myYellowCell = new PdfPCell(new Phrase(gYellow, myFontChinese));

                } else {

                    myYellowCell = new PdfPCell(new Phrase(gYellow, myFontEnglish));

                }

                myYellowCell.FixedHeight = gkDefaultCellHeight;

                myYellowCell.Colspan = 4;

                //Gold color
                myYellowCell.BackgroundColor = myGoldColor;

                myResultTable.AddCell(myYellowCell);

                PdfPCell myYellowNotesCell = null;

                if (gParentWindow.gSelectedLanguage == (int)Demo.gLanguageVersion.Chinese) {

                    myYellowNotesCell = new PdfPCell(new Phrase(gYellowNoteMessage + gPeakHighLimit, myFontChinese));

                } else {

                    myYellowNotesCell = new PdfPCell(new Phrase(gYellowNoteMessage + gPeakHighLimit, myFontEnglish));

                }
                myYellowNotesCell.FixedHeight = gkDefaultCellHeight;

                myYellowNotesCell.Colspan = pColumn - 4;

                //Middle Allignment
                myYellowNotesCell.HorizontalAlignment = 1;

                myYellowNotesCell.BackgroundColor = BaseColor.WHITE;


                myResultTable.AddCell(myYellowNotesCell);

                #endregion

            }

            //Search for energy min and max and resolution min and max
            #region Search for energy min and max and resolution min and max

            for( int i = 0; i < 256; i++ ) {

                //Add this check because don't want to save the leaking light pixel data
                if( pEnergyCount[i] > gMinimumDisplayEnergyCount ) {

                    cFittingParameters myFittingParameters = new cFittingParameters( );

                    if( pFittingData.gDictFittingParameters.TryGetValue( i, out myFittingParameters ) ) {

                        //If has this pixel
                        if( myFittingParameters.mStatus == ( int )cFittingParameters.eStatus.OK ) {

                            if (gQualifiedType == 0) {

                                myQualityTypeValue = myFittingParameters.mG2Center;

                            } else if (gQualifiedType == 1) {

                                myQualityTypeValue = myFittingParameters.mEnergyResolution;

                            }

                            if (gParentWindow.gIsUseDifferentRangesForPixels) {

                                if (gQualifiedType == 0) {

                                    #region Use Energy Count

                                    if (gParentWindow.gEnergyCountQualifiedLevel.TryGetValue ( i, out myEnergyCountQualified )) {

                                        myQualifiedMinValue = myEnergyCountQualified.mEnergyCountMin;
                                        myQualifiedMaxValue = myEnergyCountQualified.mEnergyCountMax;

                                    } else {

                                        myQualifiedMinValue = gParentWindow.gPeakLowLimit;
                                        myQualifiedMaxValue = gParentWindow.gPeakHighLimit;

                                    }

                                    #endregion

                                } else if (gQualifiedType == 1) {

                                    #region Use Energy Resolution

                                    if (gParentWindow.gEnergyResolutionQualifiedLevel.TryGetValue ( i, out myEnergyResolutionQualified )) {

                                        myQualifiedMinValue = myEnergyResolutionQualified.mEnergyResolutionMin;
                                        myQualifiedMaxValue = myEnergyResolutionQualified.mEnergyResolutionMax;

                                    } else {

                                        myQualifiedMinValue = gParentWindow.gPeakLowLimit;
                                        myQualifiedMaxValue = gParentWindow.gPeakHighLimit;

                                    }

                                    #endregion

                                }
                                


                            } else {

                                myQualifiedMinValue = gParentWindow.gPeakLowLimit;
                                myQualifiedMaxValue = gParentWindow.gPeakHighLimit;
                            
                            }

                            //Update Energy Min and Max
                            #region Update Energy Min and Max
                            if( myFittingParameters.mG2Center > myEnergyMax ) {

                                myEnergyMax = myFittingParameters.mG2Center;

                            } else if( myFittingParameters.mG2Center < myEnergyMin ) {

                                myEnergyMin = myFittingParameters.mG2Center;

                            }

                            if( myEnergyMin <= 0.5f ) {

                                myEnergyMin = myFittingParameters.mG2Center;

                            }

                            #endregion

                            myEnergyAvg += myFittingParameters.mG2Center;

                            //Update Resolution Min and Max
                            #region Update Resolution Min and Max
                            if( myFittingParameters.mEnergyResolution > myEnergyResolutionMax ) {

                                myEnergyResolutionMax = myFittingParameters.mEnergyResolution;

                            } else if( myFittingParameters.mEnergyResolution < myEnergyResolutionMin ) {

                                myEnergyResolutionMin = myFittingParameters.mEnergyResolution;

                            }

                            if( myEnergyResolutionMin <= 0.5f ) {

                                myEnergyResolutionMin = myFittingParameters.mEnergyResolution;

                            }
                            #endregion

                            //In range count, great count, less count
                            #region Range Count

                            if ((myQualityTypeValue >= myQualifiedMinValue) &&
                                 ((myQualityTypeValue <= myQualifiedMaxValue))) {

                                myInRangeCount++;

                            } else if (myQualityTypeValue < myQualifiedMinValue) {

                                myLessCount++;

                            } else if (myQualityTypeValue > myQualifiedMaxValue) {

                                myGreaterCount++;

                            }
                            #endregion

                            myEnergyResolutionAvg += myFittingParameters.mEnergyResolution;

                            myAvgSampleCount++;


                        }

                    }

                }

            }

            myEnergyAvg = myEnergyAvg / myAvgSampleCount;

            myEnergyResolutionAvg = myEnergyResolutionAvg / myAvgSampleCount;

            #endregion

            //Add Energy Min and Max
            #region Energy Min and Max Notes

            PdfPCell myEnergyCell = null;

            if( gParentWindow.gSelectedLanguage == ( int )Demo.gLanguageVersion.Chinese ) {

                myEnergyCell = new PdfPCell( new Phrase( gEnergyNotes, myFontChinese ) );

            } else {

                myEnergyCell = new PdfPCell( new Phrase( gEnergyNotes, myFontEnglish ) );

            }

            myEnergyCell.FixedHeight = gkDefaultCellHeight;

            myEnergyCell.Colspan = 4;

            myEnergyCell.BackgroundColor = BaseColor.WHITE;

            myResultTable.AddCell( myEnergyCell );

            PdfPCell myEnergyValueCell = null;

            if( gParentWindow.gSelectedLanguage == ( int )Demo.gLanguageVersion.Chinese ) {

                myEnergyValueCell = new PdfPCell( new Phrase( myEnergyMin.ToString( "0.0" ) + ", " + myEnergyMax.ToString( "0.0" ) + ", " + myEnergyAvg.ToString( "0.0" ), myFontChinese ) );

            } else {

                myEnergyValueCell = new PdfPCell( new Phrase( myEnergyMin.ToString( "0.0" ) + ", " + myEnergyMax.ToString( "0.0" ) + ", " + myEnergyAvg.ToString( "0.0" ), myFontEnglish ) );

            }

            myEnergyValueCell.FixedHeight = gkDefaultCellHeight;

            myEnergyValueCell.Colspan = pColumn - 4;

            //Middle Allignment
            myEnergyValueCell.HorizontalAlignment = 1;

            myEnergyValueCell.BackgroundColor = BaseColor.WHITE;

            myResultTable.AddCell( myEnergyValueCell );

            #endregion

            #region Energy Resolution Min and Max Notes

            PdfPCell myEnergyResolutionCell = null;

            if( gParentWindow.gSelectedLanguage == ( int )Demo.gLanguageVersion.Chinese ) {

                myEnergyResolutionCell = new PdfPCell( new Phrase( gEnergyResolutionNotes, myFontChinese ) );

            } else {

                myEnergyResolutionCell = new PdfPCell( new Phrase( gEnergyResolutionNotes, myFontEnglish ) );

            }

            myEnergyResolutionCell.FixedHeight = gkDefaultCellHeight;

            myEnergyResolutionCell.Colspan = 4;

            myEnergyResolutionCell.BackgroundColor = BaseColor.WHITE;

            myResultTable.AddCell( myEnergyResolutionCell );

            PdfPCell myEnergyResolutionValueCell = null;

            if( gParentWindow.gSelectedLanguage == ( int )Demo.gLanguageVersion.Chinese ) {

                myEnergyResolutionValueCell = new PdfPCell( new Phrase( myEnergyResolutionMin.ToString( "0.0" ) + "% , " + myEnergyResolutionMax.ToString( "0.0" ) + "%, " + myEnergyResolutionAvg.ToString( "0.0" ) + "%", myFontChinese ) );

            } else {

                myEnergyResolutionValueCell = new PdfPCell( new Phrase( myEnergyResolutionMin.ToString( "0.0" ) + "% , " + myEnergyResolutionMax.ToString( "0.0" ) + "%, " + myEnergyResolutionAvg.ToString( "0.0" ) + "%", myFontEnglish ) );

            }

            myEnergyResolutionValueCell.FixedHeight = gkDefaultCellHeight;

            myEnergyResolutionValueCell.Colspan = pColumn - 4;

            //Middle Allignment
            myEnergyResolutionValueCell.HorizontalAlignment = 1;

            myEnergyResolutionValueCell.BackgroundColor = BaseColor.WHITE;

            myResultTable.AddCell( myEnergyResolutionValueCell );

            #endregion

            #region  Energy Count Min and Max Notes

            PdfPCell myEnergyCountSummaryNotesCell = null;

            if( gParentWindow.gSelectedLanguage == ( int )Demo.gLanguageVersion.Chinese ) {

                myEnergyCountSummaryNotesCell = new PdfPCell( new Phrase( gEnergyCountSummaryNotes, myFontChinese ) );

            } else {

                myEnergyCountSummaryNotesCell = new PdfPCell( new Phrase( gEnergyCountSummaryNotes, myFontEnglish ) );

            }

            myEnergyCountSummaryNotesCell.FixedHeight = gkDefaultCellHeight;

            myEnergyCountSummaryNotesCell.Colspan = 4;

            myEnergyCountSummaryNotesCell.BackgroundColor = BaseColor.WHITE;

            myResultTable.AddCell( myEnergyCountSummaryNotesCell );

            PdfPCell myEnergyCountSummaryCell = null;

            if( gParentWindow.gSelectedLanguage == ( int )Demo.gLanguageVersion.Chinese ) {

                myEnergyCountSummaryCell = new PdfPCell( new Phrase( pFittingData.gCountMin.ToString( "" ) + " , " + pFittingData.gCountMax.ToString( "" ) + " , " + pFittingData.gCountAvg.ToString( "" ), myFontChinese ) );

            } else {

                myEnergyCountSummaryCell = new PdfPCell( new Phrase( pFittingData.gCountMin.ToString( "" ) + " , " + pFittingData.gCountMax.ToString( "" ) + " , " + pFittingData.gCountAvg.ToString( "" ), myFontEnglish ) );

            }

            myEnergyCountSummaryCell.FixedHeight = gkDefaultCellHeight;

            myEnergyCountSummaryCell.Colspan = pColumn - 4;

            //Middle Allignment
            myEnergyCountSummaryCell.HorizontalAlignment = 1;

            myEnergyCountSummaryCell.BackgroundColor = BaseColor.WHITE;

            myResultTable.AddCell( myEnergyCountSummaryCell );

            #endregion

            #region Pixel Count Notes

            PdfPCell myCountSummaryNotesCell = null;
            string myCountSummaryNotes = gCountSummaryNotes;

            if (gQualifiedType == 0) {

                myCountSummaryNotes = gCountSummaryNotes + "(" + gPixelEnergyInfo + ")";

            } else if (gQualifiedType == 1) {

                myCountSummaryNotes = gCountSummaryNotes + "(" + gPixelResolutionInfo + ")";
            
            }

            if( gParentWindow.gSelectedLanguage == ( int )Demo.gLanguageVersion.Chinese ) {

                myCountSummaryNotesCell = new PdfPCell(new Phrase(myCountSummaryNotes, myFontChinese));

            } else {

                myCountSummaryNotesCell = new PdfPCell(new Phrase(myCountSummaryNotes, myFontEnglish));

            }

            myCountSummaryNotesCell.FixedHeight = gkDefaultCellHeight;

            myCountSummaryNotesCell.Colspan = 4;

            myCountSummaryNotesCell.BackgroundColor = BaseColor.WHITE;

            myResultTable.AddCell( myCountSummaryNotesCell );

            PdfPCell myCountSummaryCell = null;

            if( gParentWindow.gSelectedLanguage == ( int )Demo.gLanguageVersion.Chinese ) {

                myCountSummaryCell = new PdfPCell( new Phrase( myInRangeCount.ToString( "" ) + " , " + myGreaterCount.ToString( "" ) + " , " + myLessCount.ToString( "" ), myFontChinese ) );

            } else {

                myCountSummaryCell = new PdfPCell( new Phrase( myInRangeCount.ToString( "" ) + " , " + myGreaterCount.ToString( "" ) + " , " + myLessCount.ToString( "" ), myFontEnglish ) );

            }

            myCountSummaryCell.FixedHeight = gkDefaultCellHeight;

            myCountSummaryCell.Colspan = pColumn - 4;

            //Middle Allignment
            myCountSummaryCell.HorizontalAlignment = 1;

            myCountSummaryCell.BackgroundColor = BaseColor.WHITE;

            myResultTable.AddCell( myCountSummaryCell );

            #endregion

            //Add X header
            #region Add X Header

            PdfPCell myEmptyXCell = null;

            if( gParentWindow.gSelectedLanguage == ( int )Demo.gLanguageVersion.Chinese ) {

                #region Decide what data to include in report

                if( ( gParentWindow.gIsIncludeEnergySpectrumInReport == true ) &&
                    ( gParentWindow.gIsIncludeResolutionInReport == false ) &&
                    ( gParentWindow.gIsIncludeEnergyCountInReport == false ) ) {
                    //A
                    gDataCellNotes = gkDataCellNotesValueCHA;


                } else if( ( gParentWindow.gIsIncludeEnergySpectrumInReport == false ) &&
                           ( gParentWindow.gIsIncludeResolutionInReport == true ) &&
                           ( gParentWindow.gIsIncludeEnergyCountInReport == false ) ) {
                    //B
                    gDataCellNotes = gkDataCellNotesResolutionCHA;

                } else if( ( gParentWindow.gIsIncludeEnergySpectrumInReport == false ) &&
                           ( gParentWindow.gIsIncludeResolutionInReport == false ) &&
                           ( gParentWindow.gIsIncludeEnergyCountInReport == true ) ) {
                    //C
                    gDataCellNotes = gkDataCellNotesCountCHA;

                } else if( ( gParentWindow.gIsIncludeEnergySpectrumInReport == true ) &&
                           ( gParentWindow.gIsIncludeResolutionInReport == true ) &&
                           ( gParentWindow.gIsIncludeEnergyCountInReport == false ) ) {
                    //AB
                    gDataCellNotes = gkDataCellNotesValueCHA + "/" + "\n" +
                                     gkDataCellNotesResolutionCHA;

                } else if( ( gParentWindow.gIsIncludeEnergySpectrumInReport == true ) &&
                           ( gParentWindow.gIsIncludeResolutionInReport == false ) &&
                           ( gParentWindow.gIsIncludeEnergyCountInReport == true ) ) {
                    //AC
                    gDataCellNotes = gkDataCellNotesValueCHA + "/" + "\n" +
                                     gkDataCellNotesCountCHA;

                } else if( ( gParentWindow.gIsIncludeEnergySpectrumInReport == false ) &&
                           ( gParentWindow.gIsIncludeResolutionInReport == true ) &&
                           ( gParentWindow.gIsIncludeEnergyCountInReport == true ) ) {
                    //BC
                    gDataCellNotes = gkDataCellNotesResolutionCHA + "/" + "\n" +
                                     gkDataCellNotesCountCHA;

                } else if( ( gParentWindow.gIsIncludeEnergySpectrumInReport == true ) &&
                           ( gParentWindow.gIsIncludeResolutionInReport == true ) &&
                           ( gParentWindow.gIsIncludeEnergyCountInReport == true ) ) {
                    //ABC
                    gDataCellNotes = gkDataCellNotesValueCHA + "/" + "\n" +
                                     gkDataCellNotesResolutionCHA + "/" + "\n" +
                                     gkDataCellNotesCountCHA;

                }

                #endregion

                myEmptyXCell = new PdfPCell( new Phrase( gDataCellNotes, new Font( myBFChinese, 5, iTextSharp.text.Font.BOLD ) ) );

            } else {

                #region Decide what data to include in report

                if( ( gParentWindow.gIsIncludeEnergySpectrumInReport == true ) &&
                    ( gParentWindow.gIsIncludeResolutionInReport == false ) &&
                    ( gParentWindow.gIsIncludeEnergyCountInReport == false ) ) {
                    //A
                    gDataCellNotes = gkDataCellNotesValueENG;


                } else if( ( gParentWindow.gIsIncludeEnergySpectrumInReport == false ) &&
                           ( gParentWindow.gIsIncludeResolutionInReport == true ) &&
                           ( gParentWindow.gIsIncludeEnergyCountInReport == false ) ) {
                    //B
                    gDataCellNotes = gkDataCellNotesResolutionENG;

                } else if( ( gParentWindow.gIsIncludeEnergySpectrumInReport == false ) &&
                           ( gParentWindow.gIsIncludeResolutionInReport == false ) &&
                           ( gParentWindow.gIsIncludeEnergyCountInReport == true ) ) {
                    //C
                    gDataCellNotes = gkDataCellNotesCountENG;

                } else if( ( gParentWindow.gIsIncludeEnergySpectrumInReport == true ) &&
                           ( gParentWindow.gIsIncludeResolutionInReport == true ) &&
                           ( gParentWindow.gIsIncludeEnergyCountInReport == false ) ) {
                    //AB
                    gDataCellNotes = gkDataCellNotesValueCHA + "/" + "\n" +
                                     gkDataCellNotesResolutionENG;

                } else if( ( gParentWindow.gIsIncludeEnergySpectrumInReport == true ) &&
                           ( gParentWindow.gIsIncludeResolutionInReport == false ) &&
                           ( gParentWindow.gIsIncludeEnergyCountInReport == true ) ) {
                    //AC
                    gDataCellNotes = gkDataCellNotesValueENG + "/" + "\n" +
                                     gkDataCellNotesCountENG;

                } else if( ( gParentWindow.gIsIncludeEnergySpectrumInReport == false ) &&
                           ( gParentWindow.gIsIncludeResolutionInReport == true ) &&
                           ( gParentWindow.gIsIncludeEnergyCountInReport == true ) ) {
                    //BC
                    gDataCellNotes = gkDataCellNotesResolutionENG + "/" + "\n" +
                                     gkDataCellNotesCountENG;

                } else if( ( gParentWindow.gIsIncludeEnergySpectrumInReport == true ) &&
                           ( gParentWindow.gIsIncludeResolutionInReport == true ) &&
                           ( gParentWindow.gIsIncludeEnergyCountInReport == true ) ) {
                    //ABC
                    gDataCellNotes = gkDataCellNotesValueENG + "/" + "\n" +
                                     gkDataCellNotesResolutionENG + "/" + "\n" +
                                     gkDataCellNotesCountENG;

                }

                #endregion

                myEmptyXCell = new PdfPCell( new Phrase( gDataCellNotes, new Font( myBFTimeNewROMAN, 5, iTextSharp.text.Font.BOLD ) ) );

            }

            myEmptyXCell.FixedHeight = gkDefaultCellHeight;

            myEmptyXCell.BackgroundColor = BaseColor.CYAN;

            myResultTable.AddCell( myEmptyXCell );


            for( int i = 0; i < 16; i++ ) {

                PdfPCell myXHeaderCell = new PdfPCell( new Phrase( "X" + i.ToString( ) ) );

                myXHeaderCell.FixedHeight = gkDefaultCellHeight;

                myXHeaderCell.BackgroundColor = BaseColor.CYAN;

                myResultTable.AddCell( myXHeaderCell );

            }

            #endregion

            //Loop all the content
            myFontEnglish = new Font( myBFTimeNewROMAN, 5, iTextSharp.text.Font.NORMAL );

            #region Save Data

            for( int i = 0; i < 256; i++ ) {

                //Add this check because don't want to save the leaking light pixel data
                cFittingParameters myFittingParameters = new cFittingParameters( );

                PdfPCell myPixelCell = null;

                if( i % 16 == 0 ) {

                    //If it is a new column
                    //Then add Yn
                    myPixelCell = new PdfPCell( new Phrase( "Y" + ( i / 16 ).ToString( ) ) );

                    myPixelCell.FixedHeight = gkDefaultCellHeight;

                    myPixelCell.BackgroundColor = BaseColor.CYAN;

                    myResultTable.AddCell( myPixelCell );
                
                } else {

                    //Will create it later
                
                }

                if( pEnergyCount[i] > gMinimumDisplayEnergyCount ) {

                    #region Get Fitting Result for Pixel i

                    if( pFittingData.gDictFittingParameters.TryGetValue( i, out myFittingParameters ) ) {

                        //If has this pixel
                        if( ( myFittingParameters.mStatus == ( int )cFittingParameters.eStatus.OK ) || ( myFittingParameters.mStatus == ( int )cFittingParameters.eStatus.ManualFit ) ) {

                            if (gQualifiedType == 0) {

                                myQualityTypeValue = myFittingParameters.mG2Center;

                                myGreaterPixelColor = myGoldColor;
                                myLessPixerlColor = BaseColor.RED;

                            } else if(gQualifiedType == 1) {
                            
                                myQualityTypeValue = myFittingParameters.mEnergyResolution;

                                myGreaterPixelColor = BaseColor.RED;
                                myLessPixerlColor = myGoldColor;

                            }

                            #region Check Qualified Range

                            if (gParentWindow.gIsUseDifferentRangesForPixels) {

                                if (gQualifiedType == 0) {

                                    #region Use Energy Count

                                    if (gParentWindow.gEnergyCountQualifiedLevel.TryGetValue ( i, out myEnergyCountQualified )) {

                                        myQualifiedMinValue = myEnergyCountQualified.mEnergyCountMin;
                                        myQualifiedMaxValue = myEnergyCountQualified.mEnergyCountMax;

                                    } else {

                                        myQualifiedMinValue = gParentWindow.gPeakLowLimit;
                                        myQualifiedMaxValue = gParentWindow.gPeakHighLimit;

                                    }

                                    #endregion

                                } else if (gQualifiedType == 1) {

                                    #region Use Energy Resolution

                                    if (gParentWindow.gEnergyResolutionQualifiedLevel.TryGetValue ( i, out myEnergyResolutionQualified )) {

                                        myQualifiedMinValue = myEnergyResolutionQualified.mEnergyResolutionMin;
                                        myQualifiedMaxValue = myEnergyResolutionQualified.mEnergyResolutionMax;

                                    } else {

                                        myQualifiedMinValue = gParentWindow.gPeakLowLimit;
                                        myQualifiedMaxValue = gParentWindow.gPeakHighLimit;

                                    }

                                    #endregion

                                }



                            } else {

                                myQualifiedMinValue = gParentWindow.gPeakLowLimit;
                                myQualifiedMaxValue = gParentWindow.gPeakHighLimit;

                            }

                            #endregion

                            if ((myQualityTypeValue >= myQualifiedMinValue) &&
                                ((myQualityTypeValue <= myQualifiedMaxValue))) {

                                //In range 
                                #region In Range

                                #region Decide what data to include in report

                                    if( ( gParentWindow.gIsIncludeEnergySpectrumInReport == true ) &&
                                        ( gParentWindow.gIsIncludeResolutionInReport == false ) &&
                                        ( gParentWindow.gIsIncludeEnergyCountInReport == false ) ) {
                                        //A
                                        myPixelCell = new PdfPCell( new Phrase( myFittingParameters.mG2Center.ToString( "0.0" ) + "/", myFontEnglish ) );


                                    } else if( ( gParentWindow.gIsIncludeEnergySpectrumInReport == false ) &&
                                               ( gParentWindow.gIsIncludeResolutionInReport == true ) &&
                                               ( gParentWindow.gIsIncludeEnergyCountInReport == false ) ) {
                                        //B
                                        myPixelCell = new PdfPCell( new Phrase( myFittingParameters.mEnergyResolution.ToString( "0.0" ) + "%" + "/", myFontEnglish ) );

                                    } else if( ( gParentWindow.gIsIncludeEnergySpectrumInReport == false ) &&
                                               ( gParentWindow.gIsIncludeResolutionInReport == false ) &&
                                               ( gParentWindow.gIsIncludeEnergyCountInReport == true ) ) {
                                        //C
                                        myPixelCell = new PdfPCell( new Phrase( pFittingData.gCountCnt[i].ToString( "0.0" ) + "Cnt" + "/", myFontEnglish ) );

                                    } else if( ( gParentWindow.gIsIncludeEnergySpectrumInReport == true ) &&
                                               ( gParentWindow.gIsIncludeResolutionInReport == true ) &&
                                               ( gParentWindow.gIsIncludeEnergyCountInReport == false ) ) {
                                        //AB
                                        myPixelCell = new PdfPCell( new Phrase( ( myFittingParameters.mG2Center ).ToString( "0.0" ) + "/" + "\n" +
                                                                                  myFittingParameters.mEnergyResolution.ToString( "0.0" ) + "%" + "/", myFontEnglish ) );


                                    } else if( ( gParentWindow.gIsIncludeEnergySpectrumInReport == true ) &&
                                               ( gParentWindow.gIsIncludeResolutionInReport == false ) &&
                                               ( gParentWindow.gIsIncludeEnergyCountInReport == true ) ) {
                                        //AC
                                        myPixelCell = new PdfPCell( new Phrase( ( myFittingParameters.mG2Center ).ToString( "0.0" ) + "/" + "\n" +
                                                                                  pFittingData.gCountCnt[i].ToString( "0.0" ) + "Cnt" + "/", myFontEnglish ) );

                                    } else if( ( gParentWindow.gIsIncludeEnergySpectrumInReport == false ) &&
                                               ( gParentWindow.gIsIncludeResolutionInReport == true ) &&
                                               ( gParentWindow.gIsIncludeEnergyCountInReport == true ) ) {
                                        //BC
                                        myPixelCell = new PdfPCell( new Phrase( myFittingParameters.mEnergyResolution.ToString( "0.0" ) + "%" + "/" + "\n" +
                                                                                pFittingData.gCountCnt[i].ToString( "0.0" ) + "Cnt" + "/", myFontEnglish ) );

                                    } else if( ( gParentWindow.gIsIncludeEnergySpectrumInReport == true ) &&
                                               ( gParentWindow.gIsIncludeResolutionInReport == true ) &&
                                               ( gParentWindow.gIsIncludeEnergyCountInReport == true ) ) {
                                        //ABC
                                        myPixelCell = new PdfPCell( new Phrase( ( myFittingParameters.mG2Center ).ToString( "0.0" ) + "/" + "\n" +
                                                                                  myFittingParameters.mEnergyResolution.ToString( "0.0" ) + "%" + "/" + "\n" +
                                                                                  pFittingData.gCountCnt[i].ToString( "0.0" ) + "Cnt" + "/", myFontEnglish ) );

                                    }

                                    #endregion

                                myPixelCell.FixedHeight = gkDefaultCellHeight;

                                myPixelCell.BackgroundColor = BaseColor.GREEN;

                                #endregion

                            } else {

                                //Not in range
                                #region Not in range

                                if (myQualityTypeValue < myQualifiedMinValue) {

                                    //Less than low limit
                                    #region Decide what data to include in report

                                    if( ( gParentWindow.gIsIncludeEnergySpectrumInReport == true ) &&
                                        ( gParentWindow.gIsIncludeResolutionInReport == false ) &&
                                        ( gParentWindow.gIsIncludeEnergyCountInReport == false ) ) {
                                        //A
                                        myPixelCell = new PdfPCell( new Phrase( myFittingParameters.mG2Center.ToString( "0.0" ) + "/", myFontEnglish ) );


                                    } else if( ( gParentWindow.gIsIncludeEnergySpectrumInReport == false ) &&
                                               ( gParentWindow.gIsIncludeResolutionInReport == true ) &&
                                               ( gParentWindow.gIsIncludeEnergyCountInReport == false ) ) {
                                        //B
                                        myPixelCell = new PdfPCell( new Phrase( myFittingParameters.mEnergyResolution.ToString( "0.0" ) + "%" + "/", myFontEnglish ) );

                                    } else if( ( gParentWindow.gIsIncludeEnergySpectrumInReport == false ) &&
                                               ( gParentWindow.gIsIncludeResolutionInReport == false ) &&
                                               ( gParentWindow.gIsIncludeEnergyCountInReport == true ) ) {
                                        //C
                                        myPixelCell = new PdfPCell( new Phrase( pFittingData.gCountCnt[i].ToString( "0.0" ) + "Cnt" + "/", myFontEnglish ) );

                                    } else if( ( gParentWindow.gIsIncludeEnergySpectrumInReport == true ) &&
                                               ( gParentWindow.gIsIncludeResolutionInReport == true ) &&
                                               ( gParentWindow.gIsIncludeEnergyCountInReport == false ) ) {
                                        //AB
                                        myPixelCell = new PdfPCell( new Phrase( ( myFittingParameters.mG2Center ).ToString( "0.0" ) + "/" + "\n" +
                                                                                  myFittingParameters.mEnergyResolution.ToString( "0.0" ) + "%" + "/", myFontEnglish ) );


                                    } else if( ( gParentWindow.gIsIncludeEnergySpectrumInReport == true ) &&
                                               ( gParentWindow.gIsIncludeResolutionInReport == false ) &&
                                               ( gParentWindow.gIsIncludeEnergyCountInReport == true ) ) {
                                        //AC
                                        myPixelCell = new PdfPCell( new Phrase( ( myFittingParameters.mG2Center ).ToString( "0.0" ) + "/" + "\n" +
                                                                                  pFittingData.gCountCnt[i].ToString( "0.0" ) + "Cnt" + "/", myFontEnglish ) );

                                    } else if( ( gParentWindow.gIsIncludeEnergySpectrumInReport == false ) &&
                                               ( gParentWindow.gIsIncludeResolutionInReport == true ) &&
                                               ( gParentWindow.gIsIncludeEnergyCountInReport == true ) ) {
                                        //BC
                                        myPixelCell = new PdfPCell( new Phrase( myFittingParameters.mEnergyResolution.ToString( "0.0" ) + "%" + "/" + "\n" +
                                                                                pFittingData.gCountCnt[i].ToString( "0.0" ) + "Cnt" + "/", myFontEnglish ) );

                                    } else if( ( gParentWindow.gIsIncludeEnergySpectrumInReport == true ) &&
                                               ( gParentWindow.gIsIncludeResolutionInReport == true ) &&
                                               ( gParentWindow.gIsIncludeEnergyCountInReport == true ) ) {
                                        //ABC
                                        myPixelCell = new PdfPCell( new Phrase( ( myFittingParameters.mG2Center ).ToString( "0.0" ) + "/" + "\n" +
                                                                                  myFittingParameters.mEnergyResolution.ToString( "0.0" ) + "%" + "/" + "\n" +
                                                                                  pFittingData.gCountCnt[i].ToString( "0.0" ) + "Cnt" + "/", myFontEnglish ) );

                                    }

                                    #endregion

                                    myPixelCell.FixedHeight = gkDefaultCellHeight;

                                    myPixelCell.BackgroundColor = myLessPixerlColor;

                                } else if (myQualityTypeValue > myQualifiedMaxValue) {

                                    //Greater than high limit

                                    #region Decide what data to include in report

                                    if( ( gParentWindow.gIsIncludeEnergySpectrumInReport == true ) &&
                                        ( gParentWindow.gIsIncludeResolutionInReport == false ) &&
                                        ( gParentWindow.gIsIncludeEnergyCountInReport == false ) ) {
                                        //A
                                        myPixelCell = new PdfPCell( new Phrase( myFittingParameters.mG2Center.ToString( "0.0" ) + "/", myFontEnglish ) );


                                    } else if( ( gParentWindow.gIsIncludeEnergySpectrumInReport == false ) &&
                                               ( gParentWindow.gIsIncludeResolutionInReport == true ) &&
                                               ( gParentWindow.gIsIncludeEnergyCountInReport == false ) ) {
                                        //B
                                        myPixelCell = new PdfPCell( new Phrase( myFittingParameters.mEnergyResolution.ToString( "0.0" ) + "%" + "/", myFontEnglish ) );

                                    } else if( ( gParentWindow.gIsIncludeEnergySpectrumInReport == false ) &&
                                               ( gParentWindow.gIsIncludeResolutionInReport == false ) &&
                                               ( gParentWindow.gIsIncludeEnergyCountInReport == true ) ) {
                                        //C
                                        myPixelCell = new PdfPCell( new Phrase( pFittingData.gCountCnt[i].ToString( "0.0" ) + "Cnt" + "/", myFontEnglish ) );

                                    } else if( ( gParentWindow.gIsIncludeEnergySpectrumInReport == true ) &&
                                               ( gParentWindow.gIsIncludeResolutionInReport == true ) &&
                                               ( gParentWindow.gIsIncludeEnergyCountInReport == false ) ) {
                                        //AB
                                        myPixelCell = new PdfPCell( new Phrase( ( myFittingParameters.mG2Center ).ToString( "0.0" ) + "/" + "\n" +
                                                                                  myFittingParameters.mEnergyResolution.ToString( "0.0" ) + "%" + "/", myFontEnglish ) );


                                    } else if( ( gParentWindow.gIsIncludeEnergySpectrumInReport == true ) &&
                                               ( gParentWindow.gIsIncludeResolutionInReport == false ) &&
                                               ( gParentWindow.gIsIncludeEnergyCountInReport == true ) ) {
                                        //AC
                                        myPixelCell = new PdfPCell( new Phrase( ( myFittingParameters.mG2Center ).ToString( "0.0" ) + "/" + "\n" +
                                                                                  pFittingData.gCountCnt[i].ToString( "0.0" ) + "Cnt" + "/", myFontEnglish ) );

                                    } else if( ( gParentWindow.gIsIncludeEnergySpectrumInReport == false ) &&
                                               ( gParentWindow.gIsIncludeResolutionInReport == true ) &&
                                               ( gParentWindow.gIsIncludeEnergyCountInReport == true ) ) {
                                        //BC
                                        myPixelCell = new PdfPCell( new Phrase( myFittingParameters.mEnergyResolution.ToString( "0.0" ) + "%" + "/" + "\n" +
                                                                                pFittingData.gCountCnt[i].ToString( "0.0" ) + "Cnt" + "/", myFontEnglish ) );

                                    } else if( ( gParentWindow.gIsIncludeEnergySpectrumInReport == true ) &&
                                               ( gParentWindow.gIsIncludeResolutionInReport == true ) &&
                                               ( gParentWindow.gIsIncludeEnergyCountInReport == true ) ) {
                                        //ABC
                                        myPixelCell = new PdfPCell( new Phrase( ( myFittingParameters.mG2Center ).ToString( "0.0" ) + "/" + "\n" +
                                                                                  myFittingParameters.mEnergyResolution.ToString( "0.0" ) + "%" + "/" + "\n" +
                                                                                  pFittingData.gCountCnt[i].ToString( "0.0" ) + "Cnt" + "/", myFontEnglish ) );

                                    }

                                    #endregion

                                    myPixelCell.FixedHeight = gkDefaultCellHeight;

                                    myPixelCell.BackgroundColor = myGreaterPixelColor;

                                }

                                #endregion

                            }

                        } else {

                            //Can not fitting
                            #region Can not fitting

                            myPixelCell = new PdfPCell( new Phrase( ) );

                            myPixelCell.FixedHeight = gkDefaultCellHeight;

                            myPixelCell.BackgroundColor = BaseColor.WHITE;

                            #endregion

                        }

                        myResultTable.AddCell( myPixelCell );

                    } else {

                        //Not in fitting result
                        #region Not in fitting result

                        myPixelCell = new PdfPCell( new Phrase( ) );

                        myPixelCell.FixedHeight = gkDefaultCellHeight;

                        myPixelCell.BackgroundColor = BaseColor.WHITE;

                        myResultTable.AddCell( myPixelCell );

                        #endregion

                    }

                    #endregion

                } else { 
                
                    //Need add a gray cell
                    //The pixel get leak light not need to save in report
                    #region Leak light pixel

                    myPixelCell = new PdfPCell( new Phrase( ) );

                    myPixelCell.FixedHeight = gkDefaultCellHeight;

                    myPixelCell.BackgroundColor = BaseColor.WHITE;

                    myResultTable.AddCell( myPixelCell );

                    #endregion
                
                }


            }

            #endregion

            gPDFDocument.Add( myResultTable );

        }

        public void AddResultTable(string pTableName, int pColumn, Fitting pFittingData, byte[] pEnergyCount, int pType, bool pIsReverse) {

            int myColorIndex = 0;
            GreenColor myGreyColor = new GreenColor();
            int myColorStep = myGreyColor.GetGreyStep();
            float myQualityTypeStep = 1.0f;
            float myQualityTypeBase = 1.0f;

            float myEnergyMin = 0f;
            float myEnergyMax = 0f;
            float myEnergyAvg = 0f;

            float myEnergyResolutionMin = 0f;
            float myEnergyResolutionMax = 0f;
            float myEnergyResolutionAvg = 0f;

            int myAvgSampleCount = 0;

            int myInRangeCount = 0;
            int myGreaterCount = 0;
            int myLessCount = 0;

            float myQualityTypeValue = 0.0f;
            float myQualifiedMinValue = 0.0f;
            float myQualifiedMaxValue = 16384.0f;
            cEnergyCountQualified myEnergyCountQualified = new cEnergyCountQualified();
            cEnergyResolutionQualified myEnergyResolutionQualified = new cEnergyResolutionQualified();

            string myTimestamp = "";

            BaseFont myBFChinese;

            BaseFont myBFTimeNewROMAN;

            Font myFontEnglish;

            Font myFontChinese;

            Font myUseFont;

            iTextSharp.text.BaseColor myGreaterPixelColor = BaseColor.MAGENTA;
            iTextSharp.text.BaseColor myLessPixerlColor = BaseColor.ORANGE;

            //BaseFont.AddToResourceSearch("iTextAsian.dll");

            //BaseFont.AddToResourceSearch("iTextAsianCmaps.dll");

            myBFTimeNewROMAN = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);

            //myBFChinese = BaseFont.CreateFont("STSong-Light", "UniGB-UCS2-H", BaseFont.EMBEDDED);

            string ARIALUNI_TFF = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "simhei.ttf");

            myBFChinese = BaseFont.CreateFont(ARIALUNI_TFF, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

            BaseColor myGoldColor = new BaseColor(255, 215, 0);

            //16x16 Table
            PdfPTable myResultTable = new PdfPTable(pColumn);

            myResultTable.SpacingBefore = 10f;
            myResultTable.SpacingAfter = 10f;

            myResultTable.HorizontalAlignment = Element.ALIGN_LEFT;

            myResultTable.WidthPercentage = 100f;

            if (pType == gkEnergyPeak) {

                pTableName = gEnergyGreyPicHeader;

            } else if (pType == gkEnergyResolution) {

                pTableName = gResolutionGreyPicHeader;

            } else if (pType == gkEnergyCount) {

                pTableName = gCountGreyPicHeader;

            }

            #region Add the table name

            //Use larger size for header
            myFontChinese = new Font(myBFChinese, 18, iTextSharp.text.Font.BOLD);

            myFontEnglish = new Font(myBFTimeNewROMAN, 18, iTextSharp.text.Font.BOLD);

            if (gParentWindow.gSelectedLanguage == (int)Demo.gLanguageVersion.Chinese) {

                myUseFont = myFontChinese;

            } else {

                myUseFont = myFontEnglish;

            }


            PdfPCell myTableNameCell = null;

            if (gParentWindow.gSelectedLanguage == (int)Demo.gLanguageVersion.Chinese) {

                if (gParentWindow.gReportNotes.Count() > 0) {

                    Phrase myPhrase = new Phrase();
                    myPhrase.Add(new Chunk(pTableName, myFontChinese));

                    Font mySubtiltFont = new Font(myBFChinese, 10, iTextSharp.text.Font.NORMAL);
                    myPhrase.Add(new Chunk("(" + gParentWindow.gReportNotes + ")", mySubtiltFont));

                    myTableNameCell = new PdfPCell(myPhrase);

                } else {

                    myTableNameCell = new PdfPCell(new Phrase(pTableName, myFontChinese));

                }

            } else {

                if (gParentWindow.gReportNotes.Count() > 0) {

                    myTableNameCell = new PdfPCell();

                    Phrase myPhrase = new Phrase();
                    myPhrase.Add(new Chunk(pTableName, myFontEnglish));

                    Font mySubtiltFont = new Font(myBFTimeNewROMAN, 10, iTextSharp.text.Font.NORMAL);
                    myPhrase.Add(new Chunk("(" + gParentWindow.gReportNotes + ")", mySubtiltFont));

                    myTableNameCell = new PdfPCell(myPhrase);

                } else {

                    myTableNameCell = new PdfPCell(new Phrase(pTableName, myFontEnglish));

                }
            }

            myTableNameCell.FixedHeight = 2 * gkDefaultCellHeight;

            myTableNameCell.Colspan = pColumn;

            //0 is Left, 1 is middle, 2 is right
            myTableNameCell.HorizontalAlignment = 1;

            myResultTable.AddCell(myTableNameCell);

            #endregion

            //Use small size for notes
            myFontChinese = new Font(myBFChinese, 8, iTextSharp.text.Font.BOLD);

            myFontEnglish = new Font(myBFTimeNewROMAN, 8, iTextSharp.text.Font.NORMAL);

            #region Add Timestamp and temperature

            PdfPCell myTimestampCell = null;

            myTimestamp = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();

            if (gParentWindow.gSelectedLanguage == (int)Demo.gLanguageVersion.Chinese) {

                myTimestampCell = new PdfPCell(new Phrase("时间：" + myTimestamp + "    " + "温度： " + gParentWindow.gAverageTemperature.ToString("0.0") + "C", myFontChinese));

            } else {

                myTimestampCell = new PdfPCell(new Phrase("Timestamp: " + myTimestamp + "    " + "Temperature: " + gParentWindow.gAverageTemperature.ToString("0.0") + "C", myFontEnglish));

            }

            myTimestampCell.FixedHeight = gkDefaultCellHeight;

            myTimestampCell.Colspan = pColumn;

            //0 is Left, 1 is middle, 2 is right
            myTimestampCell.HorizontalAlignment = 0;

            myResultTable.AddCell(myTimestampCell);

            #endregion

            //Search for energy min and max and resolution min and max
            #region Search for energy min and max and resolution min and max

            for (int i = 0; i < 256; i++) {

                //Add this check because don't want to save the leaking light pixel data
                if (pEnergyCount[i] > gMinimumDisplayEnergyCount) {

                    cFittingParameters myFittingParameters = new cFittingParameters();

                    if (pFittingData.gDictFittingParameters.TryGetValue(i, out myFittingParameters)) {

                        //If has this pixel
                        if (myFittingParameters.mStatus == (int)cFittingParameters.eStatus.OK) {

                            if (gQualifiedType == 0) {

                                myQualityTypeValue = myFittingParameters.mG2Center;

                            } else if (gQualifiedType == 1) {

                                myQualityTypeValue = myFittingParameters.mEnergyResolution;

                            }

                            if (gParentWindow.gIsUseDifferentRangesForPixels) {

                                if (gQualifiedType == 0) {

                                    #region Use Energy Count

                                    if (gParentWindow.gEnergyCountQualifiedLevel.TryGetValue(i, out myEnergyCountQualified)) {

                                        myQualifiedMinValue = myEnergyCountQualified.mEnergyCountMin;
                                        myQualifiedMaxValue = myEnergyCountQualified.mEnergyCountMax;

                                    } else {

                                        myQualifiedMinValue = gParentWindow.gPeakLowLimit;
                                        myQualifiedMaxValue = gParentWindow.gPeakHighLimit;

                                    }

                                    #endregion

                                } else if (gQualifiedType == 1) {

                                    #region Use Energy Resolution

                                    if (gParentWindow.gEnergyResolutionQualifiedLevel.TryGetValue(i, out myEnergyResolutionQualified)) {

                                        myQualifiedMinValue = myEnergyResolutionQualified.mEnergyResolutionMin;
                                        myQualifiedMaxValue = myEnergyResolutionQualified.mEnergyResolutionMax;

                                    } else {

                                        myQualifiedMinValue = gParentWindow.gPeakLowLimit;
                                        myQualifiedMaxValue = gParentWindow.gPeakHighLimit;

                                    }

                                    #endregion

                                }



                            } else {

                                myQualifiedMinValue = gParentWindow.gPeakLowLimit;
                                myQualifiedMaxValue = gParentWindow.gPeakHighLimit;

                            }

                            //Update Energy Min and Max
                            #region Update Energy Min and Max
                            if (myFittingParameters.mG2Center > myEnergyMax) {

                                myEnergyMax = myFittingParameters.mG2Center;

                            } else if (myFittingParameters.mG2Center < myEnergyMin) {

                                myEnergyMin = myFittingParameters.mG2Center;

                            }

                            if (myEnergyMin <= 0.5f) {

                                myEnergyMin = myFittingParameters.mG2Center;

                            }

                            #endregion

                            myEnergyAvg += myFittingParameters.mG2Center;

                            //Update Resolution Min and Max
                            #region Update Resolution Min and Max
                            if (myFittingParameters.mEnergyResolution > myEnergyResolutionMax) {

                                myEnergyResolutionMax = myFittingParameters.mEnergyResolution;

                            } else if (myFittingParameters.mEnergyResolution < myEnergyResolutionMin) {

                                myEnergyResolutionMin = myFittingParameters.mEnergyResolution;

                            }

                            if (myEnergyResolutionMin <= 0.5f) {

                                myEnergyResolutionMin = myFittingParameters.mEnergyResolution;

                            }
                            #endregion

                            //In range count, great count, less count
                            #region Range Count

                            if ((myQualityTypeValue >= myQualifiedMinValue) &&
                                 ((myQualityTypeValue <= myQualifiedMaxValue))) {

                                myInRangeCount++;

                            } else if (myQualityTypeValue < myQualifiedMinValue) {

                                myLessCount++;

                            } else if (myQualityTypeValue > myQualifiedMaxValue) {

                                myGreaterCount++;

                            }
                            #endregion

                            myEnergyResolutionAvg += myFittingParameters.mEnergyResolution;

                            myAvgSampleCount++;


                        }

                    }

                }

            }

            myEnergyAvg = myEnergyAvg / myAvgSampleCount;

            myEnergyResolutionAvg = myEnergyResolutionAvg / myAvgSampleCount;

            #endregion

            //Add X header
            #region Add X Header

            PdfPCell myEmptyXCell = null;

            if (gParentWindow.gSelectedLanguage == (int)Demo.gLanguageVersion.Chinese) {

                #region Decide what data to include in report

                if (pType == gkEnergyPeak) {
                    //A
                    gDataCellNotes = gkDataCellNotesValueCHA;


                } else if (pType == gkEnergyResolution) {
                    //B
                    gDataCellNotes = gkDataCellNotesResolutionCHA;

                } else if (pType == gkEnergyCount) {
                    //C
                    gDataCellNotes = gkDataCellNotesCountCHA;

                } 

                #endregion

                myEmptyXCell = new PdfPCell(new Phrase(gDataCellNotes, new Font(myBFChinese, 5, iTextSharp.text.Font.BOLD)));

            } else {

                #region Decide what data to include in report

                if (pType == gkEnergyPeak) {
                    //A
                    gDataCellNotes = gkDataCellNotesValueENG;


                } else if (pType == gkEnergyResolution) {
                    //B
                    gDataCellNotes = gkDataCellNotesResolutionENG;

                } else if (pType == gkEnergyCount) {
                    //C
                    gDataCellNotes = gkDataCellNotesCountENG;

                } 

                #endregion

                myEmptyXCell = new PdfPCell(new Phrase(gDataCellNotes, new Font(myBFTimeNewROMAN, 5, iTextSharp.text.Font.BOLD)));

            }

            myEmptyXCell.FixedHeight = gkDefaultCellHeight;

            myEmptyXCell.BackgroundColor = BaseColor.CYAN;

            myResultTable.AddCell(myEmptyXCell);


            for (int i = 0; i < 16; i++) {

                PdfPCell myXHeaderCell = new PdfPCell(new Phrase("X" + i.ToString()));

                myXHeaderCell.FixedHeight = gkDefaultCellHeight;

                myXHeaderCell.BackgroundColor = BaseColor.CYAN;

                myResultTable.AddCell(myXHeaderCell);

            }

            #endregion

            //Loop all the content
            myFontEnglish = new Font(myBFTimeNewROMAN, 5, iTextSharp.text.Font.NORMAL);

            #region Save Data

            for (int i = 0; i < 256; i++) {

                //Add this check because don't want to save the leaking light pixel data
                cFittingParameters myFittingParameters = new cFittingParameters();

                PdfPCell myPixelCell = null;

                if (i % 16 == 0) {

                    //If it is a new column
                    //Then add Yn
                    myPixelCell = new PdfPCell(new Phrase("Y" + (i / 16).ToString()));

                    myPixelCell.FixedHeight = gkDefaultCellHeight;

                    myPixelCell.BackgroundColor = BaseColor.CYAN;

                    myResultTable.AddCell(myPixelCell);

                } else {

                    //Will create it later

                }

                if (pEnergyCount[i] > gMinimumDisplayEnergyCount) {

                    #region Get Fitting Result for Pixel i

                    if (pFittingData.gDictFittingParameters.TryGetValue(i, out myFittingParameters)) {

                        //If has this pixel
                        if ((myFittingParameters.mStatus == (int)cFittingParameters.eStatus.OK) || (myFittingParameters.mStatus == (int)cFittingParameters.eStatus.ManualFit)) {

                            if (gQualifiedType == 0) {

                                myQualityTypeValue = myFittingParameters.mG2Center;

                                myGreaterPixelColor = myGoldColor;
                                myLessPixerlColor = BaseColor.RED;

                                myQualityTypeStep = (myEnergyMax - myEnergyMin) / myColorStep;
                                myQualityTypeBase = myEnergyMin;

                            } else if (gQualifiedType == 1) {

                                myQualityTypeValue = myFittingParameters.mEnergyResolution;

                                myGreaterPixelColor = BaseColor.RED;
                                myLessPixerlColor = myGoldColor;

                                myQualityTypeStep = (myEnergyResolutionMax - myEnergyResolutionMin) / myColorStep;
                                myQualityTypeBase = myEnergyResolutionMin;

                            }

                            #region Check Qualified Range

                            if (gParentWindow.gIsUseDifferentRangesForPixels) {

                                if (gQualifiedType == 0) {

                                    #region Use Energy Count

                                    if (gParentWindow.gEnergyCountQualifiedLevel.TryGetValue(i, out myEnergyCountQualified)) {

                                        myQualifiedMinValue = myEnergyCountQualified.mEnergyCountMin;
                                        myQualifiedMaxValue = myEnergyCountQualified.mEnergyCountMax;

                                    } else {

                                        myQualifiedMinValue = gParentWindow.gPeakLowLimit;
                                        myQualifiedMaxValue = gParentWindow.gPeakHighLimit;

                                    }

                                    #endregion

                                } else if (gQualifiedType == 1) {

                                    #region Use Energy Resolution

                                    if (gParentWindow.gEnergyResolutionQualifiedLevel.TryGetValue(i, out myEnergyResolutionQualified)) {

                                        myQualifiedMinValue = myEnergyResolutionQualified.mEnergyResolutionMin;
                                        myQualifiedMaxValue = myEnergyResolutionQualified.mEnergyResolutionMax;

                                    } else {

                                        myQualifiedMinValue = gParentWindow.gPeakLowLimit;
                                        myQualifiedMaxValue = gParentWindow.gPeakHighLimit;

                                    }

                                    #endregion

                                }



                            } else {

                                myQualifiedMinValue = gParentWindow.gPeakLowLimit;
                                myQualifiedMaxValue = gParentWindow.gPeakHighLimit;

                            }

                            #endregion

                            #region Decide what data to include in report

                            if (pType == gkEnergyPeak) {

                                //A
                                myPixelCell = new PdfPCell(new Phrase(myFittingParameters.mG2Center.ToString("0.0") + "/", myFontEnglish));


                            } else if (pType == gkEnergyResolution) {
                                //B
                                myPixelCell = new PdfPCell(new Phrase(myFittingParameters.mEnergyResolution.ToString("0.0") + "%" + "/", myFontEnglish));

                            } else if (pType == gkEnergyCount) {
                                //C
                                myPixelCell = new PdfPCell(new Phrase(pFittingData.gCountCnt[i].ToString("0.0") + "Cnt" + "/", myFontEnglish));

                            }

                            #endregion

                            myPixelCell.FixedHeight = gkDefaultCellHeight;

                            myColorIndex = (int)((myQualityTypeValue - myQualityTypeBase) / myQualityTypeStep);

                            if (pIsReverse) {

                                myColorIndex = myColorStep - myColorIndex;
                            
                            }

                            myPixelCell.BackgroundColor = myGreyColor.GenerateGreyColor(myColorIndex);

                        } else {

                            //Can not fitting
                            #region Can not fitting

                            myPixelCell = new PdfPCell(new Phrase());

                            myPixelCell.FixedHeight = gkDefaultCellHeight;

                            myPixelCell.BackgroundColor = BaseColor.WHITE;

                            #endregion

                        }

                        myResultTable.AddCell(myPixelCell);

                    } else {

                        //Not in fitting result
                        #region Not in fitting result

                        myPixelCell = new PdfPCell(new Phrase());

                        myPixelCell.FixedHeight = gkDefaultCellHeight;

                        myPixelCell.BackgroundColor = BaseColor.WHITE;

                        myResultTable.AddCell(myPixelCell);

                        #endregion

                    }

                    #endregion

                } else {

                    //Need add a gray cell
                    //The pixel get leak light not need to save in report
                    #region Leak light pixel

                    myPixelCell = new PdfPCell(new Phrase());

                    myPixelCell.FixedHeight = gkDefaultCellHeight;

                    myPixelCell.BackgroundColor = BaseColor.WHITE;

                    myResultTable.AddCell(myPixelCell);

                    #endregion

                }


            }

            #endregion

            gPDFDocument.Add(myResultTable);

        }

        #endregion

        public void AddQualifiedCountTable(string pTableName, int pColumn, Dictionary<int, cEnergyCountQualified> pQualifiedCount) {

            BaseFont myBFChinese;

            BaseFont myBFTimeNewROMAN;

            Font myFontEnglish;

            Font myFontChinese;

            Font myUseFont;

            myBFTimeNewROMAN = BaseFont.CreateFont ( BaseFont.TIMES_ROMAN, BaseFont.CP1252, false );

            //myBFChinese = BaseFont.CreateFont("STSong-Light", "UniGB-UCS2-H", BaseFont.EMBEDDED);

            string ARIALUNI_TFF = Path.Combine ( Environment.GetFolderPath ( Environment.SpecialFolder.Fonts ), "simhei.ttf" );

            myBFChinese = BaseFont.CreateFont ( ARIALUNI_TFF, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED );

            BaseColor myGoldColor = new BaseColor ( 255, 215, 0 );

            //16x16 Table
            PdfPTable myResultTable = new PdfPTable ( pColumn );

            myResultTable.SpacingBefore = 10f;
            myResultTable.SpacingAfter = 10f;

            myResultTable.HorizontalAlignment = Element.ALIGN_LEFT;

            myResultTable.WidthPercentage = 100f;

            #region Add the table name

            //Use larger size for header
            myFontChinese = new Font ( myBFChinese, 18, iTextSharp.text.Font.BOLD );

            myFontEnglish = new Font ( myBFTimeNewROMAN, 18, iTextSharp.text.Font.BOLD );

            if (gParentWindow.gSelectedLanguage == (int)Demo.gLanguageVersion.Chinese) {

                myUseFont = myFontChinese;

            } else {

                myUseFont = myFontEnglish;

            }


            PdfPCell myTableNameCell = null;

            if (gParentWindow.gSelectedLanguage == (int)Demo.gLanguageVersion.Chinese) {

                myTableNameCell = new PdfPCell ( new Phrase ( gQualifiedEnergyCountNote, myFontChinese ) );

            } else {

                myTableNameCell = new PdfPCell ( new Phrase ( gQualifiedEnergyCountNote, myFontEnglish ) );

            }

            myTableNameCell.FixedHeight = 2 * gkDefaultCellHeight;

            myTableNameCell.Colspan = pColumn;

            //0 is Left, 1 is middle, 2 is right
            myTableNameCell.HorizontalAlignment = 1;

            myResultTable.AddCell ( myTableNameCell );

            #endregion

            //Use small size for notes
            myFontChinese = new Font ( myBFChinese, 8, iTextSharp.text.Font.BOLD );

            myFontEnglish = new Font ( myBFTimeNewROMAN, 8, iTextSharp.text.Font.NORMAL );

            //Add X header
            #region Add X Header

            PdfPCell myEmptyXCell = null;

            if (gParentWindow.gSelectedLanguage == (int)Demo.gLanguageVersion.Chinese) {

                myEmptyXCell = new PdfPCell ( new Phrase ( gQualifiedEnergyCountNote, new Font ( myBFChinese, 5, iTextSharp.text.Font.BOLD ) ) );

            } else {


                myEmptyXCell = new PdfPCell ( new Phrase ( gQualifiedEnergyCountNote, new Font ( myBFTimeNewROMAN, 5, iTextSharp.text.Font.BOLD ) ) );

            }

            myEmptyXCell.FixedHeight = gkDefaultCellHeight;

            myEmptyXCell.BackgroundColor = BaseColor.CYAN;

            myResultTable.AddCell ( myEmptyXCell );


            for (int i = 0; i < 16; i++) {

                PdfPCell myXHeaderCell = new PdfPCell ( new Phrase ( "X" + i.ToString ( ) ) );

                myXHeaderCell.FixedHeight = gkDefaultCellHeight;

                myXHeaderCell.BackgroundColor = BaseColor.CYAN;

                myResultTable.AddCell ( myXHeaderCell );

            }

            #endregion

            //Loop all the content
            myFontEnglish = new Font ( myBFTimeNewROMAN, 5, iTextSharp.text.Font.NORMAL );

            #region Save Data

            for (int i = 0; i < 256; i++) {

                //Add this check because don't want to save the leaking light pixel data
                cFittingParameters myFittingParameters = new cFittingParameters ( );

                PdfPCell myPixelCell = null;

                if (i % 16 == 0) {

                    //If it is a new column
                    //Then add Yn
                    myPixelCell = new PdfPCell ( new Phrase ( "Y" + (i / 16).ToString ( ) ) );

                    myPixelCell.FixedHeight = gkDefaultCellHeight;

                    myPixelCell.BackgroundColor = BaseColor.CYAN;

                    myResultTable.AddCell ( myPixelCell );

                } else {

                    //Will create it later
                    

                }

                myPixelCell = new PdfPCell ( new Phrase ( pQualifiedCount[i].mEnergyCountMin.ToString ( ) + " to " + "\r\n" +
                                                                pQualifiedCount[i].mEnergyCountMax.ToString ( ), myFontEnglish ) );

                myPixelCell.FixedHeight = gkDefaultCellHeight;

                myPixelCell.BackgroundColor = BaseColor.WHITE;

                myResultTable.AddCell ( myPixelCell );



            }

            #endregion

            gPDFDocument.Add ( myResultTable );

        }

        public void AddQualifiedResolutionTable(string pTableName, int pColumn, Dictionary<int, cEnergyResolutionQualified> pQualifiedResolution) {

            BaseFont myBFChinese;

            BaseFont myBFTimeNewROMAN;

            Font myFontEnglish;

            Font myFontChinese;

            Font myUseFont;

            myBFTimeNewROMAN = BaseFont.CreateFont ( BaseFont.TIMES_ROMAN, BaseFont.CP1252, false );

            //myBFChinese = BaseFont.CreateFont("STSong-Light", "UniGB-UCS2-H", BaseFont.EMBEDDED);

            string ARIALUNI_TFF = Path.Combine ( Environment.GetFolderPath ( Environment.SpecialFolder.Fonts ), "simhei.ttf" );

            myBFChinese = BaseFont.CreateFont ( ARIALUNI_TFF, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED );

            BaseColor myGoldColor = new BaseColor ( 255, 215, 0 );

            //16x16 Table
            PdfPTable myResultTable = new PdfPTable ( pColumn );

            myResultTable.SpacingBefore = 10f;
            myResultTable.SpacingAfter = 10f;

            myResultTable.HorizontalAlignment = Element.ALIGN_LEFT;

            myResultTable.WidthPercentage = 100f;

            #region Add the table name

            //Use larger size for header
            myFontChinese = new Font ( myBFChinese, 18, iTextSharp.text.Font.BOLD );

            myFontEnglish = new Font ( myBFTimeNewROMAN, 18, iTextSharp.text.Font.BOLD );

            if (gParentWindow.gSelectedLanguage == (int)Demo.gLanguageVersion.Chinese) {

                myUseFont = myFontChinese;

            } else {

                myUseFont = myFontEnglish;

            }


            PdfPCell myTableNameCell = null;

            if (gParentWindow.gSelectedLanguage == (int)Demo.gLanguageVersion.Chinese) {

                myTableNameCell = new PdfPCell ( new Phrase ( gQualifedEnergyResolutionNote, myFontChinese ) );

            } else {

                myTableNameCell = new PdfPCell ( new Phrase ( gQualifedEnergyResolutionNote, myFontEnglish ) );

            }

            myTableNameCell.FixedHeight = 2 * gkDefaultCellHeight;

            myTableNameCell.Colspan = pColumn;

            //0 is Left, 1 is middle, 2 is right
            myTableNameCell.HorizontalAlignment = 1;

            myResultTable.AddCell ( myTableNameCell );

            #endregion

            //Use small size for notes
            myFontChinese = new Font ( myBFChinese, 8, iTextSharp.text.Font.BOLD );

            myFontEnglish = new Font ( myBFTimeNewROMAN, 8, iTextSharp.text.Font.NORMAL );

            //Add X header
            #region Add X Header

            PdfPCell myEmptyXCell = null;

            if (gParentWindow.gSelectedLanguage == (int)Demo.gLanguageVersion.Chinese) {

                myEmptyXCell = new PdfPCell ( new Phrase ( gQualifedEnergyResolutionNote+"(%)", new Font ( myBFChinese, 5, iTextSharp.text.Font.BOLD ) ) );

            } else {

                myEmptyXCell = new PdfPCell ( new Phrase ( gQualifedEnergyResolutionNote+"(%)", new Font ( myBFTimeNewROMAN, 5, iTextSharp.text.Font.BOLD ) ) );

            }

            myEmptyXCell.FixedHeight = gkDefaultCellHeight;

            myEmptyXCell.BackgroundColor = BaseColor.CYAN;

            myResultTable.AddCell ( myEmptyXCell );


            for (int i = 0; i < 16; i++) {

                PdfPCell myXHeaderCell = new PdfPCell ( new Phrase ( "X" + i.ToString ( ) ) );

                myXHeaderCell.FixedHeight = gkDefaultCellHeight;

                myXHeaderCell.BackgroundColor = BaseColor.CYAN;

                myResultTable.AddCell ( myXHeaderCell );

            }

            #endregion

            //Loop all the content
            myFontEnglish = new Font ( myBFTimeNewROMAN, 5, iTextSharp.text.Font.NORMAL );

            #region Save Data

            for (int i = 0; i < 256; i++) {

                //Add this check because don't want to save the leaking light pixel data
                cFittingParameters myFittingParameters = new cFittingParameters ( );

                PdfPCell myPixelCell = null;

                if (i % 16 == 0) {

                    //If it is a new column
                    //Then add Yn
                    myPixelCell = new PdfPCell ( new Phrase ( "Y" + (i / 16).ToString ( ) ) );

                    myPixelCell.FixedHeight = gkDefaultCellHeight;

                    myPixelCell.BackgroundColor = BaseColor.CYAN;

                    myResultTable.AddCell ( myPixelCell );

                } else {

                    //Will create it later

                }

                
                myPixelCell = new PdfPCell ( new Phrase ( pQualifiedResolution[i].mEnergyResolutionMin.ToString ( ) + " to " + "\r\n" +
                                                            pQualifiedResolution[i].mEnergyResolutionMax.ToString ( ), myFontEnglish ) );

                myPixelCell.FixedHeight = gkDefaultCellHeight;

                myPixelCell.BackgroundColor = BaseColor.WHITE;

                myResultTable.AddCell ( myPixelCell );



            }

            #endregion

            gPDFDocument.Add ( myResultTable );

        }

        public void CreateFinalResultTable( int pColumnNo ) {

            BaseFont myBFChinese;

            BaseFont myBFTimeNewROMAN;

            Font myFontEnglish;

            Font myFontChinese;

            Font myUseFont;

            string ARIALUNI_TFF = Path.Combine( Environment.GetFolderPath( Environment.SpecialFolder.Fonts ), "simhei.ttf" );

            myBFTimeNewROMAN = BaseFont.CreateFont( BaseFont.TIMES_ROMAN, BaseFont.CP1252, false );
            myBFChinese = BaseFont.CreateFont( ARIALUNI_TFF, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED );

            gFinalResultTable = new PdfPTable( pColumnNo );

            gFinalResultTable.SpacingBefore = 10f;
            gFinalResultTable.SpacingAfter = 10f;

            gFinalResultTable.HorizontalAlignment = Element.ALIGN_LEFT;

            gFinalResultTable.WidthPercentage = 100f;

            gFinalResultTableColumn = pColumnNo;

            #region Add the table name

            //Use larger size for header
            myFontChinese = new Font( myBFChinese, 18, iTextSharp.text.Font.BOLD );

            myFontEnglish = new Font( myBFTimeNewROMAN, 18, iTextSharp.text.Font.BOLD );

            if( gParentWindow.gSelectedLanguage == ( int )Demo.gLanguageVersion.Chinese ) {

                myUseFont = myFontChinese;

            } else {

                myUseFont = myFontEnglish;

            }

            PdfPCell myTableNameCell = null;

            if( gParentWindow.gSelectedLanguage == ( int )Demo.gLanguageVersion.Chinese ) {

                myTableNameCell = new PdfPCell( new Phrase( gTableHeader, myFontChinese ) );

            } else {

                myTableNameCell = new PdfPCell( new Phrase( gTableHeader, myFontEnglish ) );

            }

            myTableNameCell.FixedHeight = 2 * gkDefaultCellHeight;

            myTableNameCell.Colspan = pColumnNo;

            //0 is Left, 1 is middle, 2 is right
            myTableNameCell.HorizontalAlignment = 1;

            gFinalResultTable.AddCell( myTableNameCell );

            #endregion

            #region Add  Header

            myFontChinese = new Font( myBFChinese, 8, iTextSharp.text.Font.BOLD );

            myFontEnglish = new Font( myBFTimeNewROMAN, 8, iTextSharp.text.Font.BOLD );

            if( gParentWindow.gSelectedLanguage == ( int )Demo.gLanguageVersion.Chinese ) {

                myUseFont = myFontChinese;

            } else {

                myUseFont = myFontEnglish;

            }

            #region Index Header

            PdfPCell myIndexHeaderCell = new PdfPCell( new Phrase( gPixelIndexInfo, myUseFont ) );

            myIndexHeaderCell.FixedHeight = gkDefaultCellHeight;

            myIndexHeaderCell.BackgroundColor = BaseColor.WHITE;

            myIndexHeaderCell.HorizontalAlignment = 1;

            gFinalResultTable.AddCell( myIndexHeaderCell );

            #endregion

            #region Decide what data to include in report

            if ((gParentWindow.gIsIncludeEnergySpectrumInReport == true) &&
                (gParentWindow.gIsIncludeResolutionInReport == false) &&
                (gParentWindow.gIsIncludeEnergyCountInReport == false)) {

                //A
                #region Energy Header

                PdfPCell myEnergyHeaderCell = new PdfPCell ( new Phrase ( gPixelEnergyInfo, myUseFont ) );

                myEnergyHeaderCell.FixedHeight = gkDefaultCellHeight;

                myEnergyHeaderCell.BackgroundColor = BaseColor.WHITE;

                myEnergyHeaderCell.HorizontalAlignment = 1;

                gFinalResultTable.AddCell ( myEnergyHeaderCell );

                #endregion

            } else if ((gParentWindow.gIsIncludeEnergySpectrumInReport == false) &&
                       (gParentWindow.gIsIncludeResolutionInReport == true) &&
                       (gParentWindow.gIsIncludeEnergyCountInReport == false)) {
                //B
                #region Resolution Header

                PdfPCell myResolutionHeaderCell = new PdfPCell ( new Phrase ( gPixelResolutionInfo, myUseFont ) );

                myResolutionHeaderCell.FixedHeight = gkDefaultCellHeight;

                myResolutionHeaderCell.BackgroundColor = BaseColor.WHITE;

                myResolutionHeaderCell.HorizontalAlignment = 1;

                gFinalResultTable.AddCell ( myResolutionHeaderCell );

                #endregion

            } else if ((gParentWindow.gIsIncludeEnergySpectrumInReport == false) &&
                       (gParentWindow.gIsIncludeResolutionInReport == false) &&
                       (gParentWindow.gIsIncludeEnergyCountInReport == true)) {
                //C
                #region Count Header

                PdfPCell myCountHeaderCell = new PdfPCell ( new Phrase ( gPixelCountInfo, myUseFont ) );

                myCountHeaderCell.FixedHeight = gkDefaultCellHeight;

                myCountHeaderCell.BackgroundColor = BaseColor.WHITE;

                myCountHeaderCell.HorizontalAlignment = 1;

                gFinalResultTable.AddCell ( myCountHeaderCell );

                #endregion

            } else if ((gParentWindow.gIsIncludeEnergySpectrumInReport == true) &&
                       (gParentWindow.gIsIncludeResolutionInReport == true) &&
                       (gParentWindow.gIsIncludeEnergyCountInReport == false)) {
                //AB
                #region Energy Header

                PdfPCell myEnergyHeaderCell = new PdfPCell ( new Phrase ( gPixelEnergyInfo, myUseFont ) );

                myEnergyHeaderCell.FixedHeight = gkDefaultCellHeight;

                myEnergyHeaderCell.BackgroundColor = BaseColor.WHITE;

                myEnergyHeaderCell.HorizontalAlignment = 1;

                gFinalResultTable.AddCell ( myEnergyHeaderCell );

                #endregion

                #region Resolution Header

                PdfPCell myResolutionHeaderCell = new PdfPCell ( new Phrase ( gPixelResolutionInfo, myUseFont ) );

                myResolutionHeaderCell.FixedHeight = gkDefaultCellHeight;

                myResolutionHeaderCell.BackgroundColor = BaseColor.WHITE;

                myResolutionHeaderCell.HorizontalAlignment = 1;

                gFinalResultTable.AddCell ( myResolutionHeaderCell );

                #endregion

            } else if ((gParentWindow.gIsIncludeEnergySpectrumInReport == true) &&
                       (gParentWindow.gIsIncludeResolutionInReport == false) &&
                       (gParentWindow.gIsIncludeEnergyCountInReport == true)) {
                //AC
                #region Energy Header

                PdfPCell myEnergyHeaderCell = new PdfPCell ( new Phrase ( gPixelEnergyInfo, myUseFont ) );

                myEnergyHeaderCell.FixedHeight = gkDefaultCellHeight;

                myEnergyHeaderCell.BackgroundColor = BaseColor.WHITE;

                myEnergyHeaderCell.HorizontalAlignment = 1;

                gFinalResultTable.AddCell ( myEnergyHeaderCell );

                #endregion

                #region Count Header

                PdfPCell myCountHeaderCell = new PdfPCell ( new Phrase ( gPixelCountInfo, myUseFont ) );

                myCountHeaderCell.FixedHeight = gkDefaultCellHeight;

                myCountHeaderCell.BackgroundColor = BaseColor.WHITE;

                myCountHeaderCell.HorizontalAlignment = 1;

                gFinalResultTable.AddCell ( myCountHeaderCell );

                #endregion

            } else if ((gParentWindow.gIsIncludeEnergySpectrumInReport == false) &&
                       (gParentWindow.gIsIncludeResolutionInReport == true) &&
                       (gParentWindow.gIsIncludeEnergyCountInReport == true)) {
                //BC
                #region Resolution Header

                PdfPCell myResolutionHeaderCell = new PdfPCell ( new Phrase ( gPixelResolutionInfo, myUseFont ) );

                myResolutionHeaderCell.FixedHeight = gkDefaultCellHeight;

                myResolutionHeaderCell.BackgroundColor = BaseColor.WHITE;

                myResolutionHeaderCell.HorizontalAlignment = 1;

                gFinalResultTable.AddCell ( myResolutionHeaderCell );

                #endregion

                #region Count Header

                PdfPCell myCountHeaderCell = new PdfPCell ( new Phrase ( gPixelCountInfo, myUseFont ) );

                myCountHeaderCell.FixedHeight = gkDefaultCellHeight;

                myCountHeaderCell.BackgroundColor = BaseColor.WHITE;

                myCountHeaderCell.HorizontalAlignment = 1;

                gFinalResultTable.AddCell ( myCountHeaderCell );

                #endregion

            } else if ((gParentWindow.gIsIncludeEnergySpectrumInReport == true) &&
                       (gParentWindow.gIsIncludeResolutionInReport == true) &&
                       (gParentWindow.gIsIncludeEnergyCountInReport == true)) {
                //ABC
                #region Energy Header

                PdfPCell myEnergyHeaderCell = new PdfPCell ( new Phrase ( gPixelEnergyInfo, myUseFont ) );

                myEnergyHeaderCell.FixedHeight = gkDefaultCellHeight;

                myEnergyHeaderCell.BackgroundColor = BaseColor.WHITE;

                myEnergyHeaderCell.HorizontalAlignment = 1;

                gFinalResultTable.AddCell ( myEnergyHeaderCell );

                #endregion

                #region Resolution Header

                PdfPCell myResolutionHeaderCell = new PdfPCell ( new Phrase ( gPixelResolutionInfo, myUseFont ) );

                myResolutionHeaderCell.FixedHeight = gkDefaultCellHeight;

                myResolutionHeaderCell.BackgroundColor = BaseColor.WHITE;

                myResolutionHeaderCell.HorizontalAlignment = 1;

                gFinalResultTable.AddCell ( myResolutionHeaderCell );

                #endregion

                #region Count Header

                PdfPCell myCountHeaderCell = new PdfPCell ( new Phrase ( gPixelCountInfo, myUseFont ) );

                myCountHeaderCell.FixedHeight = gkDefaultCellHeight;

                myCountHeaderCell.BackgroundColor = BaseColor.WHITE;

                myCountHeaderCell.HorizontalAlignment = 1;

                gFinalResultTable.AddCell ( myCountHeaderCell );

                #endregion

            }

            #endregion

            #endregion


        }

        public void InsertDataToResultTable( cReportPixelInfo[] pPixelInfoData, int pCount, float pMin, float pMax ) {

            BaseFont myBFChinese;

            BaseFont myBFTimeNewROMAN;

            string ARIALUNI_TFF = Path.Combine( Environment.GetFolderPath( Environment.SpecialFolder.Fonts ), "simhei.ttf" );

            myBFTimeNewROMAN = BaseFont.CreateFont( BaseFont.TIMES_ROMAN, BaseFont.CP1252, false );
            myBFChinese = BaseFont.CreateFont( ARIALUNI_TFF, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED );

            BaseColor myGoldColor = new BaseColor( 255, 215, 0 );

            float myEnergyAvg = 0.0f;
            float myResolutionAvg = 0.0f;

            gDelta = ( pMax - pMin ) / 10.0f;

            if( pCount > 0 ) {

                for( int i = 0; i < pCount; i++ ) {

                    #region Loop All data

                    BaseColor myCellColor = BaseColor.WHITE;

                    myEnergyAvg += pPixelInfoData[i].mEnergyValue;
                    myResolutionAvg += pPixelInfoData[i].mResolution;

                    if( gTotalPixelCount == 0 ) {

                        //FIrst time
                        gFinalResultEnergyMin = pPixelInfoData[i].mEnergyValue;
                        gFinalResultEnergyMax = pPixelInfoData[i].mEnergyValue;
                        gFinalResultResolutionMin = pPixelInfoData[i].mResolution;
                        gFinalResultResolutionMax = pPixelInfoData[i].mResolution;

                        gMinTemperature = pPixelInfoData[i].mTemperature;
                        gMaxTempetature = pPixelInfoData[i].mTemperature;

                    } else {

                        if( pPixelInfoData[i].mTemperature < gMinTemperature ) {

                            gMinTemperature = pPixelInfoData[i].mTemperature;

                        } else if( pPixelInfoData[i].mTemperature > gMaxTempetature ) {

                            gMaxTempetature = pPixelInfoData[i].mTemperature;

                        }

                        if( pPixelInfoData[i].mEnergyValue < gFinalResultEnergyMin ) {

                            gFinalResultEnergyMin = pPixelInfoData[i].mEnergyValue;

                        } else if( pPixelInfoData[i].mEnergyValue > gFinalResultEnergyMax ) {

                            gFinalResultEnergyMax = pPixelInfoData[i].mEnergyValue;

                        }

                        if( pPixelInfoData[i].mResolution >= 0 ) {

                            if( pPixelInfoData[i].mResolution < gFinalResultResolutionMin ) {

                                gFinalResultResolutionMin = pPixelInfoData[i].mResolution;

                            } else if( pPixelInfoData[i].mResolution > gFinalResultResolutionMax ) {

                                gFinalResultResolutionMax = pPixelInfoData[i].mResolution;

                            }

                        }

                    }

                    #region Calculate In range, out range count

                    if( ( pPixelInfoData[i].mEnergyValue >= pMin ) && ( pPixelInfoData[i].mEnergyValue <= pMax ) ) {

                        myCellColor = BaseColor.GREEN;

                        gInRangeCount++;

                        #region For Later Histogram

                        if( pPixelInfoData[i].mEnergyValue < pMin + gDelta ) {

                            gHistogramDataCount[0]++;

                        } else if( ( pPixelInfoData[i].mEnergyValue >= pMin + gDelta ) && ( pPixelInfoData[i].mEnergyValue < pMin + 2 * gDelta ) ) {

                            gHistogramDataCount[1]++;

                        } else if( ( pPixelInfoData[i].mEnergyValue >= pMin + 2 * gDelta ) && ( pPixelInfoData[i].mEnergyValue < pMin + 3 * gDelta ) ) {

                            gHistogramDataCount[2]++;

                        } else if( ( pPixelInfoData[i].mEnergyValue >= pMin + 3 * gDelta ) && ( pPixelInfoData[i].mEnergyValue < pMin + 4 * gDelta ) ) {

                            gHistogramDataCount[3]++;

                        } else if( ( pPixelInfoData[i].mEnergyValue >= pMin + 4 * gDelta ) && ( pPixelInfoData[i].mEnergyValue < pMin + 5 * gDelta ) ) {

                            gHistogramDataCount[4]++;

                        } else if( ( pPixelInfoData[i].mEnergyValue >= pMin + 5 * gDelta ) && ( pPixelInfoData[i].mEnergyValue < pMin + 6 * gDelta ) ) {

                            gHistogramDataCount[5]++;

                        } else if( ( pPixelInfoData[i].mEnergyValue >= pMin + 6 * gDelta ) && ( pPixelInfoData[i].mEnergyValue < pMin + 7 * gDelta ) ) {

                            gHistogramDataCount[6]++;

                        } else if( ( pPixelInfoData[i].mEnergyValue >= pMin + 7 * gDelta ) && ( pPixelInfoData[i].mEnergyValue < pMin + 8 * gDelta ) ) {

                            gHistogramDataCount[7]++;

                        } else if( ( pPixelInfoData[i].mEnergyValue >= pMin + 8 * gDelta ) && ( pPixelInfoData[i].mEnergyValue < pMin + 9 * gDelta ) ) {

                            gHistogramDataCount[8]++;

                        } else if( ( pPixelInfoData[i].mEnergyValue >= pMin + 9 * gDelta ) ) {

                            gHistogramDataCount[9]++;

                        }

                        #endregion

                    } else if( pPixelInfoData[i].mEnergyValue < pMin ) {

                        myCellColor = BaseColor.RED;

                        gDownRangeCount++;

                    } else if( pPixelInfoData[i].mEnergyValue > pMax ) {

                        myCellColor = myGoldColor;

                        gUpRangeCount++;

                    }

                    #endregion

                    #region Add Index

                    PdfPCell myIndexCell = new PdfPCell( new Phrase( gTotalPixelCount.ToString( ) ) );

                    myIndexCell.FixedHeight = gkDefaultCellHeight;

                    myIndexCell.BackgroundColor = myCellColor;

                    gFinalResultTable.AddCell( myIndexCell );

                    #endregion

                    #region Decide what data to include in report

                    if ((gParentWindow.gIsIncludeEnergySpectrumInReport == true) &&
                        (gParentWindow.gIsIncludeResolutionInReport == false) &&
                        (gParentWindow.gIsIncludeEnergyCountInReport == false)) {

                        //A
                        #region Add Energy Data

                        PdfPCell myEnergyCell;
                        
                        if (pPixelInfoData[i].mResolution >= 0) {

                            myEnergyCell = new PdfPCell ( new Phrase ( pPixelInfoData[i].mEnergyValue.ToString ( "0.0" ) ) );

                        } else {

                            myEnergyCell = new PdfPCell ( new Phrase ( "N/A" ) );

                        }

                        myEnergyCell.FixedHeight = gkDefaultCellHeight;

                        myEnergyCell.BackgroundColor = myCellColor;

                        gFinalResultTable.AddCell ( myEnergyCell );

                        #endregion

                    } else if ((gParentWindow.gIsIncludeEnergySpectrumInReport == false) &&
                               (gParentWindow.gIsIncludeResolutionInReport == true) &&
                               (gParentWindow.gIsIncludeEnergyCountInReport == false)) {
                        //B
                        #region Add Resolution Data

                        PdfPCell myResolutionCell;

                        if (pPixelInfoData[i].mResolution >= 0) {

                            myResolutionCell = new PdfPCell ( new Phrase ( pPixelInfoData[i].mResolution.ToString ( "0.0" ) + "%" ) );

                        } else {

                            myResolutionCell = new PdfPCell ( new Phrase ( "N/A" ) );


                        }

                        myResolutionCell.FixedHeight = gkDefaultCellHeight;

                        myResolutionCell.BackgroundColor = myCellColor;

                        gFinalResultTable.AddCell ( myResolutionCell );

                        #endregion

                    } else if ((gParentWindow.gIsIncludeEnergySpectrumInReport == false) &&
                               (gParentWindow.gIsIncludeResolutionInReport == false) &&
                               (gParentWindow.gIsIncludeEnergyCountInReport == true)) {
                        //C
                        #region Add Count Data

                        PdfPCell myCountCell;

                        if (pPixelInfoData[i].mCount >= 0) {

                            myCountCell = new PdfPCell ( new Phrase ( pPixelInfoData[i].mCount.ToString ( "0.0" ) ) );

                        } else {

                            myCountCell = new PdfPCell ( new Phrase ( "N/A" ) );
                        
                        }

                        myCountCell.FixedHeight = gkDefaultCellHeight;

                        myCountCell.BackgroundColor = myCellColor;

                        gFinalResultTable.AddCell ( myCountCell );

                        #endregion

                    } else if ((gParentWindow.gIsIncludeEnergySpectrumInReport == true) &&
                               (gParentWindow.gIsIncludeResolutionInReport == true) &&
                               (gParentWindow.gIsIncludeEnergyCountInReport == false)) {
                        //AB
                        #region Add Energy Data

                        PdfPCell myEnergyCell;

                        if (pPixelInfoData[i].mResolution >= 0) {

                            myEnergyCell = new PdfPCell ( new Phrase ( pPixelInfoData[i].mEnergyValue.ToString ( "0.0" ) ) );

                        } else {

                            myEnergyCell = new PdfPCell ( new Phrase ( "N/A" ) );

                        }

                        myEnergyCell.FixedHeight = gkDefaultCellHeight;

                        myEnergyCell.BackgroundColor = myCellColor;

                        gFinalResultTable.AddCell ( myEnergyCell );

                        #endregion

                        #region Add Resolution Data

                        PdfPCell myResolutionCell;

                        if (pPixelInfoData[i].mResolution >= 0) {

                            myResolutionCell = new PdfPCell ( new Phrase ( pPixelInfoData[i].mResolution.ToString ( "0.0" ) + "%" ) );

                        } else {

                            myResolutionCell = new PdfPCell ( new Phrase ( "N/A" ) );


                        }

                        myResolutionCell.FixedHeight = gkDefaultCellHeight;

                        myResolutionCell.BackgroundColor = myCellColor;

                        gFinalResultTable.AddCell ( myResolutionCell );

                        #endregion

                    } else if ((gParentWindow.gIsIncludeEnergySpectrumInReport == true) &&
                               (gParentWindow.gIsIncludeResolutionInReport == false) &&
                               (gParentWindow.gIsIncludeEnergyCountInReport == true)) {
                        //AC
                        #region Add Energy Data

                        PdfPCell myEnergyCell;

                        if (pPixelInfoData[i].mResolution >= 0) {

                            myEnergyCell = new PdfPCell ( new Phrase ( pPixelInfoData[i].mEnergyValue.ToString ( "0.0" ) ) );

                        } else {

                            myEnergyCell = new PdfPCell ( new Phrase ( "N/A" ) );

                        }

                        myEnergyCell.FixedHeight = gkDefaultCellHeight;

                        myEnergyCell.BackgroundColor = myCellColor;

                        gFinalResultTable.AddCell ( myEnergyCell );

                        #endregion

                        #region Add Count Data

                        PdfPCell myCountCell;

                        if (pPixelInfoData[i].mCount >= 0) {

                            myCountCell = new PdfPCell ( new Phrase ( pPixelInfoData[i].mCount.ToString ( "0.0" ) ) );

                        } else {

                            myCountCell = new PdfPCell ( new Phrase ( "N/A" ) );

                        }

                        myCountCell.FixedHeight = gkDefaultCellHeight;

                        myCountCell.BackgroundColor = myCellColor;

                        gFinalResultTable.AddCell ( myCountCell );

                        #endregion

                    } else if ((gParentWindow.gIsIncludeEnergySpectrumInReport == false) &&
                               (gParentWindow.gIsIncludeResolutionInReport == true) &&
                               (gParentWindow.gIsIncludeEnergyCountInReport == true)) {
                        //BC
                        #region Add Resolution Data

                        PdfPCell myResolutionCell;

                        if (pPixelInfoData[i].mResolution >= 0) {

                            myResolutionCell = new PdfPCell ( new Phrase ( pPixelInfoData[i].mResolution.ToString ( "0.0" ) + "%" ) );

                        } else {

                            myResolutionCell = new PdfPCell ( new Phrase ( "N/A" ) );


                        }

                        myResolutionCell.FixedHeight = gkDefaultCellHeight;

                        myResolutionCell.BackgroundColor = myCellColor;

                        gFinalResultTable.AddCell ( myResolutionCell );

                        #endregion

                        #region Add Count Data

                        PdfPCell myCountCell;

                        if (pPixelInfoData[i].mCount >= 0) {

                            myCountCell = new PdfPCell ( new Phrase ( pPixelInfoData[i].mCount.ToString ( "0.0" ) ) );

                        } else {

                            myCountCell = new PdfPCell ( new Phrase ( "N/A" ) );

                        }

                        myCountCell.FixedHeight = gkDefaultCellHeight;

                        myCountCell.BackgroundColor = myCellColor;

                        gFinalResultTable.AddCell ( myCountCell );

                        #endregion

                    } else if ((gParentWindow.gIsIncludeEnergySpectrumInReport == true) &&
                               (gParentWindow.gIsIncludeResolutionInReport == true) &&
                               (gParentWindow.gIsIncludeEnergyCountInReport == true)) {
                        //ABC
                        #region Add Energy Data

                        PdfPCell myEnergyCell;

                        if (pPixelInfoData[i].mResolution >= 0) {

                            myEnergyCell = new PdfPCell ( new Phrase ( pPixelInfoData[i].mEnergyValue.ToString ( "0.0" ) ) );

                        } else {

                            myEnergyCell = new PdfPCell ( new Phrase ( "N/A" ) );

                        }

                        myEnergyCell.FixedHeight = gkDefaultCellHeight;

                        myEnergyCell.BackgroundColor = myCellColor;

                        gFinalResultTable.AddCell ( myEnergyCell );

                        #endregion

                        #region Add Resolution Data

                        PdfPCell myResolutionCell;

                        if (pPixelInfoData[i].mResolution >= 0) {

                            myResolutionCell = new PdfPCell ( new Phrase ( pPixelInfoData[i].mResolution.ToString ( "0.0" ) + "%" ) );

                        } else {

                            myResolutionCell = new PdfPCell ( new Phrase ( "N/A" ) );


                        }

                        myResolutionCell.FixedHeight = gkDefaultCellHeight;

                        myResolutionCell.BackgroundColor = myCellColor;

                        gFinalResultTable.AddCell ( myResolutionCell );

                        #endregion

                        #region Add Count Data

                        PdfPCell myCountCell;

                        if (pPixelInfoData[i].mCount >= 0) {

                            myCountCell = new PdfPCell ( new Phrase ( pPixelInfoData[i].mCount.ToString ( "0.0" ) ) );

                        } else {

                            myCountCell = new PdfPCell ( new Phrase ( "N/A" ) );

                        }

                        myCountCell.FixedHeight = gkDefaultCellHeight;

                        myCountCell.BackgroundColor = myCellColor;

                        gFinalResultTable.AddCell ( myCountCell );

                        #endregion

                    }

                    #endregion

                    #endregion

                    gTotalPixelCount++;
                }

            } else {

                gPdfGroupCount++;

            }

            myEnergyAvg /= pCount;
            myResolutionAvg /= pCount;

            if( gPdfGroupCount > 0 ) {

                gFinalResultEnergyAvg = ( gFinalResultEnergyAvg + myEnergyAvg ) / 2.0f;
                gFinalResultEnergyAvg = ( gFinalResultResolutionAvg + myResolutionAvg ) / 2.0f;

            } else {

                gFinalResultEnergyAvg = myEnergyAvg;
                gFinalResultResolutionAvg = myResolutionAvg;

            }


        }

        public void CreateLookupTable(int pColumnNo) {

            BaseFont myBFChinese;

            BaseFont myBFTimeNewROMAN;

            Font myFontEnglish;

            Font myFontChinese;

            Font myUseFont;

            string ARIALUNI_TFF = Path.Combine ( Environment.GetFolderPath ( Environment.SpecialFolder.Fonts ), "simhei.ttf" );

            myBFTimeNewROMAN = BaseFont.CreateFont ( BaseFont.TIMES_ROMAN, BaseFont.CP1252, false );
            myBFChinese = BaseFont.CreateFont ( ARIALUNI_TFF, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED );

            gLookupTable = new PdfPTable ( pColumnNo );

            gLookupTable.SpacingBefore = 10f;
            gLookupTable.SpacingAfter = 10f;

            gLookupTable.HorizontalAlignment = Element.ALIGN_LEFT;

            gLookupTable.WidthPercentage = 100f;

            gLookupTableColumn = pColumnNo;

            #region Add the table name

            //Use larger size for header
            myFontChinese = new Font ( myBFChinese, 18, iTextSharp.text.Font.BOLD );

            myFontEnglish = new Font ( myBFTimeNewROMAN, 18, iTextSharp.text.Font.BOLD );

            if (gParentWindow.gSelectedLanguage == (int)Demo.gLanguageVersion.Chinese) {

                myUseFont = myFontChinese;

            } else {

                myUseFont = myFontEnglish;

            }

            PdfPCell myTableNameCell = null;

            if (gParentWindow.gSelectedLanguage == (int)Demo.gLanguageVersion.Chinese) {

                myTableNameCell = new PdfPCell ( new Phrase ( gTableHeader, myFontChinese ) );

            } else {

                myTableNameCell = new PdfPCell ( new Phrase ( gTableHeader, myFontEnglish ) );

            }

            myTableNameCell.FixedHeight = 2 * gkDefaultCellHeight;

            myTableNameCell.Colspan = pColumnNo;

            //0 is Left, 1 is middle, 2 is right
            myTableNameCell.HorizontalAlignment = 1;

            gLookupTable.AddCell ( myTableNameCell );

            #endregion

            #region Add  Header

            myFontChinese = new Font ( myBFChinese, 8, iTextSharp.text.Font.BOLD );

            myFontEnglish = new Font ( myBFTimeNewROMAN, 8, iTextSharp.text.Font.BOLD );

            if (gParentWindow.gSelectedLanguage == (int)Demo.gLanguageVersion.Chinese) {

                myUseFont = myFontChinese;

            } else {

                myUseFont = myFontEnglish;

            }

            #region Index Header

            PdfPCell myIndexHeaderCell = new PdfPCell ( new Phrase ( gPixelIndexInfo, myUseFont ) );

            myIndexHeaderCell.FixedHeight = gkDefaultCellHeight;

            myIndexHeaderCell.BackgroundColor = BaseColor.WHITE;

            myIndexHeaderCell.HorizontalAlignment = 1;

            gLookupTable.AddCell ( myIndexHeaderCell );

            #endregion

            #region Energy Min

            PdfPCell myEnergyMinHeaderCell = new PdfPCell ( new Phrase ( gLookupTableEnergyMinInfo, myUseFont ) );

            myEnergyMinHeaderCell.FixedHeight = gkDefaultCellHeight;

            myEnergyMinHeaderCell.BackgroundColor = BaseColor.WHITE;

            myEnergyMinHeaderCell.HorizontalAlignment = 1;

            gLookupTable.AddCell ( myEnergyMinHeaderCell );

            #endregion

            #region Energy Max

            PdfPCell myEnergyMaxHeaderCell = new PdfPCell ( new Phrase ( gLookupTableEnergyMaxInfo, myUseFont ) );

            myEnergyMaxHeaderCell.FixedHeight = gkDefaultCellHeight;

            myEnergyMaxHeaderCell.BackgroundColor = BaseColor.WHITE;

            myEnergyMaxHeaderCell.HorizontalAlignment = 1;

            gLookupTable.AddCell ( myEnergyMaxHeaderCell );

            #endregion

            #region Resolution Min

            PdfPCell myResolutionMinHeaderCell = new PdfPCell ( new Phrase ( gLookupTableResolutionMinInfo, myUseFont ) );

            myResolutionMinHeaderCell.FixedHeight = gkDefaultCellHeight;

            myResolutionMinHeaderCell.BackgroundColor = BaseColor.WHITE;

            myResolutionMinHeaderCell.HorizontalAlignment = 1;

            gLookupTable.AddCell ( myResolutionMinHeaderCell );

            #endregion

            #region Resolution Max

            PdfPCell myResolutionMaxHeaderCell = new PdfPCell ( new Phrase ( gLookupTableResolutionMaxInfo, myUseFont ) );

            myResolutionMaxHeaderCell.FixedHeight = gkDefaultCellHeight;

            myResolutionMaxHeaderCell.BackgroundColor = BaseColor.WHITE;

            myResolutionMaxHeaderCell.HorizontalAlignment = 1;

            gLookupTable.AddCell(myResolutionMaxHeaderCell);

            #endregion

            #endregion


        }

        public void InsertLoopupTableToResultTable(List<cMaxMinValue> pMaxMinArray) {

            BaseFont myBFChinese;

            BaseFont myBFTimeNewROMAN;

            string ARIALUNI_TFF = Path.Combine ( Environment.GetFolderPath ( Environment.SpecialFolder.Fonts ), "simhei.ttf" );

            myBFTimeNewROMAN = BaseFont.CreateFont ( BaseFont.TIMES_ROMAN, BaseFont.CP1252, false );
            myBFChinese = BaseFont.CreateFont ( ARIALUNI_TFF, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED );

            BaseColor myGoldColor = new BaseColor ( 255, 215, 0 );

            BaseColor myCellColor = BaseColor.WHITE;

            int myIndex = 1;

            foreach(cMaxMinValue myValue in pMaxMinArray ) {
            
                PdfPCell myIndexCell = new PdfPCell ( new Phrase ( myIndex.ToString ( ) ) );

                myIndexCell.FixedHeight = gkDefaultCellHeight;

                myIndexCell.BackgroundColor = myCellColor;

                gLookupTable.AddCell ( myIndexCell );

                #region Min Energy Cell
                
                PdfPCell myMinEnergyCell;

                if (myValue.mMinEnergyValue >= 0) {

                    myMinEnergyCell = new PdfPCell ( new Phrase ( myValue.mMinEnergyValue.ToString ( "0.0" ) ) );

                } else {

                    myMinEnergyCell = new PdfPCell ( new Phrase ( "N/A" ) );

                }

                myMinEnergyCell.FixedHeight = gkDefaultCellHeight;

                myMinEnergyCell.BackgroundColor = myCellColor;

                gLookupTable.AddCell ( myMinEnergyCell );

                #endregion

                #region Max Energy Cell
 
                PdfPCell myMaxEnergyCell;

                if (myValue.mMaxEnergyValue >= 0) {

                    myMaxEnergyCell = new PdfPCell ( new Phrase ( myValue.mMaxEnergyValue.ToString ( "0.0" ) ) );

                } else {

                    myMaxEnergyCell = new PdfPCell ( new Phrase ( "N/A" ) );

                }

                myMaxEnergyCell.FixedHeight = gkDefaultCellHeight;

                myMaxEnergyCell.BackgroundColor = myCellColor;

                gLookupTable.AddCell ( myMaxEnergyCell );

                #endregion

                #region Min Resolution Cell
                
                PdfPCell myMinResolutionCell;

                if (myValue.mMinResolutionValue >= 0) {

                    myMinResolutionCell = new PdfPCell ( new Phrase ( myValue.mMinResolutionValue.ToString ( "0.0" ) ) );

                } else {

                    myMinResolutionCell = new PdfPCell ( new Phrase ( "N/A" ) );

                }

                myMinResolutionCell.FixedHeight = gkDefaultCellHeight;

                myMinResolutionCell.BackgroundColor = myCellColor;

                gLookupTable.AddCell ( myMinResolutionCell );

                #endregion

                #region Max Resolution Cell
 
                PdfPCell myMaxResolutionCell;

                if (myValue.mMaxResolutionValue >= 0) {

                    myMaxResolutionCell = new PdfPCell ( new Phrase ( myValue.mMaxResolutionValue.ToString ( "0.0" ) ) );

                } else {

                    myMaxResolutionCell = new PdfPCell ( new Phrase ( "N/A" ) );

                }

                myMaxResolutionCell.FixedHeight = gkDefaultCellHeight;

                myMaxResolutionCell.BackgroundColor = myCellColor;

                gLookupTable.AddCell ( myMaxResolutionCell );

                #endregion

                myIndex++;

            
            }


        }

        public void SaveLookupTable() {

            gPDFDocument.Add ( gLookupTable );

        }


        public void SaveResultTable( ) {

            gPDFDocument.Add( gFinalResultTable );

        }

        public void CreateCoverPage( int pColumn, string pTempRange, float pRangeMin, float pRangeMax, float pEnergyMin, 
            float pEnergyMax, float pEnergyAvg,float pResolutionMin, float pResolutionMax, float pResolutionAvg, 
            int pInRangeCount, int pUpRangeCount, int pDownRangeCount, string pHistogramFile) {

            string myTimestamp = "";

            BaseFont myBFChinese;

            BaseFont myBFTimeNewROMAN;

            Font myFontEnglish;

            Font myFontChinese;

            Font myUseFont;

            //BaseFont.AddToResourceSearch("iTextAsian.dll");

            //BaseFont.AddToResourceSearch("iTextAsianCmaps.dll");

            myBFTimeNewROMAN = BaseFont.CreateFont( BaseFont.TIMES_ROMAN, BaseFont.CP1252, false );

            //myBFChinese = BaseFont.CreateFont("STSong-Light", "UniGB-UCS2-H", BaseFont.EMBEDDED);

            string ARIALUNI_TFF = Path.Combine( Environment.GetFolderPath( Environment.SpecialFolder.Fonts ), "simhei.ttf" );
            myBFChinese = BaseFont.CreateFont( ARIALUNI_TFF, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED );

            BaseColor myGoldColor = new BaseColor( 255, 215, 0 );

            //16x16 Table
            PdfPTable myResultTable = new PdfPTable( pColumn );

            myResultTable.SpacingBefore = 10f;
            myResultTable.SpacingAfter = 10f;

            myResultTable.HorizontalAlignment = Element.ALIGN_LEFT;

            myResultTable.WidthPercentage = 100f;

            #region Add the table name

            //Use larger size for header
            myFontChinese = new Font( myBFChinese, 18, iTextSharp.text.Font.BOLD );

            myFontEnglish = new Font( myBFTimeNewROMAN, 18, iTextSharp.text.Font.BOLD );

            if( gParentWindow.gSelectedLanguage == ( int )Demo.gLanguageVersion.Chinese ) {

                myUseFont = myFontChinese;

            } else {

                myUseFont = myFontEnglish;

            }


            PdfPCell myTableNameCell = null;

            if( gParentWindow.gSelectedLanguage == ( int )Demo.gLanguageVersion.Chinese ) {

                myTableNameCell = new PdfPCell( new Phrase( gTableHeader, myFontChinese ) );

            } else {

                myTableNameCell = new PdfPCell( new Phrase( gTableHeader, myFontEnglish ) );

            }

            myTableNameCell.FixedHeight = 2 * gkDefaultCellHeight;

            myTableNameCell.Colspan = pColumn;

            //0 is Left, 1 is middle, 2 is right
            myTableNameCell.HorizontalAlignment = 1;

            myResultTable.AddCell( myTableNameCell );

            #endregion

            //Use small size for notes
            myFontChinese = new Font( myBFChinese, 8, iTextSharp.text.Font.BOLD );

            myFontEnglish = new Font( myBFTimeNewROMAN, 8, iTextSharp.text.Font.NORMAL );

            #region Add Timestamp and temperature

            PdfPCell myTimestampCell = null;

            myTimestamp = DateTime.Now.ToShortDateString( ) + " " + DateTime.Now.ToShortTimeString( );

            if( gParentWindow.gSelectedLanguage == ( int )Demo.gLanguageVersion.Chinese ) {

               //myTimestampCell = new PdfPCell( new Phrase( "时间：" + myTimestamp + "    " + "温度范围（C）： " + pTempRange, myFontChinese ) );
                myTimestampCell = new PdfPCell( new Phrase( "时间：" + myTimestamp, myFontChinese ) );

            } else {

                //myTimestampCell = new PdfPCell( new Phrase( "Timestamp: " + myTimestamp + "    " + "Temperature range(C): " + pTempRange, myFontEnglish ) );
                myTimestampCell = new PdfPCell( new Phrase( "Timestamp: " + myTimestamp, myFontEnglish ) );

            }

            myTimestampCell.FixedHeight = gkDefaultCellHeight;

            myTimestampCell.Colspan = pColumn;

            //0 is Left, 1 is middle, 2 is right
            myTimestampCell.HorizontalAlignment = 0;

            myResultTable.AddCell( myTimestampCell );

            #endregion

            //Add Red Notes Part

            #region Green Notes

            PdfPCell myGreenCell = null;

            if( gParentWindow.gSelectedLanguage == ( int )Demo.gLanguageVersion.Chinese ) {

                myGreenCell = new PdfPCell( new Phrase( gGreen, myFontChinese ) );

            } else {

                myGreenCell = new PdfPCell( new Phrase( gGreen, myFontEnglish ) );

            }

            myGreenCell.FixedHeight = gkDefaultCellHeight;

            myGreenCell.Colspan = 4;

            myGreenCell.BackgroundColor = BaseColor.GREEN;

            myResultTable.AddCell( myGreenCell );

            PdfPCell myGreenNotesCell = null;

            if( gParentWindow.gSelectedLanguage == ( int )Demo.gLanguageVersion.Chinese ) {

                myGreenNotesCell = new PdfPCell( new Phrase( gInRangeMessage + pRangeMin.ToString( "0.0" ) + " - " + pRangeMax.ToString( "0.0" ), myFontChinese ) );

            } else {

                myGreenNotesCell = new PdfPCell( new Phrase( gInRangeMessage + pRangeMin.ToString( "0.0" ) + " - " + pRangeMax.ToString( "0.0" ), myFontEnglish ) );

            }
            myGreenNotesCell.FixedHeight = gkDefaultCellHeight;

            myGreenNotesCell.Colspan = pColumn - 4;

            //Middle Allignment
            myGreenNotesCell.HorizontalAlignment = 1;

            myGreenNotesCell.BackgroundColor = BaseColor.WHITE;

            myResultTable.AddCell( myGreenNotesCell );

            #endregion

            #region Red Notes

            PdfPCell myRedCell = null;

            if( gParentWindow.gSelectedLanguage == ( int )Demo.gLanguageVersion.Chinese ) {

                myRedCell = new PdfPCell( new Phrase( gRed, myFontChinese ) );

            } else {

                myRedCell = new PdfPCell( new Phrase( gRed, myFontEnglish ) );

            }

            myRedCell.FixedHeight = gkDefaultCellHeight;

            myRedCell.Colspan = 4;

            myRedCell.BackgroundColor = BaseColor.RED;

            myResultTable.AddCell( myRedCell );

            PdfPCell myRedNotesCell = null;

            if( gParentWindow.gSelectedLanguage == ( int )Demo.gLanguageVersion.Chinese ) {

                myRedNotesCell = new PdfPCell( new Phrase( gRedNoteMessage + pRangeMin.ToString( "0.0" ), myFontChinese ) );

            } else {

                myRedNotesCell = new PdfPCell( new Phrase( gRedNoteMessage + pRangeMin.ToString( "0.0" ), myFontEnglish ) );

            }
            myRedNotesCell.FixedHeight = gkDefaultCellHeight;

            myRedNotesCell.Colspan = pColumn - 4;

            //Middle Allignment
            myRedNotesCell.HorizontalAlignment = 1;

            myRedNotesCell.BackgroundColor = BaseColor.WHITE;


            myResultTable.AddCell( myRedNotesCell );

            #endregion

            #region Yellow Notes

            PdfPCell myYellowCell = null;

            if( gParentWindow.gSelectedLanguage == ( int )Demo.gLanguageVersion.Chinese ) {

                myYellowCell = new PdfPCell( new Phrase( gYellow, myFontChinese ) );

            } else {

                myYellowCell = new PdfPCell( new Phrase( gYellow, myFontEnglish ) );

            }

            myYellowCell.FixedHeight = gkDefaultCellHeight;

            myYellowCell.Colspan = 4;

            //Gold color
            myYellowCell.BackgroundColor = myGoldColor;

            myResultTable.AddCell( myYellowCell );

            PdfPCell myYellowNotesCell = null;

            if( gParentWindow.gSelectedLanguage == ( int )Demo.gLanguageVersion.Chinese ) {

                myYellowNotesCell = new PdfPCell( new Phrase( gYellowNoteMessage + pRangeMax.ToString( "0.0" ), myFontChinese ) );

            } else {

                myYellowNotesCell = new PdfPCell( new Phrase( gYellowNoteMessage + pRangeMax.ToString( "0.0" ), myFontEnglish ) );

            }
            myYellowNotesCell.FixedHeight = gkDefaultCellHeight;

            myYellowNotesCell.Colspan = pColumn - 4;

            //Middle Allignment
            myYellowNotesCell.HorizontalAlignment = 1;

            myYellowNotesCell.BackgroundColor = BaseColor.WHITE;


            myResultTable.AddCell( myYellowNotesCell );

            #endregion

            //Add Energy Min and Max
            #region Energy Min and Max Notes

            PdfPCell myEnergyCell = null;

            if( gParentWindow.gSelectedLanguage == ( int )Demo.gLanguageVersion.Chinese ) {

                myEnergyCell = new PdfPCell( new Phrase( gEnergyNotes, myFontChinese ) );

            } else {

                myEnergyCell = new PdfPCell( new Phrase( gEnergyNotes, myFontEnglish ) );

            }

            myEnergyCell.FixedHeight = gkDefaultCellHeight;

            myEnergyCell.Colspan = 4;

            myEnergyCell.BackgroundColor = BaseColor.WHITE;

            myResultTable.AddCell( myEnergyCell );

            PdfPCell myEnergyValueCell = null;

            if( gParentWindow.gSelectedLanguage == ( int )Demo.gLanguageVersion.Chinese ) {

                myEnergyValueCell = new PdfPCell( new Phrase( pEnergyMin.ToString( "0.0" ) + ", " + pEnergyMax.ToString( "0.0" ) + ", " + pEnergyAvg.ToString( "0.0" ), myFontChinese ) );

            } else {

                myEnergyValueCell = new PdfPCell( new Phrase( pEnergyMin.ToString( "0.0" ) + ", " + pEnergyMax.ToString( "0.0" ) + ", " + pEnergyAvg.ToString( "0.0" ), myFontEnglish ) );

            }

            myEnergyValueCell.FixedHeight = gkDefaultCellHeight;

            myEnergyValueCell.Colspan = pColumn - 4;

            //Middle Allignment
            myEnergyValueCell.HorizontalAlignment = 1;

            myEnergyValueCell.BackgroundColor = BaseColor.WHITE;

            myResultTable.AddCell( myEnergyValueCell );

            #endregion

            //Only if it required the resolution in final report
            if( gParentWindow.gIsIncludeResolutionInReport ) {
             
                #region Energy Resolution Min and Max Notes

                PdfPCell myEnergyResolutionCell = null;

                if( gParentWindow.gSelectedLanguage == ( int )Demo.gLanguageVersion.Chinese ) {

                    myEnergyResolutionCell = new PdfPCell( new Phrase( gEnergyResolutionNotes, myFontChinese ) );

                } else {

                    myEnergyResolutionCell = new PdfPCell( new Phrase( gEnergyResolutionNotes, myFontEnglish ) );

                }

                myEnergyResolutionCell.FixedHeight = gkDefaultCellHeight;

                myEnergyResolutionCell.Colspan = 4;

                myEnergyResolutionCell.BackgroundColor = BaseColor.WHITE;

                myResultTable.AddCell( myEnergyResolutionCell );

                PdfPCell myEnergyResolutionValueCell = null;

                if( ( pResolutionMin >= 0 ) && ( pResolutionMax >= 0 ) ) {

                    if( gParentWindow.gSelectedLanguage == ( int )Demo.gLanguageVersion.Chinese ) {

                        myEnergyResolutionValueCell = new PdfPCell( new Phrase( pResolutionMin.ToString( "0.0" ) + "% , " + pResolutionMax.ToString( "0.0" ) + "%, " + pResolutionAvg.ToString( "0.0" ) + "%", myFontChinese ) );

                    } else {

                        myEnergyResolutionValueCell = new PdfPCell( new Phrase( pResolutionMin.ToString( "0.0" ) + "% , " + pResolutionMax.ToString( "0.0" ) + "%, " + pResolutionAvg.ToString( "0.0" ) + "%", myFontEnglish ) );

                    }

                } else {

                    if( gParentWindow.gSelectedLanguage == ( int )Demo.gLanguageVersion.Chinese ) {

                        myEnergyResolutionValueCell = new PdfPCell( new Phrase( gkCanNotCalculateResolutionCHA, myFontChinese ) );

                    } else {

                        myEnergyResolutionValueCell = new PdfPCell( new Phrase( gkCanNotCalculateResolutionENG, myFontEnglish ) );

                    }
                
                
                }



                myEnergyResolutionValueCell.FixedHeight = gkDefaultCellHeight;

                myEnergyResolutionValueCell.Colspan = pColumn - 4;

                //Middle Allignment
                myEnergyResolutionValueCell.HorizontalAlignment = 1;

                myEnergyResolutionValueCell.BackgroundColor = BaseColor.WHITE;

                myResultTable.AddCell( myEnergyResolutionValueCell );

                #endregion

            } 

            #region Count Notes

            PdfPCell myCountSummaryNotesCell = null;

            if( gParentWindow.gSelectedLanguage == ( int )Demo.gLanguageVersion.Chinese ) {

                myCountSummaryNotesCell = new PdfPCell( new Phrase( gCountSummaryNotes, myFontChinese ) );

            } else {

                myCountSummaryNotesCell = new PdfPCell( new Phrase( gCountSummaryNotes, myFontEnglish ) );

            }

            myCountSummaryNotesCell.FixedHeight = gkDefaultCellHeight;

            myCountSummaryNotesCell.Colspan = 4;

            myCountSummaryNotesCell.BackgroundColor = BaseColor.WHITE;

            myResultTable.AddCell( myCountSummaryNotesCell );

            PdfPCell myCountSummaryCell = null;

            if( gParentWindow.gSelectedLanguage == ( int )Demo.gLanguageVersion.Chinese ) {

                myCountSummaryCell = new PdfPCell( new Phrase( pInRangeCount.ToString( "" ) + " , " + pUpRangeCount.ToString( "" ) + " , " + pDownRangeCount.ToString( "" ), myFontChinese ) );

            } else {

                myCountSummaryCell = new PdfPCell( new Phrase( pInRangeCount.ToString( "" ) + " , " + pUpRangeCount.ToString( "" ) + " , " + pDownRangeCount.ToString( "" ), myFontEnglish ) );

            }

            myCountSummaryCell.FixedHeight = gkDefaultCellHeight;

            myCountSummaryCell.Colspan = pColumn - 4;

            //Middle Allignment
            myCountSummaryCell.HorizontalAlignment = 1;

            myCountSummaryCell.BackgroundColor = BaseColor.WHITE;

            myResultTable.AddCell( myCountSummaryCell );

            #endregion

            gPDFDocument.Add( myResultTable );

            gPDFDocument.Add( new Paragraph( ) );

            for( int i = 0; i < 3; i++ ) {
             
                gPDFDocument.Add( Chunk.NEWLINE );

            }

            gPDFDocument.Add( new Paragraph( "Histogram" ) );

            Image myHistogram = Image.GetInstance( pHistogramFile);

            gPDFDocument.Add( myHistogram );


        }


        public void GetFinalReportParameters( out int pInRangeCount, out int pUpRangeCount, out int pDownRangeCount,
            out float pFinalResultEnergyMin, out float pFinalResultEnergyMax, out float pFinalResultEnergyAvg,
            out float pFinalResultResolutionMin, out float pFinalResultResolutionMax, out float pFinalResultResolutionAvg ) {

            pInRangeCount = 0;
            pUpRangeCount = 0;
            pDownRangeCount = 0;

            //Give min a very small value to get over write for first time
            pFinalResultEnergyMin = 65536.0f;
            pFinalResultEnergyMax = 0.0f;
            pFinalResultEnergyAvg = 0.0f;

            pFinalResultResolutionMin = 100.0f;
            pFinalResultResolutionMax = 0.0f;
            pFinalResultResolutionAvg = 0.0f;

            pInRangeCount = gInRangeCount;
            pUpRangeCount = gUpRangeCount;
            pDownRangeCount = gDownRangeCount;

            pFinalResultEnergyMin = gFinalResultEnergyMin;
            pFinalResultEnergyMax = gFinalResultEnergyMax;
            pFinalResultEnergyAvg = gFinalResultEnergyAvg;

            pFinalResultResolutionMin = gFinalResultResolutionMin;
            pFinalResultResolutionMax = gFinalResultResolutionMax;
            pFinalResultResolutionAvg = gFinalResultResolutionAvg;

        }


        public void AddNewPage( ) {

            gPDFDocument.NewPage( );
        
        
        }

        public void AddHeaderMiddleAligned(string pHeader) {

            Paragraph myNewParagraph = new Paragraph(pHeader, new Font(gBFTimeNewROMAN, 18, iTextSharp.text.Font.BOLD));

            myNewParagraph.Alignment = Element.ALIGN_MIDDLE;

            gPDFDocument.Add(myNewParagraph);

            gPDFDocument.Add(Chunk.NEWLINE);
        
        }

        public void AddPngToReport(string pFileName, int pPixelName, bool pNewParagraph) {

            Font myFont = new Font(gBFTimeNewROMAN, 5, iTextSharp.text.Font.NORMAL );

            if( true ) {

                if( gTrackImagePerLine == 0 ) {

                    gPngParagraph = new Paragraph( );
                
                }

                Image myHistogram = Image.GetInstance( pFileName );

                if( (gParentWindow.gMaxPixelPerLine > 8) && (gParentWindow.gMaxPixelPerLine < 14) ) {

                    myHistogram.ScalePercent( 15 );

                } else if( gParentWindow.gMaxPixelPerLine < 8 ) {

                    myHistogram.ScalePercent( 25 );

                } else {

                    myHistogram.ScalePercent( 10 );
                
                }

                gPngParagraph.Font = myFont;


                if( ( ( int )gParentWindow.gSelectedScanParametersForDataAcq.gSourceType >= ( int )cScanParameters.eSourceType.LightShare )  ) {

                    if( gTrackPixelNo == -1 ) {

                        gTrackPixelNo = pPixelName;
                        gPngParagraph.Add( pPixelName.ToString( "000" ) );

                    } else {

                        gTrackPixelNo++;
                        gPngParagraph.Add( gTrackPixelNo.ToString( "000" ) );

                    }

                } else {
                 
                    gPngParagraph.Add( pPixelName.ToString( "000" ) );

                }

                gPngParagraph.Add( new Chunk( myHistogram, 0, 0, true ) );

                gTrackImagePerLine++;

                if( gParentWindow.gNewLinePixel.ContainsKey( pPixelName ) && ( gTrackImagePerLine != 0 ) ) {

                    gTrackImagePerLine = 0;
                    gPDFDocument.Add( gPngParagraph );

                }

                

            }

        }

        public void AddRestPngToReport( ) {

            if( gTrackImagePerLine > 0 ) {
             
                gTrackImagePerLine = 0;
                gPDFDocument.Add( gPngParagraph );

            }
        
        }

        public void AddPngToReport(string pFileName) {

            Image myHistogram = Image.GetInstance(pFileName);

            Paragraph myPngParagraph = new Paragraph();

            myPngParagraph.Add(new Chunk(myHistogram, 0, 0, true));

            gPDFDocument.Add(myPngParagraph);


        }

        public void Close( ) {

            gPDFDocument.Close( );

            gPDFWriter.Close( );

            gReportFileStream.Close( );

        }

    }

    #region cPDFHeaderFooterEvent

    class cPDFHeaderFooterEvent:PdfPageEventHelper {

        private string gPDFHeader = "";
        private string gPDFFooter = "";

        public cPDFHeaderFooterEvent( string pPDFHeader, string pPDFFooter ) {

            gPDFHeader = pPDFHeader;

            gPDFFooter = pPDFFooter;

        }

        iTextSharp.text.Font FONT = new iTextSharp.text.Font( iTextSharp.text.Font.NORMAL, 18, iTextSharp.text.Font.BOLD );

        public override void OnEndPage( PdfWriter writer, Document document ) {

            PdfContentByte canvas = writer.DirectContent;
            ColumnText.ShowTextAligned(
              canvas, Element.ALIGN_LEFT,
              new Phrase( gPDFHeader, FONT ), 25, 810, 0
            );
            ColumnText.ShowTextAligned(
              canvas, Element.ALIGN_LEFT,
              new Phrase( gPDFFooter, FONT ), 25, 10, 0
            );
        }
    }

    #endregion

}

