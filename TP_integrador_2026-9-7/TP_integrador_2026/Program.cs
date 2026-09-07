/*
 * Created by SharpDevelop.
 * User: Familia
 * Date: 28/8/2026
 * Time: 16:53
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace TP_integrador_2026
{
	class Program
	{
		//static Libro ELlibro = new Libro();
		static Biblioteca LAbiblioteca = new Biblioteca();
		//static Libro UNlibro = null;
		public static void Main(string[] args)
		{	
			
			int opcion;
			do
			{
				Console.WriteLine("                      MENU DE OPCIONES"+
                    "\n +___________________________________________________________+"+  				                  
				    "\n |1- Agregar un libro a la biblioteca                        |"+				    
				    "\n |2- Eliminar libro de la biblioteca                         |"+             
				    "\n |3- Agregar socio						                    |"+
				    "\n |4- Eliminar socio	                                        |"+
				    "\n |5- Prestar libro											|"+
				    "\n |6-	Regresar libro											|"+
				    "\n |7- Consultar stock					 						|"+
				    "\n |8- Ver listas						 						|"+
				    "\n |8- Cambiar stock					 						|"+
				    "\n |0 - para salir del programa                                |"+
				    "\n +___________________________________________________________+");
				
				Console.WriteLine("");
				Console.Write("ingresa una opcion: ");
				opcion = int.Parse(Console.ReadLine());
				switch(opcion)
				{
					case 1:
						Console.WriteLine("agregar libro ");
						LAbiblioteca.AgregarLibro();
						break;
						
					case 2:
						Console.WriteLine("eliminar libro ");
						LAbiblioteca.EliminarLibro();
						break;
						
					case 3:
						Console.WriteLine("agregar socio ");
						LAbiblioteca.AgregarSocio();
						break;

					case 4:
						Console.WriteLine("eliminar socio ");
						LAbiblioteca.EliminarSocio();
						break;

					case 5:
						Console.WriteLine("prestar libro ");
						LAbiblioteca.PrestarLibro();
						break;

					case 6:
						Console.WriteLine("regresar libro ");
						LAbiblioteca.RegresarLibro();
						break;

					case 7:
						Console.WriteLine("consultar stock general ");
						LAbiblioteca.ConsultarStockGeneral();
						break;
					case 8:
						LAbiblioteca.ConsultarLibros();
						Console.WriteLine("----------------------------------------------------------");
						LAbiblioteca.ConsultarSocio();
						Console.WriteLine("----------------------------------------------------------");
						LAbiblioteca.ConsultarSocioPremium();
						Console.WriteLine("----------------------------------------------------------");
						LAbiblioteca.ConsultarPrestamos();
						break;
						
					case 9:
						LAbiblioteca.ActualizarStock();
						break;
						
					case 0:
						Console.WriteLine("saliendo del programa");
						break;

					default:
						Console.WriteLine("elige una opcion valida");
						break;
				}
				
			}while(opcion != 0);
			
			
			Console.ReadKey(true);
		}
		
		//luego voy a mover las clases a otras secciones
		
		class Biblioteca
		{
			private List<Libro> ListaLibros;
			private List<Socio> ListaSocios;
			private List<Socio_Premium> ListaSociosPremium;
			private List<Prestamo> ListaPrestamos;
			
			public Biblioteca()
			{
				ListaLibros = new List<Libro>();
				ListaSocios = new List<Socio>();
				ListaSociosPremium = new List<Socio_Premium>();
				ListaPrestamos = new List<Prestamo>();
			}
			
			public void AgregarLibro()
			{
				Console.Write("Ingresa el titulo del libro: ");
				string ELtitulo = Console.ReadLine();
				Console.Write("Ingresa el nombre y apellido del autor: ");
				string ELautor = Console.ReadLine();
				Console.Write("Ingresa la editorial: ");
				string LAeditorial = Console.ReadLine();
				Console.Write("Ingresa el stock: ");
				int ELstock = int.Parse(Console.ReadLine());
				
				Libro UNlibro = new Libro(ELtitulo,ELautor,LAeditorial,ELstock);
				ListaLibros.Add(UNlibro);
			}
			
			public void EliminarLibro()
			{
				Console.Write("Ingresa el codigo del libro que deseas eliminar: ");
				string cod = Console.ReadLine();
				Libro UNbook = null;
				
				foreach(var lib in ListaLibros)
				{
					if(lib.Codigo == cod)
					{
						UNbook = lib;
						break;
					}
				}
				
				if (UNbook == null)
				{
					Console.WriteLine("No existe ese libro");
					return;
				}
				
				for (int i = ListaPrestamos.Count - 1; i >= 0; i--)
				{
					if(ListaPrestamos[i].Codigo_Libro == cod)
					{
						ListaPrestamos.RemoveAt(i);
					}
				}
				
				ListaLibros.Remove(UNbook);
				Console.WriteLine("El libro se ha eliminado");
			}
			
			public void AgregarSocio()
			{
				Console.Write("Ingresa el nombre del socio: ");
				string ELnombre = Console.ReadLine();
				Console.Write("Ingresa el apellido del socio: ");
				string ELapellido = Console.ReadLine();
				Console.Write("Ingresa el numero de DNI del socio: ");
				int elDNI = int.Parse(Console.ReadLine());
				Console.Write("Ingresa el numero de telefono del socio: ");
				int ELnumeroTelefonico = int.Parse(Console.ReadLine());
				Console.Write("Ingresa la direccion del socio: ");
				string LAdireccion = Console.ReadLine();
				Console.Write("Quieres que sea un socio premium) (SI / NO): ");
				string Eleccion = Console.ReadLine();
				
				if(Eleccion.ToUpper() == "NO")
				{
					Socio UNsocio = new Socio(ELnombre,ELapellido,elDNI,ELnumeroTelefonico,LAdireccion);
					ListaSocios.Add(UNsocio);
				}
				
				if(Eleccion.ToUpper() == "SI")
				{
					Socio_Premium UNsocioPremium = new Socio_Premium(ELnombre,ELapellido,elDNI,ELnumeroTelefonico,LAdireccion);
					ListaSociosPremium.Add(UNsocioPremium);
				}
			}
			
			/*public void AgregarSocioPremium()
			{
				Console.Write("Ingresa el nombre del socio premium: ");
				string ELnombre_ = Console.ReadLine();
				Console.Write("Ingresa el apellido del socio premium: ");
				string ELapellido_ = Console.ReadLine();
				Console.Write("Ingresa el numero de DNI del socio premium: ");
				int elDNI_ = int.Parse(Console.ReadLine());
				Console.Write("Ingresa el numero de telefono del socio premium: ");
				int ELnumeroTelefonico_ = int.Parse(Console.ReadLine());
				Console.Write("Ingresa la direccion del socio premium: ");
				string LAdireccion_ = Console.ReadLine();
				
				Socio_Premium UNsocioPremium = new Socio_Premium(ELnombre_,ELapellido_,elDNI_,ELnumeroTelefonico_,LAdireccion_);
				ListaSociosPremium.Add(UNsocioPremium);
			}*/
			
			public void EliminarSocio()
			{
				Console.Write("Ingresa el numero de DNI del socio que deseas eliminar: ");
				int codi = int.Parse(Console.ReadLine());
				Socio UNsoc = null;
				
				foreach (var soc in ListaSocios)
				{
					if(soc.DNI == codi)
					{
						UNsoc = soc;
						break;
					}
				}
				
				if (UNsoc == null)
				{
					foreach(var vip in ListaSociosPremium)
					{
						if(vip.DNI == codi)
						{
							UNsoc = vip;
							break;
						}
					}
				}
				
				if(UNsoc == null)
				{
					Console.WriteLine("No existe ese socio");
					return;
				}
				
				if(ListaSocios.Contains(UNsoc))
				{
					ListaSocios.Remove(UNsoc);
					Console.WriteLine("El socio se ha eliminado");
				}
				
				else
				{
					ListaSociosPremium.Remove((Socio_Premium)UNsoc);
					Console.WriteLine("El socio premium se ha eliminado");
				}
				
			}
			
			public void PrestarLibro()
			{
				Libro UNlibro;
				Console.Write("Ingresa el codigo del libro que vas a prestar: ");
				string cod = Console.ReadLine();
				Console.Write("Ingresa el DNI del socio al que le vas a prestar: ");
				int docu = int.Parse(Console.ReadLine());
				Console.Write("Ingresa la cantidad de dias que vas a prestar este libro: ");
				int dias = int.Parse(Console.ReadLine());
				
				Socio UNsocio = null;
				
				foreach(var s in ListaSocios)
				{
					if(s.DNI == docu)
					{
						if(dias <= 20 || dias > 0)
						{
							UNsocio = s;
						}
						else
						{
							Console.WriteLine("El limite para un socio es de 20 dias");
							return;
						}
						break;
					}
				}
				
				if(UNsocio == null)
				{
					foreach(var sp in ListaSociosPremium)
					{
						if(sp.DNI == docu)
						{
							if(dias <= 30 || dias > 0)
							{
								UNsocio = sp;
							}
							else
							{
								Console.WriteLine("El limite para un socio premium es de 30 dias");
								return;
							}
							break;
						}
					}
				}
				
				if(UNsocio == null)
				{
					Console.WriteLine("Ese socio no existe");
					return;
				}
				
				UNlibro = null;
				
				foreach(var book in ListaLibros)
				{
					if(book.Codigo == cod)
					{ 
						UNlibro = book;
						break;
					}
				}
				
				if(UNlibro == null)
				{
					Console.WriteLine("Ese libro no existe");
					return;
				}
				
				if(UNlibro.Stock <= 0)
				{
					Console.WriteLine("Ese libro no tiene stock disponible");
					return;
				}
				
				if(UNlibro != null && UNsocio != null && UNlibro.Stock > 0)
				{
					Prestamo UNprestamo;
					DateTime Fprestamo = DateTime.Today;
					DateTime Fdevolucion = DateTime.Today.AddDays(dias);
					UNlibro.Stock -=1;
					UNprestamo = new Prestamo(UNlibro, UNsocio, Fprestamo, Fdevolucion);
					ListaPrestamos.Add(UNprestamo);
				}
			}
			
			public void RegresarLibro()
			{
				Console.Write("Ingresa el codigo del libro que vas a regresar: ");
				string cod = Console.ReadLine();
				Console.Write("Ingresa el DNI del socio que lo posee: ");
				int docu = int.Parse(Console.ReadLine());
				
				Socio SOC = null;
				
				foreach(var s in ListaSocios)
				{
					if(s.DNI == docu)
					{
						SOC = s;
						break;
					}
				}
				
				if(SOC == null)
				{
					foreach(var sp in ListaSociosPremium)
					{
						if(sp.DNI == docu)
						{
							SOC = sp;
							break;
						}
					}
				}
				
				if(SOC == null)
				{
					Console.WriteLine("Ese socio no existe");
					return;
				}
				
				Prestamo UNprestamo = null;
				
				foreach(var p in ListaPrestamos)
				{
					if(p.Codigo_Libro == cod && p.Nom_Soc == SOC.Nombre  && p.AP_Soc == SOC.Apellido)
					{
						UNprestamo = p;
						break;
					}
				}
				
				if(UNprestamo == null)
				{
					Console.WriteLine("Ese prestamo no existe");
					return;
				}
				
				Libro UNlibro = null;
				
				if(UNlibro == null)
				{
					foreach(var book in ListaLibros)
					{
						if(book.Codigo == cod)
						{
							UNlibro = book; 
						}
					}
				}
				
				if(UNlibro == null)
				{
					Console.WriteLine("Ese libro no existe");
					return;
				}
				
				if(UNprestamo != null)
				{
					UNlibro.Stock += 1;
					ListaPrestamos.Remove(UNprestamo);
					Console.WriteLine("El libro fue devuelto");
				}
			}
			
			public void ConsultarLibros()
			{
				foreach(var lib in ListaLibros)
				{
					Console.WriteLine("Titulo: {0}/ Autor: {1}/ Editorial {2}/ Codigo: {3}/ Stock: {4}", lib.Titulo, lib.Autor, lib.Editorial, lib.Codigo, lib.Stock);
				}
			}
			
			public void ConsultarSocio()
			{
				foreach(var soc in ListaSocios)
				{
					Console.WriteLine("Nombre: {0}/ Apellido: {1}/ DNI: {2}/ Num de telefono: {3}/ Direccion {4}",soc.Nombre, soc.Apellido, soc.DNI, soc.Num_telefono, soc.Direccion);
				}
			}
			
			public void ConsultarSocioPremium()
			{
				foreach(var vip in ListaSociosPremium)
				{
					Console.WriteLine("Nombre: {0}/ Apellido: {1}/ DNI: {2}/ Num de telefono: {3}/ Direccion {4}",vip.Nombre, vip.Apellido, vip.DNI, vip.Num_telefono, vip.Direccion);
				}
			}
			
			public void ConsultarPrestamos()
			{
				foreach(var pres in ListaPrestamos)
				{
					Console.WriteLine("ID: {0}/ Nombre del libro: {1}/ Codigo del libro: {2}/ Nombre del socio: {3}/ Apellido socio: {4}/ Fecha del prestamo: {5}/ Fecha de devolucion: {6}",pres.ID_prestamo,pres.Nombre_libro,pres.Codigo_Libro,pres.Nom_Soc,pres.AP_Soc,pres.Fecha_prestamo,pres.Fecha_devolucion);
				}
			}
			
			public void ActualizarStock()
			{
				Libro UNbook = null;
				Console.Write("Ingresa el codigo del libro que deseas modificar el stock: ");
				string cod = Console.ReadLine();
				
				foreach(var book in ListaLibros)
				{
					if(book.Codigo == cod)
					{
						UNbook = book;
					}
				}
				
				Console.WriteLine("Stock anterior = {0}", UNbook.Stock);
				Console.Write("Ingresa el nuevo stock: ");
				int Nstock = int.Parse(Console.ReadLine());
				UNbook.Stock = Nstock;
				Console.WriteLine("El stock nuevo es = {0}", UNbook.Stock);
			}
			
			public void ConsultarStockGeneral()
			{
				int StockPrestamos = ListaPrestamos.Count;
				int StockLibreria = 0;
				
				foreach(var lib in ListaLibros)
				{
					StockLibreria += lib.Stock;
				}
				int StockGeneral = StockLibreria + StockPrestamos;
				
				Console.WriteLine("El stock general es: {0}", StockGeneral);
			}
		}
		
		class Socio
		{
			private string _Nombre;
			private string _Apellido;
			private int _DNI;
			private int _Num_telefono;
			private string _Direccion;
			
			public string Nombre
			{
				get {return _Nombre;}
				set	{_Nombre = value;}
			}
			
			public string Apellido
			{
				get {return _Apellido;}
				set	{_Apellido = value;}
			}
			
			public int DNI
			{
				get {return _DNI;}
				set	{_DNI = value;}
			}
			
			public int Num_telefono
			{
				get {return _Num_telefono;}
				set	{_Num_telefono = value;}
			}
			
			public string Direccion
			{
				get {return _Direccion;}
				set	{_Direccion = value;}
			}
			
			public Socio(string name, string lastname, int ID, int telnum, string address)
			{
				_Nombre = name;
				_Apellido = lastname;
				_DNI = ID;
				_Num_telefono = telnum;
				_Direccion = address;
			}
			
			/*public void ConsultarNombre()
			{
				Console.WriteLine(_Nombre);
			}
			
			public void ConsultarApellido()
			{
				Console.WriteLine(_Apellido);
			}
			
			public void ConsultarDNI()
			{
				Console.WriteLine(_DNI);
			}
			
			public void ConsultarTelefono()
			{
				Console.WriteLine(_Num_telefono);
			}
			
			public void ConsultarDireccion()
			{
				Console.WriteLine(_Direccion);
			}*/
		}
		
		class Socio_Premium: Socio
		{
			public Socio_Premium(string name, string lastname, int ID, int telnum, string address)
				: base(name,lastname,ID,telnum,address)
			{
			}
			
		}
		
		class Libro
		{
			private string _Titulo;
			private string _Autor;
			private string _Editorial;
			private string _Codigo;
			private int _Stock;

			public string Titulo
			{
				get {return _Titulo;}
				set	{_Titulo = value;}
			}
			
			public string Autor
			{
				get {return _Autor;}
				set {_Autor = value;}
			}
			
			public string Editorial
			{
				get {return _Editorial;}
				set {_Editorial = value;}
			}
			
			public string Codigo
			{
				get {return _Codigo;}
				set {_Codigo = value;}
			}
			
			public int Stock
			{
				get {return _Stock;}
				set {_Stock = value;}
			}
			
			public Libro(string Title, string Writer, string Publisher,int Num)
			{
				_Titulo = Title;
				_Autor = Writer;
				_Editorial = Publisher;
				Random rand = new Random();
				int numID = 0;
				
				for (int i = 0; i < 5; i++)
				{
					numID++;
				}
				
				_Codigo = char.ToUpper(Title[0]) + char.ToUpper(Autor[0])+ ((rand.Next(1, 100) * numID).ToString()) + char.ToUpper(Publisher[0]);
				_Stock = Num;
			}
			
			/*public void ActualizarStock()//ver si es posible cambiar de lugar este metodo
			{
				Console.WriteLine("Stock anterior = {0}", _Stock);
				Console.Write("Ingresa el nuevo stock: ");
				Nstock = int.Parse(Console.ReadLine());
				_Stock = Nstock;
				Console.WriteLine("El stock nuevo es = {0}", _Stock);
			}*/
			
			/*public void ConsultarStock()
			{
				Console.WriteLine(_Stock);
			}
			
			public void ConsultarEditorial()
			{
				Console.WriteLine(_Editorial);
			}
			
			public void ConsultarCodigo()
			{
				Console.WriteLine(_Codigo);
			}
			
			public void ConsultarTitulo()
			{
				Console.WriteLine(_Titulo);
			}
			
			public void ConsultarAutor()
			{
				Console.WriteLine(_Autor);
			}*/
		}
		
		class Prestamo
		{
			private static int ContarID = 1;
			private int _ID_prestamo;
			private string _Nombre_libro;
			private string _Codigo_libro;
			private string _Nom_Soc;
			private string _AP_Soc;
			private DateTime _Fecha_Prestamo;
			private DateTime _Fecha_Devolucion;
			
			/*public List<Libro> Lista_libros
			{
				get {return _Lista_libros;}
				set {_Lista_libros = value;}
			}*/
			
			public int ID_prestamo
			{
				get {return _ID_prestamo;}
				set {_ID_prestamo = value;}
			}
			
			public string Nombre_libro
			{
				get {return _Nombre_libro;}
				set {_Nombre_libro = value;}
			}
			
			public string Codigo_Libro
			{
				get {return _Codigo_libro;}
				set {_Codigo_libro = value;}
			}
			
			public string Nom_Soc
			{
				get {return _Nom_Soc;}
				set {_Nom_Soc = value;}
			}
			
			public string AP_Soc
			{
				get {return _AP_Soc;}
				set {_AP_Soc = value;}
			}
			
			public DateTime Fecha_prestamo
			{
				get {return _Fecha_Prestamo;}
				set {_Fecha_Prestamo = value;}
			}
			
			public DateTime Fecha_devolucion
			{
				get {return _Fecha_Devolucion;}
				set {_Fecha_Devolucion = value;}
			}
			
			public Prestamo(Libro Book, Socio UNsoc ,DateTime DateLoan, DateTime DateReturn)
			{
				_ID_prestamo = ContarID++;
				_Nombre_libro = Book.Titulo;
				_Codigo_libro = Book.Codigo;
				_Nom_Soc = UNsoc.Nombre;
				_AP_Soc = UNsoc.Apellido;
				_Fecha_Prestamo = DateLoan;
				_Fecha_Devolucion = DateReturn;
			}
			
			/*public void ConsultarListaLibros()
			{
				Console.WriteLine("{0} {1}", );//poner solo el nombre y el codigo del libro
			}*/
			
			/*public void ConsultarID()
			{
				Console.WriteLine(_ID_prestamo);
			}
			
			public void ConsultarNombre()
			{
				Console.WriteLine(_Nombre_libro);
			}
			
			public void ConsultarCodigo()
			{
				Console.WriteLine(_Codigo_libro);
			}
			
			public void ConsultarNombreSOC()
			{
				Console.WriteLine(_Nom_Soc);
			}
			
			public void ConsultarApellidoSOC()
			{
				Console.WriteLine(_AP_Soc);
			}
			
			public void ConsultarFechaPrestamo()
			{
				Console.WriteLine(_Fecha_Prestamo);
			}
			
			public void ConsultarFechaDevolucion()
			{
				Console.WriteLine(_Fecha_Devolucion);
			}*/
		}
		
	}
}