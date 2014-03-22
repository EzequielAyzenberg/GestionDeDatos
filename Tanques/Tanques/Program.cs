using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tanques
{
    class Tanque
    {
        //Atributos .3.
        public string Nombre;
        public List<Arma> Armas;
        public int Blindaje;
        public bool Vivo;

        //Método de inicialización :3
        public Tanque(string nombre, int blindaje)
        {
            this.Nombre = nombre;
            this.Blindaje = blindaje;
            this.Armas = new List<Arma>();
            this.Vivo = true;
        }

        //Métodos!!! o.O
        public void info() {
            Console.WriteLine("Nombre: " + this.Nombre);
            Console.WriteLine("Blindaje: " + this.Blindaje);
            Console.WriteLine("Vivo? " + this.Vivo);
            Console.WriteLine("Nivel: " + this.nivel());
            foreach (Arma chumbo in this.Armas)
            {
                Console.WriteLine("* Arma: "+chumbo.Nombre+" -Danio: "+chumbo.Danio+" -Usos: "+chumbo.Usos);
            }
        }

        public void armasContra(Tanque enemigo)
        {
            Console.WriteLine("Peleando contra: " + enemigo.Nombre);
            foreach (Arma chumbo in this.Armas)
            {
                Console.WriteLine("* Arma: " + chumbo.Nombre + " -SuperDanio: " + chumbo.superDanio(enemigo,this));
            }
        }

        public int nivel()
        {
            int sumatoria = this.Armas.Select(arma => arma.Danio).Sum();
            return (int)sumatoria / 10;
        }

        public void bajaTuBlindaje(int danio, Tanque enemigo)
        {
            if (danio < this.Blindaje) this.Blindaje -= danio;
            else
            {
                this.Blindaje = 0;
                this.Vivo = false;
                enemigo.recibirArmas(this.Armas);
                this.Armas = new List<Arma>();
            }
        }

        public void recibirArmas(List<Arma> unasArmas)
        {
            Console.WriteLine("");
            Console.WriteLine("Concatenando...");
            foreach (Arma chumbo in this.Armas)
            {
                Console.WriteLine("** Arma: " + chumbo.Nombre);
            }
            Console.WriteLine("Con...");
            foreach (Arma chumbo in unasArmas)
            {
                Console.WriteLine("** Arma: " + chumbo.Nombre);
            }
            Console.WriteLine("");        
            this.Armas = this.Armas.Concat(unasArmas).ToList();
        }

        public int cantidadArmas()
        {
            return this.Armas.Count;
        }

        public List<Arma> armasDisponibles()
        {
            //Filter flasheado e.e
            return this.Armas.Where(arma => arma.Usos > 0).ToList();
        }

        public Arma armaMasDanina(List<Arma> listaArmas, Tanque enemigo)
        {
            Arma masDanina = listaArmas.OrderByDescending(arma => arma.superDanio(enemigo,this)).First();
            Console.WriteLine("Nombre: " + masDanina.Nombre);
            Console.WriteLine("Danio: " + masDanina.Danio);
            Console.WriteLine("SuperDanio: " + masDanina.superDanio(enemigo, this));
            return masDanina;
        }

        public void atacaA(Tanque enemigo)
        {
            if( !this.Vivo || !enemigo.Vivo ) 
            {
                Console.WriteLine("Alguno esta muerto. Imposible atacar");
                return;
            }

            List<Arma> armasDisp = armasDisponibles();
            Arma superArma = this.elegiTuArma(armasDisp, enemigo);
            if (superArma == null) return;
            this.atacaCon(superArma,enemigo);
            superArma.bajaTuUso();
            return;

        }

        public void atacaCon(Arma arma, Tanque enemigo)
        {
            arma.atacaA(enemigo, this);
        }

        public Arma elegiTuArma(List<Arma> unasArmas, Tanque enemigo)
        {
            List<Arma> armasDisp = this.armasDisponibles();
            if (armasDisp.Count() == 0) {
                Console.WriteLine("No hay armas disponibles!!");
                return null;
            }
            List<Arma> armasAsesinas = this.armasAsesinas(armasDisp,enemigo);
            if (armasAsesinas.Count() == 0)
            {
                Console.WriteLine("Se eligió el arma mas Danina");
                return this.armaMasDanina(armasDisp, enemigo);
            }
            else
            {
                Console.WriteLine("Se eligió el arma mas efectiva");
                return this.armaMasEfectiva(armasAsesinas, enemigo);
            }
        }

        public List<Arma> armasAsesinas(List<Arma> unasArmas, Tanque enemigo)
        {
            //Es un filter ¬¬ menos expresivo no podia ser el C#
            return unasArmas.Where(arma => arma.superDanio(enemigo,this) >= enemigo.Blindaje).ToList();
        }

        public Arma armaMasEfectiva(List<Arma> unasArmas, Tanque enemigo)
        {
            //Selecciona el arma cuya diferencia entre el blindaje y el superdanio sea minima
            return unasArmas.OrderByDescending(arma => Math.Abs(enemigo.Blindaje - arma.superDanio(enemigo, this))).Last();
        }

    }

    class Arma
    {
        public delegate bool FuncionCondicional(Tanque x, Tanque y);

        //Atributos .3.
        public string Nombre;
        public int Danio;
        public int PlusDanio;
        public int Usos;
        public FuncionCondicional Condicion;

        //Método de inicialización :3
        public Arma(string nombre, int danio, int plus, int usos)
        {
            this.Nombre = nombre;
            this.Danio = danio;
            this.PlusDanio = plus;
            this.Usos = usos;
        }
        public void condicion(FuncionCondicional condicion){
            this.Condicion = condicion;
        }


        //Métodos!!! o.O
        public void atacaA(Tanque enemigo, Tanque duenio)
        {
            enemigo.bajaTuBlindaje(this.superDanio(enemigo,duenio),duenio);
        }
        public int superDanio(Tanque x, Tanque y) 
        {
            int danio = this.Danio;
            if (this.Condicion(x, y)) danio += this.PlusDanio;
            return danio;
        }
        public void bajaTuUso(){
            this.Usos -= 1;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Arma shotgun = new Arma("Shotgun",25,5,80);
            shotgun.condicion( (x,y) => x.Nombre == "hola" );

            Arma uzi942 = new Arma("Uzi-942", 23, 54, 50);
            uzi942.condicion((x, y) => y.Nombre == "WR44");

            Arma chuck = new Arma("Chuck Norris", 3000000, 5, 1);
            chuck.condicion((x, y) => true);
            
            Tanque wr44 = new Tanque("WR44", 90);
            
            Tanque megatron = new Tanque("Megatron", 78);
            
            wr44.Armas.Add(shotgun);
            wr44.Armas.Add(uzi942);
            megatron.Armas.Add(chuck);
            wr44.info();
            Console.WriteLine("");
            wr44.armasContra(megatron);
            Console.WriteLine("");
            wr44.atacaA(megatron);
            wr44.info();
            Console.WriteLine("");
            megatron.info();
            //wr44.atacaA(megatron);
        }
    }
}
