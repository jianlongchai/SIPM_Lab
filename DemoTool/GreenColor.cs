using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTextSharp.text;

public class GreenColor {

    const int gkQueueSize = 6;

    BaseColor[] gColorQueue = new BaseColor[gkQueueSize];

    /*
    public GreenColor() {

        gColorQueue[0] = new BaseColor(0, 102, 0);
        gColorQueue[1] = new BaseColor(0, 153, 0);
        gColorQueue[2] = new BaseColor(0, 204, 0);
        gColorQueue[3] = new BaseColor(0, 255, 0);
        gColorQueue[4] = new BaseColor(51, 255, 51);
        gColorQueue[5] = new BaseColor(102, 255, 102);
        gColorQueue[6] = new BaseColor(153, 255, 153);
        gColorQueue[7] = new BaseColor(204, 255, 204);

    }*/
    
    public GreenColor() {

        gColorQueue[0] = new BaseColor(96, 96, 96);
        gColorQueue[1] = new BaseColor(128, 128, 128);
        gColorQueue[2] = new BaseColor(160, 160, 160);
        gColorQueue[3] = new BaseColor(192, 192, 192);
        gColorQueue[4] = new BaseColor(224, 224, 224);
        gColorQueue[5] = new BaseColor(255, 255, 255);

    }
    public int GetGreyStep() {

        return gColorQueue.Count();

    }

    public BaseColor GenerateGreyColor(int pColorLevel) {

        if (pColorLevel >= gColorQueue.Count()) {

            pColorLevel = gColorQueue.Count() - 1;

        } else if (pColorLevel < 0) {

            pColorLevel = 0;

        }

        BaseColor myColor = gColorQueue[pColorLevel];

        return myColor;

    }

}
