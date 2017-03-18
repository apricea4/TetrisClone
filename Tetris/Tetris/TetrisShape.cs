using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Drawing;
using System.Collections;

namespace Tetris
{
    class TetrisShape
    {

        public int[,] container = new int[4, 4];
        public int[] gameBoardRef = new int[2]; //<R,C> bottom left of container
        public List<Point> squarePoints;
        public int shapeColor;


        public TetrisShape() //switch to array  arraylist sucks need references to the point object in it for squarePoints
        {

            this.squarePoints = new List<Point>();
            Random randy = new Random();
            int shapeNum = randy.Next(3);
            while(shapeNum == 0)
            {
              shapeNum = randy.Next(3);
            }
            //int shapeNum = 1;
            if(shapeNum == 1)
            {
                for (int i = 0; i < 4; i++)
                {
                    this.squarePoints.Add(new Point(5, i));

                }
                this.shapeColor = 1;


            }


           if(shapeNum == 2)
            {
                this.squarePoints.Add(new Point(4, 0));
                this.squarePoints.Add(new Point(3, 0));
                this.squarePoints.Add(new Point(3, 1));
                this.squarePoints.Add(new Point(3, 2));
                this.shapeColor = 2;

            }
            

            

        }

        public Color getColor()
        {
            switch(this.shapeColor)
            { 
                case 1:
                return Color.Red;

                case 2:
                    return Color.BlueViolet;

                case 3:
                    return Color.DarkSeaGreen;

                case 4:
                    return Color.LightGoldenrodYellow;

                case 5:
                    return Color.Purple;

                case 6:
                    return Color.OrangeRed;

                case 7:
                    return Color.HotPink;
                    
            }
            return Color.Black;
            

        }
        /*public  moveDown(int i)
        {
            this.squarePoints[i].Y += 1;

        }
        */

        public int[,] getRandomShape()
        {

            //TODO: implement creating a random rotation to start out each shape
            //only returns the bar shape as of now
            int[,] bar = new int[4, 4];
            for (int i = 0; i < 4; i++)
            {
                bar[3, i] = 1; //1 is red

            }
            this.gameBoardRef[0] = 3;
            this.squarePoints = new List<Point>();
            for(int i = 0; i < 4; i++)
            {
                this.squarePoints.Add(new Point(i, 5));

            }

            return bar;


        }



    }

    


}
