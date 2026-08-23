/*
 * Created by SharpDevelop.
 * User: Familia
 * Date: 20/10/2024
 * Time: 00:51
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections;
using System.Collections.Generic;
using intento;

namespace intento
{
	class Program
	{
		public static void Main(string[] args)
		{
			Libro unLibro;
			Socio unSocio;
			Socio_lector unSocioLector;
			string codi;
			int num_doc;
			int opcion;
			int contador = 0;
			int eleccion;
			unLibro = new Libro();
			unSocio = new Socio();
			unSocioLector = new Socio_lector();
			do
			{
				
			      	Console.WriteLine("                      MENU DE OPCIONES"+
                    "\n +___________________________________________________________+" +  				                  
				    "\n |1- Agregar un libro a la biblioteca                        |" +				    
				    "\n |2- Eliminar libro de la biblioteca                         |" +             
				    "\n |3- Dar de alta un socio/ socio lector                      |"+
				    "\n |4- Dar de baja un socio/ socio lector                      |"+
				    "\n |5- Prestar un libro                                        |"+
				    "\n |6- Devolver un libro                                       |"+
				    "\n |7- lista de Libros prestados/Libros de la biblioteca/Socios|"+
				    "\n |0 - para salir del programa                                |"+
				    "\n +___________________________________________________________+");
				
				Console.WriteLine("");
				Console.WriteLine("elige una opcion:");
				opcion = Convert.ToInt32(Console.ReadLine());
				switch(opcion)
				{
						
						
					case 1:
							unLibro.AgregarLibro();
							Console.WriteLine("has agregado un libro");
							break;
					case 2:
							Console.WriteLine("ingresa el codigo del libro que deseas eliminar:");
							codi = Console.ReadLine();
							foreach(Libro l in unLibro.get_colLibros())
							{
								if(l.get_codigo() == codi)
								{
									unLibro.EliminarLibro(l);
									Console.WriteLine("has eliminado el libro correctamente");
									break;
								}
							}
							
							break;
					case 3:
							Console.WriteLine("vas a crear un [1]socio normal/[2]socio lector de sala:");
							eleccion = int.Parse(Console.ReadLine());
							if(eleccion == 1)
							{
								unSocio.AgregarSocio();
								break;
							}
							if(eleccion == 2)
							{
								unSocioLector.AgregarSocioLector();
								break;
							}
							else
							{
								Console.WriteLine("elige bien la proxima vez");
							}
							break;
					case 4:
							Console.WriteLine("ingresa el documendo del socio que daras de baja");
							num_doc= int.Parse(Console.ReadLine());
							foreach(Socio s in unSocio.get_colSocios())
							{
								if(s.get_dni() == num_doc)
								{
									unSocio.EliminarSocio(s);
									Console.WriteLine("lograste dar de baja a un socio correctamente");
									break;
									
								}
							}
							break;
					case 5:
										try{
										Console.WriteLine("elige una opcion [1]socio normal/[2]socio lector de sala:");
										eleccion = int.Parse(Console.ReadLine());
										Console.WriteLine("numero de documento: ");
										num_doc = Convert.ToInt32(Console.ReadLine());
										Console.WriteLine("codigo del libro que deseas prestar: ");
										codi = Console.ReadLine();
											foreach(Libro u in unLibro.get_colLibros())
											{
												foreach(Socio so in unSocio.get_colSocios())
												{	
											
													if(eleccion == 1 && num_doc == so.get_dni() && codi == u.get_codigo() && u.get_estado() == "Disponible")
													{
														do
														{
															Console.WriteLine("¿cuantos dias prestaras el libro(el limite es 15 dias)?:");
															contador = Convert.ToInt32(Console.ReadLine());
														}
														while(contador > 15 || contador < 1);
														
														u.set_FechaPrestamo(DateTime.Today);
														u.set_FechaDevolucion(u.get_FechaPrestamo(),contador);
														u.set_dnilibro(num_doc);
														u.set_estado("Prestado");
														Console.WriteLine("se ha prestado un libro a un socio normal",u.get_FechaDevolucion());
													}
												}
												foreach(Socio_lector lec in unSocioLector.get_colSociosLectores())
												{
													if(eleccion == 2 && num_doc == lec.get_dni() && codi == u.get_codigo() && u.get_estado() == "Disponible")
													{
														u.set_dnilibro(num_doc);
														u.set_estado("Prestado");
														Console.WriteLine("se ha prestado un libro a un socio lector");
													}
												}
											}

											
											
												}catch(Exception error)
												{
													Console.WriteLine(error);
												}
							break;
					case 6:
							Console.WriteLine("ingrese el codigo del libro que devolveras");
							unLibro.DevolverLibro();

							break;
					case 7:
						   	int opcionli;
							Console.WriteLine("lista de libros prestados/ de la biblioteca/ socios");
							Console.WriteLine("---------------------------------------------------");

							
					do
					 {
						Console.WriteLine("\n1 -lista de libros prestados" +
								              "\n2 -lista de libros en la biblioteca" +
								              "\n3 - lista de socios normales"+
								              "\n4 - lista de socios lectores de sala"+
								              "\n0 - salir"+
								              "\n-----------------------------------");
						opcionli = Convert.ToInt32(Console.ReadLine());
						switch(opcionli)
						{	
							case 1:
								Console.WriteLine("elegiste ver la lista de libros prestados");
								foreach(Libro x in unLibro.get_colPrestamos())
								{
									Console.WriteLine(string.Format("titulo: {0}, autor: {1}, editorial: {2}, codigo: {3}, estado: {4}, dni: {5} ,fecha de prestamo: {6} ,fecha de devolucion: {7}", x.get_titulo(), x.get_autor(), x.get_editorial(),x.get_codigo(),x.get_estado(),x.get_dnilibri(),x.get_FechaPrestamo().ToString("dd/MM/yyyy"),x.get_FechaDevolucion().ToString("dd/MM/yyyy")));
								}
								break;
							case 2:
								Console.WriteLine("elegiste ver la lista de libros de la biblioteca");
								foreach(Libro v in unLibro.get_colLibros())
								{
									//Console.WriteLine(unLibro.cantproductos());
									Console.WriteLine(string.Format("titulo: {0}, autor: {1}, editorial: {2}, codigo: {3}, estado: {4}, dni: {5} ,fecha de prestamo: {6} ,fecha de devolucion: {7}", v.get_titulo(), v.get_autor(), v.get_editorial(),v.get_codigo(),v.get_estado(),v.get_dnilibri(),v.get_FechaPrestamo().ToString("dd/MM/yyyy"),v.get_FechaDevolucion().ToString("dd/MM/yyyy")));
								}	
								continue;

							case 3:
								Console.WriteLine("elegiste ver la lista de socios");
								foreach(Socio s in unSocio.get_colSocios())
								{
									Console.WriteLine(string.Format("nombre: {0}, apellido: {1}, dni: {2}, numero telefonico: {3}, direccion: {4}", s.get_nombre(), s.get_apellido(), s.get_dni(), s.get_telefono(), s.get_direccion()));
								}
								break;
							case 4:
								Console.WriteLine("elegiste ver la lista de socios lectores de sala:");
								foreach(Socio_lector l in unSocioLector.get_colSociosLectores())
								{
									Console.WriteLine(string.Format("nombre: {0}, apellido: {1}, dni: {2}", l.get_nombre(),l.get_apellido(),l.get_dni()));
									
								}
								break;
						    case 0:
								Console.WriteLine("*************************************");
								break;
						    
							default:
								Console.WriteLine("elige una opcion valida");
								break;
							
						}
					}
					
					while(opcionli != 0);
					
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
	}
	class Biblioteca
	{	
		private ArrayList colLibros;
		private ArrayList colPrestamos;
		private ArrayList colSocios;
		private ArrayList colSociosLectores;
		
		public Biblioteca()
		{
			this.colLibros = new ArrayList();
			this.colPrestamos = new ArrayList();
			this.colSocios = new ArrayList();
			this.colSociosLectores = new ArrayList();
		}
		
		public ArrayList get_colLibros()
		{
			return colLibros;
		}
		
		public void set_colPrestamos(ArrayList colLoans)
		{
			colLoans = colPrestamos;
		}
		
		public ArrayList get_colPrestamos()
		{
			return colPrestamos;
		}
		
		public void set_colSocios(ArrayList colPartners)
		{
			colPartners = colSocios;
		}
		
		public ArrayList get_colSocios()
		{
			return colSocios;
		}
		
		public void set_colSociosLectores(ArrayList colPartnerReaders)
		{
			colPartnerReaders = colSociosLectores;
		}
		
		public ArrayList get_colSociosLectores()
		{
			return colSociosLectores;
		}
		
		
		public void AgregarLibro()
		{
			Libro unLibro = new Libro();
			ArrayList lisLibros = new ArrayList();
			Console.WriteLine("titulo del libro:");
			unLibro.set_titulo(Console.ReadLine());
			Console.WriteLine("autor del libro:");
			unLibro.set_autor(Console.ReadLine());
			Console.WriteLine("editorial:");
			unLibro.set_editorial(Console.ReadLine());
			unLibro.set_codigo((Convert.ToString(unLibro.get_titulo()[0])+Convert.ToString(unLibro.get_autor()[0])+Convert.ToString(unLibro.get_autor().Length)+Convert.ToString(unLibro.get_editorial()[0])+Convert.ToString(colLibros.Count+1+unLibro.get_titulo().Length)));

			colLibros.Add(unLibro);
		}
		
		public void PrestarLibro(Libro lib)
		{
			colPrestamos.Add(lib);
		}
		
		public void AgregarSocio()
		{
			Socio unSocio = new Socio();
			Console.WriteLine("nombre del socio:");
			unSocio.set_nombre(Console.ReadLine());
			Console.WriteLine("apellido del socio:");
			unSocio.set_apellido(Console.ReadLine());
			Console.WriteLine("dni del socio:");
			unSocio.set_dni(Convert.ToInt32(Console.ReadLine()));
			Console.WriteLine("numero telefonico del socio:");
			unSocio.set_telefono(Convert.ToInt32(Console.ReadLine()));
			Console.WriteLine("direccion del socio:");
			unSocio.set_direcion(Console.ReadLine());
			colSocios.Add(unSocio);
		}
		
		public void AgregarSocioLector()
		{
			Socio_lector unSocioLector = new Socio_lector();
			Console.WriteLine("nombre del socio lector de sala:");
			unSocioLector.set_nombre(Console.ReadLine());
			Console.WriteLine("apellido del socio lector de sala:");
			unSocioLector.set_apellido(Console.ReadLine());
			Console.WriteLine("dni del socio lector de sala:");
			unSocioLector.set_dni(Convert.ToInt32(Console.ReadLine()));
			Console.WriteLine("diste de alta a un socio lector de sala");
			colSociosLectores.Add(unSocioLector);
		}
		
		public void EliminarLibro(Libro lib)
		{
			colLibros.Remove(lib);
		}
		
		public void EliminarSocio(Socio soc)
		{
			colSocios.Remove(soc);
		}
		
		public void EliminarSocioLector(Socio_lector soclec)
		{
			colSociosLectores.Remove(soclec);
		}
		

		public void DevolverLibro()
		{
			Libro unLibro;
			unLibro = new Libro();
			string cod;
			cod = Console.ReadLine();
			foreach(Libro p in colLibros)
				{
				if(p.get_codigo() == cod && p.get_estado() == "Prestado")
				{
				p.set_dnilibro(0);
				p.set_estado("Disponible");
									
				Console.WriteLine("devolviste un libro a la biblioteca");
				}
				else
				{
				Console.WriteLine("incorrecto, el libro {0} ya se encuentra en la biblioteca", p.get_titulo());
				break;
				}
			}
		}
		
	}
	
	class Libro: Biblioteca
		{	
			private string Autor;
			private string Titulo;
			private string Editorial;
			private string Codigo = "";
			private string Estado;
			private DateTime Fecha_prestamo;
			private DateTime Fecha_devolucion;
			private int Dnilibro;
			
			public Libro()
			{
				
				this.Autor = "indefinido";
				this.Titulo = "indefinido";
				this.Editorial = "indefinido";
				this.Codigo = "indefinido";
				this.Estado = "Disponible";
				this.Fecha_prestamo = new DateTime(0001,01,01);
				this.Fecha_devolucion = new DateTime(0001,01,01);
				this.Dnilibro = 0;
			}
			
			public void set_autor(string author)
			{
				Autor = author;
			}
			
			public string get_autor()
			{
				return Autor;
			}
			
			public void set_titulo(string title)
			{
				Titulo = title;
			}
			
			public string get_titulo()
			{
				return Titulo;
			}
			
			public void set_editorial(string publisher)
			{
				Editorial = publisher;
			}
			
			public string get_editorial()
			{
				return Editorial;
			}
			
			public void set_estado(string status)
			{
				Estado = status;
			}
			
			public string get_estado()
			{
				return Estado;
			}
			
			public void set_codigo(string codi)
			{
				Codigo = codi;
			}
			
			public string get_codigo()
			{
				return Codigo;
			}
			
			public void set_dnilibro(int idbook)
			{
				Dnilibro = idbook;
			}
			
			public int get_dnilibri()
			{
				return Dnilibro;
			}	
			
			public void set_FechaPrestamo(DateTime loandate)
			{
				Fecha_prestamo = loandate;
			}
			
			public DateTime get_FechaPrestamo()
			{
				return Fecha_prestamo;
			}
			
			public void set_FechaDevolucion(DateTime devolutiodate,int dias)
			{
				devolutiodate = Fecha_devolucion;
				Fecha_devolucion = Fecha_prestamo.AddDays(dias);
			}
			
			public DateTime get_FechaDevolucion()
			{
				return Fecha_devolucion;;
			}
			
		}
	class Socio: Biblioteca
	{
		private string Nombre;
		private string Apellido;
		private int Dni;
		private int Num_telefono;
		private string Direccion;
		
		public Socio()
		{
			this.Nombre = "indefinido";
			this.Apellido = "indefinido";
			this.Dni = -1;
			this.Num_telefono = -1;
			this.Direccion = "indefinido";
				
		}
		
		public void set_nombre(string name)
		{
			Nombre = name;
		}
		
		public string get_nombre()
		{
			return Nombre;
		}
		
		public void set_apellido(string lastname)
		{
			Apellido = lastname;
		}
		
		public string get_apellido()
		{
			return Apellido;
		}
		
		public void set_dni(int docid)
		{
			Dni = docid;
		}
		
		public int get_dni()
		{
			return Dni;
		}
		
		public void set_telefono(int phone_num)
		{
			Num_telefono = phone_num;
		}
		
		public int get_telefono()
		{
			return Num_telefono;
		}
		
		public void set_direcion(string adress)
		{
			Direccion = adress;
		}
		
		public string get_direccion()
		{
			return Direccion;
		}
		
		public bool cs( Socio soc)
		{
			int i = 0;
			bool esigual = true;
			
			foreach(Libro li in get_colLibros())
			{
				if(li.get_dnilibri().Equals(soc.get_dni()))
				{
					i++;
				}
			}
			
			if(i > 0)
			{
				esigual = false;
			}
			
			return esigual;
		}

	}
	
	class Socio_lector: Socio
	{
		public bool csl( Socio_lector soclec)
		{
			int y = 0;
			bool soniguales = true;
			
			foreach(Libro li in get_colLibros())
			{
				if(li.get_dnilibri().Equals(soclec.get_dni()))
				{
					y++;
				}
			}
			
			if(y > 15)
			{
				soniguales = false;
			}
			
			return soniguales;
		}
	}
}