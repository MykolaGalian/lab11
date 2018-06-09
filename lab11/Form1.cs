using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace lab11
{
    public partial class Form1 : Form
    {
        private int _x; //начальные координаты подвижного обьекта
        private int _y;

        private int _dx; //шаг
        private int _dy;
        private int b_size = 45; // габаритный размер картинки "смайлик" в пикселях 
        private int r_size = 186; // габариты неподвижного обьекта
        private int r_left_up_point_x = 160;  // координаты неподвижного обьекта
        private int r_left_up_point_y = 180;
                     
        public Form1()
        {
            InitializeComponent();
            _x = 50; 
            _y = 150;
            _dx = 2; 
            _dy = 2;
        }
         
        private void ve_Tick(object sender, EventArgs e)
        {
            MoveSmile();     // ф-ия управления движения обьектом (отрисовка по таймеру, каждую 1 мс)      
        }
                           
        private void Load_O(object sender, EventArgs e)
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);  // буферизация для снижения мерцания изображения
        }
        
        private void Form1_Paint(object sender, PaintEventArgs e) //получаем ссылку на графический объект "e" как часть PaintEventArgs в событии Paint формы
        {
            Image smile_image = Image.FromFile("smile.jpg"); // создание картинки
            Image wall_image = Image.FromFile("wall7.jpg");

            e.Graphics.DrawImage(smile_image, new Rectangle(_x, _y, b_size, b_size)); //Рисует заданный объект Image в заданном месте, используя указанный размер. 
                                                                                      // создание прямоугольника для подвижного обьекта (_x, _y меняются)

            e.Graphics.DrawImage(wall_image, r_left_up_point_x-5, r_left_up_point_y-5);       // отрисовка картинки неподвижного обьекта
        }

        private void MoveSmile() {

            Rectangle wall_XL = new Rectangle(r_left_up_point_x - 7, r_left_up_point_y, 1, r_size);          // создание прямоугольника для неподвижного обьекта(лево) 
            Rectangle wall_XR = new Rectangle(r_left_up_point_x + r_size + 7, r_left_up_point_y, 1, r_size);    // создание прямоугольника для неподвижного обьекта(право) 
            Rectangle wall_YU = new Rectangle(r_left_up_point_x, r_left_up_point_y - 7, r_size, 1);          // создание прямоугольника для неподвижного обьекта(верх) 
            Rectangle wall_YB = new Rectangle(r_left_up_point_x, r_left_up_point_y + r_size + 7, r_size, 1);    // создание прямоугольника для неподвижного обьекта(низ) 
            
            Rectangle ball_image_ = new Rectangle(_x, _y, b_size, b_size);  // создание (копии) прямоугольника (не отрисовываем) для подвижного обьекта, для проверки пересечения

            if ( (_x < 0) || (_x > this.ClientSize.Width-45)  )  _dx = -_dx;                                 // отскок от левого и правого края формы
            else if (wall_XL.IntersectsWith(ball_image_) || wall_XR.IntersectsWith(ball_image_)) _dx = -_dx;  // отскок от левого и правого края неподвижного обьекта

            else if ((_y < 0) || (_y > this.ClientSize.Height-45) )  _dy = -_dy;                             //отскок от верхнего и нижнего края формы
            else if (wall_YU.IntersectsWith(ball_image_) || wall_YB.IntersectsWith(ball_image_)  ) _dy = -_dy; //отскок от верхнего и нижнего края неподвижного обьекта

            _x += _dx;  // приращение движения обьекта
            _y += _dy;
            
            Invalidate(); // стирание для последующей перерисовки
        }
    }
}
