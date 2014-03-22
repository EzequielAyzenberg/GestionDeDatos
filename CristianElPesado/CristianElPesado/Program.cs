using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

 class Golondrina 
    {
        public Golondrina(string nombre)
        {
            this.Energia = 0;
            this.Hambre = 0;
            this.Nombre = nombre;
        }
        public int Energia;
        public int Hambre;
        public string Nombre;
        
        public void vola(int unosKm)
        {
            this.Energia = this.Energia - unosKm*2;
            Console.WriteLine("Energia: {0}", this.Energia);
        }
        public void come(int comida)
        {
            this.Energia += comida * 3;
            Console.WriteLine("Energia: {0}", this.Energia);

        }
        public void decimeNombre()
        {
            Console.WriteLine("Mi nombre es {0}", this.Nombre);
        }
        
    }
 class WorkSpace
 {
     static void Main()
     {
         Golondrina Pepita = new Golondrina("Cristian");
         Pepita.decimeNombre();
         Console.WriteLine("Energia: {0}", Pepita.Energia);
         Console.WriteLine("Hambre: {0}", Pepita.Hambre);
         Pepita.come(34);
         Pepita.vola(15);
     }
 }