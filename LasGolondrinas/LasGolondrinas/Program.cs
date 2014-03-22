using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LasGolondrinas
{
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
            this.Energia = this.Energia - unosKm * 2;
            Console.WriteLine("Energia: {0}", this.Energia);
        }
        public void come(int comida)
        {
            this.Energia += comida * 3;
            Console.WriteLine("Energia: {0}", this.Energia);

        }
        public String nombre(){
            return this.Nombre;
        }

        public void nombre(String unNombre){
            this.Nombre = unNombre;
            Console.WriteLine("Ahora mi nombre es "+this.Nombre);
        }
    }
    class workspace
    {
        static void Main(string[] args)
        {
            Golondrina Pepita = new Golondrina("Cristian");
            Console.WriteLine(Pepita.nombre());
            Console.WriteLine("Energia: "+ Pepita.Energia);
            Console.WriteLine("Hambre: "+ Pepita.Hambre);
            Pepita.come(34);
            Pepita.vola(15);
        }
    }
}
