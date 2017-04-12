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

    //TODO: make menu to start stop and pause game, fix row dropping, dont let square rotate, randomize start positions for figures
    public partial class MainForm : Form
    {
        public bool gameRunning = false;
        public int level = 1;
        public int score = 0;
        public int highScore = 0;
        public int curLevelRowsClear = 0;
        public const int topOffset = 50;
        public const int leftOffset = 50;
        public const int widthOfSquare = 40; //40px
        public int[,] mainGameBoard = new int[18, 10]; //each square is 40px by 40px
        TetrisShape fallingBlock = new TetrisShape();
        Rectangle paintBoard = new Rectangle(49, 49, 400, 720);
        
        public MainForm()
        {
            InitializeComponent();
            lblScore.Text = "Score " + score.ToString();
            lblLevel.Text = "Level " + level.ToString();

            for (int i = 0; i < 18; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    mainGameBoard[i, j] = 0;

                }

            }
           
        }

       

        public void checkWin()
        {
            List<int> rowsToBeCleared = new List<int>(); 
            int consecutiveRows = 0;
            for(int i = 0; i<10; i++)
            {
                if(mainGameBoard[0,i] != 0)
                {
                    gameRunning = false;
                    
                    MessageBox.Show("loser");
                    newToolStripMenuItem.Enabled = true;
                    return;
                }


            }

            for(int j = 17; j > -1; j--)
            {
                int tmpColsFull = 0;
                for(int k = 9; k > -1; k--)
                {
                   if(mainGameBoard[j,k] != 0)
                    {
                        
                        tmpColsFull++;
                    }


                }
                if(tmpColsFull == 10)
                {
                    rowsToBeCleared.Add(j);
                    consecutiveRows++;
                    curLevelRowsClear++; 
                    for(int c = 0; c< 10; c++)
                    {
                        
                        mainGameBoard[j, c] = 0;



                    }
                    clearRowsv2(j);
                    j++;
                    



                    



                }


            }


            
            
            if(consecutiveRows > 0)
            {
                score += getScore(consecutiveRows);
                if(score > highScore)
                {
                    highScore = score;
                }



            }
            if(curLevelRowsClear > 9)
            {
                clearLevel();

            }
            lblScore.Text = "Score " + score.ToString();
            Invalidate();
           

        }

        public void clearRowsv2(int rowStart)
        {

            for(int j = rowStart; j > 0; j--)
            {
                for(int i = 9; i > - 1; i--)
                {
                    mainGameBoard[j, i] = mainGameBoard[j - 1, i];

                }

            }
            Invalidate();

        }

   

        public int getScore(int consecutiveRows)
        {

            if (consecutiveRows > 1)
            {
                return (((100 * level) * consecutiveRows) + ((50 * level) * consecutiveRows)) - 50;
            }
           
            else return 100 * level;
            
            
            


        }



        public void clearLevel()
        {

            
            level++;
            MessageBox.Show("You advanced to level " + level.ToString());
            tmrMoveBlock.Interval = (int)(tmrMoveBlock.Interval * .75);
            curLevelRowsClear = 0;
            lblLevel.Text = "Level " + level.ToString();
            return;


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
            score = 0;
            curLevelRowsClear = 0;
            level = 1;
            gameRunning = true;

            
            return;

        }

        protected override void OnPaint(PaintEventArgs e) //loop through entire gameboard and look at each number to decide color to paint
        {
            //have a pen for drawing the falling block then a pen and brush for drawing the 
            //gameboard  set the color every iteration fallingBlock.getColor(),
            Graphics graphic = e.Graphics;
            Pen p = new Pen(Color.Black, 5);//pen for falling block
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
                    
                    if(fallingBlock.squarePoints.Contains(checkPoint) )
                    {
                        Rectangle r;
                        graphic.DrawRectangle(p, getOffset(j), getOffset(i), 35, 35);
                        r = new Rectangle(getOffset(j), getOffset(i), 35, 35);
                        graphic.FillRectangle(brosh, r);
                        
                    }

                    if(mainGameBoard[i, j] != 0)
                    {
                        
                        Rectangle boardRect = new Rectangle(getOffset(j), getOffset(i), 35,35);
                        boardPen = new Pen(Color.Black, 5);
                        graphic.DrawRectangle(boardPen, getOffset(j), getOffset(i), 35, 35);
                        boardBrosh = new SolidBrush(colorLookUp(mainGameBoard[i, j]));
                        graphic.FillRectangle(boardBrosh, boardRect);

                    }


                }


              }

            




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
                    return Color.Orange;

                case 7:
                    return Color.HotPink;

            }
            return Color.Black;


        }


       


        public void manageFallingBlock()
        {


            
            if(!gameRunning)
            {
                return;
            }
            fallingBlock = new TetrisShape();
            tmrMoveBlock.Enabled = true;
            
            


            



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
                fallingBlock.topLeft = new Point(fallingBlock.topLeft.X, fallingBlock.topLeft.Y + 1);
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
                checkWin();
                manageFallingBlock();

            }
           
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
            fallingBlock.topLeft = new Point(fallingBlock.topLeft.X-1, fallingBlock.topLeft.Y );
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
            fallingBlock.topLeft = new Point(fallingBlock.topLeft.X + 1, fallingBlock.topLeft.Y );
            Invalidate();


        }


        public bool canRotateCCW(int degree)
        {
           
        

            int xOrig = fallingBlock.squarePoints[2].X;
            int yOrig = fallingBlock.squarePoints[2].Y;
            double rad = (Math.PI * degree) / 180;
            int[,] rotationMatrix = new int[2, 2];
            rotationMatrix[0, 0] = (int)Math.Cos(rad);
            rotationMatrix[1, 1] = (int)Math.Cos(rad);
            rotationMatrix[0, 1] = -(int)Math.Sin(rad);
            rotationMatrix[1, 0] = (int)Math.Sin(rad);
            for (int i = 0; i < fallingBlock.squarePoints.Count; i++)
            {
                int[,] pointVector = new int[2, 1];
                pointVector[0, 0] = fallingBlock.squarePoints[i].X - xOrig;
                pointVector[1, 0] = fallingBlock.squarePoints[i].Y - yOrig;
                int tempX = ((rotationMatrix[0, 0] * pointVector[0, 0]) + (rotationMatrix[0, 1] * pointVector[1, 0])) + xOrig;
                int tempy = ((rotationMatrix[1, 0] * pointVector[0, 0]) + (rotationMatrix[1, 1] * pointVector[1, 0])) + yOrig;
                if(tempX > 9 || tempX < 0 || tempy > 17 || tempy < 0 || mainGameBoard[tempy,tempX] != 0)
                {
                    return false;
                }


            }
            return true;


        }

    

        public void rotateCCW()
        {

            fallingBlock.setRotatedShapePoints(90);

         
            Invalidate();

        }

        public void dropDown()
        {
            while(moveDown())
            {
                for (int i = 0; i < fallingBlock.squarePoints.Count; i++)
                {
                    Point p = fallingBlock.squarePoints[i];//
                    fallingBlock.squarePoints[i] = new Point(p.X, p.Y + 1);

                }
                fallingBlock.topLeft = new Point(fallingBlock.topLeft.X, fallingBlock.topLeft.Y + 1);
                
            }

            Invalidate();


        }




        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if(!gameRunning)
            {
                return false; 
            }
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

            if(keyData == Keys.Up)
            {
                if(canRotateCCW(90))
                {
                    rotateCCW();


                }


            }
            if (keyData == Keys.Down)
            {
                if (canRotateCCW(-90))
                {
                    rotateCCW();


                }


            }


            if(keyData == Keys.Space)
            {
                dropDown();


            }
            if(keyData == Keys.Home)
            {
                clearLevel();

            }


            return base.ProcessCmdKey(ref msg, keyData);
        }

       

        private void pauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            goToolStripMenuItem.Enabled = true;
            pauseToolStripMenuItem.Enabled = false;
            tmrMoveBlock.Enabled = false;
            gameRunning = false;
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Tetris game by Alex Price v1.9 March 23 2017. \nCurrent high score for this session is " + highScore.ToString(),"About", MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void goToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gameRunning = true;
            
            goToolStripMenuItem.Enabled = false;
            pauseToolStripMenuItem.Enabled = true;
            tmrMoveBlock.Enabled = true;
            return;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {

            gameRunning = true;
            initializeGame();
            manageFallingBlock();
            newToolStripMenuItem.Enabled = false;
            pauseToolStripMenuItem.Enabled = true;
            tmrMoveBlock.Enabled = true;
            return;

        }
    }





}
