using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Collections;




namespace Tetris
{
    public partial class MainForm : Form
    {


        public const int topOffset = 50;
        public const int leftOffset = 50;
        public const int widthOfSquare = 40; //40px
        public int[,] mainGameBoard = new int[18, 10]; //each square is 40px by 40px
        TetrisShape fallingBlock = new TetrisShape();
        Rectangle paintBoard = new Rectangle(49, 49, 405, 725);
        Thread t;
        public MainForm()
        {
            InitializeComponent();
            //initializeGame();
            manageFallingBlock();
            //t = new Thread(new ThreadStart(run));
            //t.Start();
        }

       
        
        public void initializeGame()
        {
            for (int i = 0; i< 18; i++)
            {
                for(int j = 0; j < 10; j++)
                {
                    mainGameBoard[i, j] = 0;

                }

            }

        }

        protected override void OnPaint(PaintEventArgs e) //loop through entire gameboard and look at each number to decide color to paint
        {
            //have a pen for drawing the falling block then a pen and brush for drawing the 
            //gameboard  set the color every iteration
            Graphics graphic = e.Graphics;
            Pen p = new Pen(fallingBlock.getColor(), 3);//pen for falling block
            DoubleBuffered = true;
            Pen boardPen; //pen for board
            SolidBrush boardBrosh;
            LinearGradientBrush brush = new LinearGradientBrush(paintBoard, Color.White, Color.Black,90.0F);
            graphic.FillRectangle(brush, paintBoard);
            SolidBrush brosh = new SolidBrush(fallingBlock.getColor()); //brush for filling in falling block
            for (int i = 0; i < 18; i++) //rows are Y
            {

                for (int j = 0; j < 10; j++)//columns are X
                {
                    Point checkPoint = new Point(j, i);
                    //split this if or statement up to check whether i need to paint falling block or 
                    //the gameboard which will hvae diffeernt colors
                    if(fallingBlock.squarePoints.Contains(checkPoint) )
                    {
                        Rectangle r;
                        graphic.DrawRectangle(p, getOffset(j), getOffset(i), 40, 40);
                        r = new Rectangle(getOffset(j), getOffset(i), 40, 40);
                        graphic.FillRectangle(brosh, r);
                        //do switch here based on color but for now only doing one color
                        //mainGameBoard[i, j] = 1;
                    }

                    if(mainGameBoard[i, j] != 0)
                    {

                        Rectangle boardRect = new Rectangle(getOffset(j), getOffset(i), 40,40);
                        boardPen = new Pen(colorLookUp(mainGameBoard[i, j]), 3);
                        graphic.DrawRectangle(boardPen, getOffset(j), getOffset(i), 40, 40);
                        boardBrosh = new SolidBrush(colorLookUp(mainGameBoard[i, j]));
                        graphic.FillRectangle(boardBrosh, boardRect);

                    }


                }


              }


                    /*for(int i =0; i<18; i++)
                    {

                        for(int j = 0; j< 10; j++)
                        {
                            if (mainGameBoard[i,j] != 0)
                            {
                                graphic.DrawRectangle(p, getOffset(j), getOffset(i), 40, 40);

                            }

                        }

                    }
                    
                    */




                    base.OnPaint(e);
           
        }
        public Color colorLookUp(int colorInt)
        {
            switch (colorInt)
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


        /* public void run()
         {
             //Point p;

             //probaly can keep threading just check that when changing to array that the reference is kept and y value is actually changed.

            // while (true)
             //{



                 for (int i = 0; i < fallingBlock.squarePoints.Count; i++)
                 {
                     Point p = fallingBlock.squarePoints[i];
                      fallingBlock.squarePoints[i] = new Point(p.X, p.Y + 1);


                 }
                 Invalidate();
                 //Thread.Sleep(1000);
             //}
             int containerR;
             int containerC = fallingBlock.gameBoardRef[1];
             while (fallingBlock.gameBoardRef[0] < 18)
             {
                 containerR = fallingBlock.gameBoardRef[0];


                 for (int GR = 0; GR < 18; GR++)
                 {
                     for (int GC = 0; GC < 10; GC++)

                     {
                         //if(!(container - gr > 3) && !(containerR - gc > 3)
                         if(containerR - GR < 4 && containerR - GR > -1 && containerR - GC < 4 && containerR - GC > -1) 
                         //if (!(GR + containerR >=(2 * containerR) - 1) && !(GC + containerR >= (2 * containerR) - 1))
                         {
                             mainGameBoard[GR, GC] = fallingBlock.container[GR, GC];

                         }


                     }
                 }

                 Invalidate();
                 Thread.Sleep(500);
                 fallingBlock.gameBoardRef[0] += 1;

             }




         }
         */

        /*public bool containerContained(int gRow, int gCol) //TODO: FINISH THIS METHOD TO CHECK IF THE CURRENT INDEXS ARE IN THE CONTAINER ARRAY
        {

            int topRRow = fallingBlock.gameBoardRef[0] - 3;
            Point gameBoardPoint = new Point(gRow, gCol);
            Point curContainPoint;
            for(int i = topRRow; i< topRRow + 3; i ++)
            {
                for(int j = fallingBlock.gameBoardRef[1]; j < fallingBlock.gameBoardRef[1] +3;  )


            }


        }
        */


        public void manageFallingBlock()
        {


            //fallingBlock.container = fallingBlock.getRandomShape();
            fallingBlock = new TetrisShape();
            tmrMoveBlock.Enabled = true;
            
            //tmrMoveBlock.Tick += tmrMoveBlock_Tick;


            



        }
        
       

       

        public int getOffset(int offSet)
        {
            return (offSet * 40) + topOffset;

        }

        public bool moveDown()
        {
            Point p;
            for (int i = 0; i < fallingBlock.squarePoints.Count; i++)
            {
                p = fallingBlock.squarePoints[i];
                if (p.Y + 1 > 17 || mainGameBoard[p.Y + 1, p.X ] != 0)
                {
                    return false;

                }

            }
            return true;


        


        }



        private void tmrMoveBlock_Tick(object sender, EventArgs e)
        {

            if(moveDown())
            {
                for (int i = 0; i < fallingBlock.squarePoints.Count; i++)
                {
                    Point p = fallingBlock.squarePoints[i];//
                    fallingBlock.squarePoints[i] = new Point(p.X, p.Y + 1);
                    
                }

                Invalidate();
            }

            else
            {
                tmrMoveBlock.Stop();
                Point pr;
                for (int j = 0; j < fallingBlock.squarePoints.Count; j++)
                {
                    pr = fallingBlock.squarePoints[j];
                    mainGameBoard[pr.Y, pr.X] = fallingBlock.shapeColor;

                }

                manageFallingBlock();

            }
            //manageFallingBlock();

            /*  for (int i = 0; i < fallingBlock.squarePoints.Count; i++)
              {



                  Point p = fallingBlock.squarePoints[i];//
                  fallingBlock.squarePoints[i] = new Point(p.X, p.Y + 1);
                  if (p.Y + 2 > 17 || mainGameBoard[p.Y + 2,p.X] != 0 )
                  {

                      tmrMoveBlock.Stop();
                      Point pr;
                      for(int j = 0; j < fallingBlock.squarePoints.Count; j++)
                      {
                          pr = fallingBlock.squarePoints[j];
                          mainGameBoard[pr.Y, pr.X] = 1;

                      }
                      manageFallingBlock();
                      break;

                      //tmrMoveBlock.Stop();

                       //manageFallingBlock();


                  }




              }
              Invalidate();*/
        }

        public bool canMoveLeft()
        {
            Point p;
            
                
                for(int i = 0; i <  fallingBlock.squarePoints.Count; i++)
                {
                    p = fallingBlock.squarePoints[i];
                    if(p.X - 1 < 0 ||mainGameBoard[p.Y, p.X - 1] != 0)
                    {
                        return false;

                    }
                    
                }
                return true;

            
            


        }

        public void moveLeft()
        {
            for(int i = 0; i < fallingBlock.squarePoints.Count; i ++ )
            {
                Point p = fallingBlock.squarePoints[i];
                fallingBlock.squarePoints[i] = new Point(p.X - 1, p.Y);



            }
            Invalidate();


        }


        public bool canMoveRight()
        {
            Point p;


            for (int i = 0; i < fallingBlock.squarePoints.Count; i++)
            {
                p = fallingBlock.squarePoints[i];
                if (p.X + 1 > 9 || mainGameBoard[p.Y, p.X + 1] != 0)
                {
                    return false;

                }

            }
            return true;


        }

        public void moveRight()
        {
            for (int i = 0; i < fallingBlock.squarePoints.Count; i++)
            {
                Point p = fallingBlock.squarePoints[i];
                fallingBlock.squarePoints[i] = new Point(p.X + 1, p.Y);



            }
            Invalidate();


        }


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if(keyData == Keys.Left)
            {
                if(canMoveLeft())
                {
                    moveLeft();
                }
            }

            if( keyData == Keys.Right)
            {
                if(canMoveRight())
                {
                    moveRight();

                }

            }


            return base.ProcessCmdKey(ref msg, keyData);
        }
    }





}
