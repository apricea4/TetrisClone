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
        public Point topLeft;
        public int rotateAngle;


        public TetrisShape() 
        {

            this.squarePoints = new List<Point>();
            Random randy = new Random();
            int shapeNum = randy.Next(8);
            while(shapeNum == 0)
            {
              shapeNum = randy.Next(8);
            }
            
            if(shapeNum == 1)
            {
                this.topLeft = new Point(5, 0);
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


           if(shapeNum == 3)
            {

                this.squarePoints.Add(new Point(1, 0));
                this.squarePoints.Add(new Point(2, 0));
                this.squarePoints.Add(new Point(2, 1));
                this.squarePoints.Add(new Point(2, 2));
                this.shapeColor = 3;


            }
            
           if(shapeNum == 4)
            {
                this.squarePoints.Add(new Point(2, 0));
                this.squarePoints.Add(new Point(2, 1));
                this.squarePoints.Add(new Point(3, 0));
                this.squarePoints.Add(new Point(3, 1));
                this.shapeColor = 4;


            }

           if(shapeNum == 5)
            {
                this.squarePoints.Add(new Point(1, 0));
                this.squarePoints.Add(new Point(1, 1));
                this.squarePoints.Add(new Point(2, 1));
                this.squarePoints.Add(new Point(2, 2));
                this.shapeColor = 5;


            }

           if(shapeNum == 6)
            {
                this.squarePoints.Add(new Point(1, 1));
                this.squarePoints.Add(new Point(2, 0));
                this.squarePoints.Add(new Point(2, 1));
                this.squarePoints.Add(new Point(3, 1));
                this.shapeColor = 6;


            }

           if(shapeNum == 7)
            {

                this.squarePoints.Add(new Point(2, 0));
                this.squarePoints.Add(new Point(1, 1));
                this.squarePoints.Add(new Point(2, 1));
                this.squarePoints.Add(new Point(1, 2));
                this.shapeColor = 7;


            }







            

        }

        


        public void setRotatedShapePoints(int degree)
        {
            int xOrig = this.squarePoints[2].X;
            int yOrig = this.squarePoints[2].Y;
            double rad = (Math.PI * degree) / 180;
            int[,] rotationMatrix = new int[2, 2];
            rotationMatrix[0, 0] = (int)Math.Cos(rad);
            rotationMatrix[1, 1] = (int)Math.Cos(rad);
            rotationMatrix[0, 1] = -(int)Math.Sin(rad);
            rotationMatrix[1, 0] = (int)Math.Sin(rad);
            for(int i = 0; i < this.squarePoints.Count; i++)
            {
                int[,] pointVector = new int[2, 1];
                pointVector[0, 0] = this.squarePoints[i].X - xOrig;
                pointVector[1, 0] = this.squarePoints[i].Y - yOrig;
                this.squarePoints[i] = new Point(((rotationMatrix[0,0] * pointVector[0,0]) + (rotationMatrix[0,1] * pointVector[1,0])) + xOrig, ((rotationMatrix[1, 0] * pointVector[0, 0]) + (rotationMatrix[1, 1] * pointVector[1, 0])) + yOrig);


            }
            return;
            



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
                    return Color.Orange;

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
