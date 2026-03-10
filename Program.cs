using System;
using System.Collections.Generic;

namespace SistemaExamenes
{

    public interface IPrototipo
    {
        IPrototipo Clonar();
        string MostrarInfo();
    }

    public abstract class ExamenBase : IPrototipo
    {
        protected readonly string _claveMateria;
        protected readonly string _nombreAsignatura;
        protected readonly int _creditos;
        protected readonly string _carrera;
        protected readonly int _semestre;
        protected const string TIPO_EXAMEN = "Ordinario";

        protected string _docente;
        protected string _salon;
        protected string _grupo;
        protected string _fecha;
        protected int _numPreguntas;
        protected string _idExamen;

        protected List<string> _instrucciones;
        protected List<string> _reactivos;

        protected ExamenBase(
            string claveMateria,
            string nombreAsignatura,
            string docente,
            string salon,
            string carrera,
            int semestre,
            int creditos,
            int numPreguntas)
        {
            _claveMateria = claveMateria;
            _nombreAsignatura = nombreAsignatura;
            _docente = docente;
            _salon = salon;
            _carrera = carrera;
            _semestre = semestre;
            _creditos = creditos;
            _numPreguntas = numPreguntas;

            _grupo = "A";
            _fecha = DateTime.Today.ToString("yyyy-MM-dd");
            _idExamen = GenerarId();
            _instrucciones = new List<string>();
            _reactivos = new List<string>();
        }

        public string ClaveMateria => _claveMateria;
        public string NombreAsignatura => _nombreAsignatura;
        public int Creditos => _creditos;
        public string Carrera => _carrera;
        public int Semestre => _semestre;
        public string IdExamen => _idExamen;
        public string TipoExamen => TIPO_EXAMEN;

        public string Docente { get => _docente; set => _docente = value; }
        public string Salon { get => _salon; set => _salon = value; }
        public string Grupo { get => _grupo; set => _grupo = value; }
        public string Fecha { get => _fecha; set => _fecha = value; }
        public int NumPreguntas { get => _numPreguntas; set => _numPreguntas = value; }

        public List<string> Instrucciones => _instrucciones;
        public List<string> Reactivos => _reactivos;

        public void AgregarInstruccion(string i) => _instrucciones.Add(i);
        public void AgregarReactivo(string r) => _reactivos.Add(r);

        public virtual IPrototipo Clonar()
        {
            ExamenBase clon = (ExamenBase)MemberwiseClone();
            clon._instrucciones = new List<string>(_instrucciones);
            clon._reactivos = new List<string>(_reactivos);
            clon._idExamen = GenerarId();
            return clon;
        }

        public virtual string MostrarInfo()
        {
            string sep = new string('─', 60);
            var sb = new System.Text.StringBuilder();
            sb.AppendLine(sep);
            sb.AppendLine($"  ID Examen    : {_idExamen}");
            sb.AppendLine($"  Materia      : {_nombreAsignatura}");
            sb.AppendLine($"  Clave        : {_claveMateria}");
            sb.AppendLine($"  Tipo         : {TIPO_EXAMEN}");
            sb.AppendLine($"  Docente      : {_docente}");
            sb.AppendLine($"  Salón        : {_salon}");
            sb.AppendLine($"  Grupo        : {_grupo}");
            sb.AppendLine($"  Fecha        : {_fecha}");
            sb.AppendLine($"  Semestre     : {_semestre}°");
            sb.AppendLine($"  Carrera      : {_carrera}");
            sb.AppendLine($"  Créditos     : {_creditos}");
            sb.AppendLine($"  # Preguntas  : {_numPreguntas}");

            if (_instrucciones.Count > 0)
            {
                sb.AppendLine("  Instrucciones:");
                foreach (var inst in _instrucciones)
                    sb.AppendLine($"    • {inst}");
            }
            if (_reactivos.Count > 0)
            {
                sb.AppendLine("  Reactivos:");
                for (int i = 0; i < _reactivos.Count; i++)
                    sb.AppendLine($"    P{i + 1}: {_reactivos[i]}");
            }
            sb.AppendLine(sep);
            return sb.ToString();
        }

        public override string ToString() =>
            $"[{GetType().Name} | {_claveMateria} | Grupo:{_grupo} | Docente:{_docente}]";

        private static string GenerarId() =>
            Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper();
    }

    public class ExamenPatronesDiseno : ExamenBase
    {
        private List<string> _patronesEvaluados;
        public List<string> PatronesEvaluados => _patronesEvaluados;
        public void AgregarPatron(string p) => _patronesEvaluados.Add(p);

        public ExamenPatronesDiseno(string docente, string salon, string grupo = "A")
            : base("SCC-1023", "Patrones de Diseño",
                   docente, salon, "Ing. en Sistemas Computacionales",
                   semestre: 7, creditos: 4, numPreguntas: 30)
        {
            _grupo = grupo;
            _patronesEvaluados = new List<string>();
        }

        public override IPrototipo Clonar()
        {
            var clon = (ExamenPatronesDiseno)base.Clonar();
            clon._patronesEvaluados = new List<string>(_patronesEvaluados);
            return clon;
        }

        public override string MostrarInfo()
        {
            var sb = new System.Text.StringBuilder(base.MostrarInfo());
            int pos = sb.ToString().LastIndexOf(new string('─', 60));
            sb.Insert(pos, $"  Patrones     : {string.Join(", ", _patronesEvaluados)}\n");
            return sb.ToString();
        }
    }

    public class ExamenCalculoDiferencial : ExamenBase
    {
        private List<string> _temas;
        public List<string> Temas => _temas;
        public void AgregarTema(string t) => _temas.Add(t);

        public ExamenCalculoDiferencial(string docente, string salon, string grupo = "A")
            : base("ACF-0902", "Cálculo Diferencial",
                   docente, salon, "Ing. en Sistemas Computacionales",
                   semestre: 1, creditos: 5, numPreguntas: 20)
        {
            _grupo = grupo;
            _temas = new List<string>();
        }

        public override IPrototipo Clonar()
        {
            var clon = (ExamenCalculoDiferencial)base.Clonar();
            clon._temas = new List<string>(_temas);
            return clon;
        }

        public override string MostrarInfo()
        {
            var sb = new System.Text.StringBuilder(base.MostrarInfo());
            int pos = sb.ToString().LastIndexOf(new string('─', 60));
            sb.Insert(pos, $"  Temas        : {string.Join(", ", _temas)}\n");
            return sb.ToString();
        }
    }

    public class ExamenBasesDatos : ExamenBase
    {
        public string MotorBD { get; set; }

        public ExamenBasesDatos(string docente, string salon, string grupo = "A")
            : base("SCD-1003", "Bases de Datos",
                   docente, salon, "Ing. en Sistemas Computacionales",
                   semestre: 4, creditos: 4, numPreguntas: 25)
        {
            _grupo = grupo;
            MotorBD = "MySQL";
        }

        public override string MostrarInfo()
        {
            var sb = new System.Text.StringBuilder(base.MostrarInfo());
            int pos = sb.ToString().LastIndexOf(new string('─', 60));
            sb.Insert(pos, $"  Motor BD     : {MotorBD}\n");
            return sb.ToString();
        }
    }

    public class ExamenFisica : ExamenBase
    {
        public int Unidad { get; set; }

        public ExamenFisica(string docente, string salon, string grupo = "A")
            : base("ACF-0904", "Física",
                   docente, salon, "Ing. en Sistemas Computacionales",
                   semestre: 2, creditos: 4, numPreguntas: 15)
        {
            _grupo = grupo;
            Unidad = 1;
        }

        public override string MostrarInfo()
        {
            var sb = new System.Text.StringBuilder(base.MostrarInfo());
            int pos = sb.ToString().LastIndexOf(new string('─', 60));
            sb.Insert(pos, $"  Unidad       : {Unidad}\n");
            return sb.ToString();
        }
    }

    public class ExamenPOO : ExamenBase
    {
        public string Lenguaje { get; set; }

        public ExamenPOO(string docente, string salon, string grupo = "A")
            : base("SCC-1003", "Programación Orientada a Objetos",
                   docente, salon, "Ing. en Sistemas Computacionales",
                   semestre: 3, creditos: 5, numPreguntas: 28)
        {
            _grupo = grupo;
            Lenguaje = "C#";
        }

        public override string MostrarInfo()
        {
            var sb = new System.Text.StringBuilder(base.MostrarInfo());
            int pos = sb.ToString().LastIndexOf(new string('─', 60));
            sb.Insert(pos, $"  Lenguaje     : {Lenguaje}\n");
            return sb.ToString();
        }
    }

    public class ExamenIngles : ExamenBase
    {
        public string NivelCEFR { get; set; }

        public ExamenIngles(string docente, string salon, string grupo = "A")
            : base("AEF-1031", "Inglés VI",
                   docente, salon, "Ing. en Sistemas Computacionales",
                   semestre: 6, creditos: 3, numPreguntas: 40)
        {
            _grupo = grupo;
            NivelCEFR = "B1";
        }

        public override string MostrarInfo()
        {
            var sb = new System.Text.StringBuilder(base.MostrarInfo());
            int pos = sb.ToString().LastIndexOf(new string('─', 60));
            sb.Insert(pos, $"  Nivel CEFR   : {NivelCEFR}\n");
            return sb.ToString();
        }
    }

    public class ExamenRedes : ExamenBase
    {
        public string Modelo { get; set; }

        public ExamenRedes(string docente, string salon, string grupo = "A")
            : base("SCC-1043", "Redes de Computadoras",
                   docente, salon, "Ing. en Sistemas Computacionales",
                   semestre: 5, creditos: 4, numPreguntas: 30)
        {
            _grupo = grupo;
            Modelo = "OSI";
        }

        public override string MostrarInfo()
        {
            var sb = new System.Text.StringBuilder(base.MostrarInfo());
            int pos = sb.ToString().LastIndexOf(new string('─', 60));
            sb.Insert(pos, $"  Modelo       : {Modelo}\n");
            return sb.ToString();
        }
    }

    public class ExamenIA : ExamenBase
    {
        private List<string> _algoritmos;
        public List<string> Algoritmos => _algoritmos;
        public void AgregarAlgoritmo(string a) => _algoritmos.Add(a);

        public ExamenIA(string docente, string salon, string grupo = "A")
            : base("SCC-1063", "Inteligencia Artificial",
                   docente, salon, "Ing. en Sistemas Computacionales",
                   semestre: 8, creditos: 4, numPreguntas: 25)
        {
            _grupo = grupo;
            _algoritmos = new List<string>();
        }

        public override IPrototipo Clonar()
        {
            var clon = (ExamenIA)base.Clonar();
            clon._algoritmos = new List<string>(_algoritmos);
            return clon;
        }

        public override string MostrarInfo()
        {
            var sb = new System.Text.StringBuilder(base.MostrarInfo());
            int pos = sb.ToString().LastIndexOf(new string('─', 60));
            sb.Insert(pos, $"  Algoritmos   : {string.Join(", ", _algoritmos)}\n");
            return sb.ToString();
        }
    }

    public class RegistroPrototipos
    {
        private readonly Dictionary<string, ExamenBase> _prototipos
            = new Dictionary<string, ExamenBase>();

        public void Registrar(string clave, ExamenBase proto)
        {
            _prototipos[clave] = proto;
            Console.WriteLine(
                $"  ✅  [{clave,-20}]  {proto.NombreAsignatura,-38}  Docente: {proto.Docente}");
        }

        public ExamenBase ObtenerClon(string clave)
        {
            if (!_prototipos.ContainsKey(clave))
                throw new KeyNotFoundException($"Prototipo '{clave}' no existe.");
            return (ExamenBase)_prototipos[clave].Clonar();
        }

        public void Listar()
        {
            Console.WriteLine("\n  📚  PROTOTIPOS EN REGISTRO:");
            foreach (var p in _prototipos)
                Console.WriteLine($"       {p.Key,-22}  →  {p.Value}");
            Console.WriteLine();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Titulo("PATRÓN PROTOTIPO — Sistema de Exámenes Tecnológico");

            Seccion("PASO 1 : Registrando prototipos base (plantillas)");

            var registro = new RegistroPrototipos();
            Console.WriteLine();

            var protoPD = new ExamenPatronesDiseno("Ing. Alejandro Fuentes Herrera", "C-201");
            protoPD.AgregarPatron("Singleton");
            protoPD.AgregarPatron("Factory Method");
            protoPD.AgregarPatron("Prototype");
            protoPD.AgregarReactivo("Describa el patrón Singleton y cuándo aplicarlo.");
            protoPD.AgregarReactivo("Implemente Factory Method para vehículos en C#.");
            protoPD.AgregarReactivo("Explique la diferencia entre Prototype y Clone().");

            var protoCalc = new ExamenCalculoDiferencial("Ing. Lorena Castillo Mendoza", "A-105");
            protoCalc.AgregarTema("Límites y continuidad");
            protoCalc.AgregarTema("Derivadas — regla de la cadena");
            protoCalc.AgregarTema("Derivadas implícitas");
            protoCalc.AgregarReactivo("Calcule lim x→0  (sen x / x).");
            protoCalc.AgregarReactivo("Derive f(x) = x³·cos(2x) usando la regla del producto.");

            var protoBD = new ExamenBasesDatos("Ing. Citlali Ramos Espinoza", "B-310");
            protoBD.MotorBD = "PostgreSQL";
            protoBD.AgregarReactivo("Normalice la tabla Pedidos hasta 3FN.");
            protoBD.AgregarReactivo("Escriba un JOIN entre Clientes y Facturas.");

            var protoFis = new ExamenFisica("Ing. Lorena Castillo Mendoza", "A-208");
            protoFis.Unidad = 2;
            protoFis.AgregarReactivo("Calcule la aceleración en plano inclinado 30°.");
            protoFis.AgregarReactivo("Aplique Newton a un sistema de dos poleas.");

            var protoPOO = new ExamenPOO("Ing. Alejandro Fuentes Herrera", "C-202");
            protoPOO.Lenguaje = "C#";
            protoPOO.AgregarReactivo("Implemente herencia con figuras geométricas.");
            protoPOO.AgregarReactivo("Cree una interfaz IComparable y ordene una lista.");

            var protoIng = new ExamenIngles("Mtra. Xóchitl Pedraza Villanueva", "D-102");
            protoIng.NivelCEFR = "B2";
            protoIng.AgregarReactivo("Write a formal email to apply for a software internship.");
            protoIng.AgregarReactivo("Read the passage and answer the comprehension questions.");

            var protoRed = new ExamenRedes("Ing. Gerardo Mixtega Salinas", "B-201");
            protoRed.Modelo = "TCP/IP";
            protoRed.AgregarReactivo("Diseñe una subred para 50 hosts con CIDR.");
            protoRed.AgregarReactivo("Describa las capas TCP/IP y sus protocolos.");

            var protoIA = new ExamenIA("Dr. Cuauhtémoc Ibarra Pozos", "C-305");
            protoIA.AgregarAlgoritmo("A*");
            protoIA.AgregarAlgoritmo("Backtracking");
            protoIA.AgregarAlgoritmo("MinMax");
            protoIA.AgregarReactivo("Trace A* sobre el grafo proporcionado.");
            protoIA.AgregarReactivo("Implemente Backtracking para N-reinas.");

            registro.Registrar("PD_Fuentes", protoPD);
            registro.Registrar("CALC_Castillo", protoCalc);
            registro.Registrar("BD_Ramos", protoBD);
            registro.Registrar("FISICA_Castillo", protoFis);
            registro.Registrar("POO_Fuentes", protoPOO);
            registro.Registrar("INGLES_Pedraza", protoIng);
            registro.Registrar("REDES_Mixtega", protoRed);
            registro.Registrar("IA_Ibarra", protoIA);

            registro.Listar();

            Seccion("PASO 2 : Generando exámenes por grupo / docente");

            var examenes = new List<ExamenBase>();

            var pdA = registro.ObtenerClon("PD_Fuentes");
            pdA.Grupo = "A"; pdA.Salon = "C-201"; pdA.Fecha = "2024-11-05";
            examenes.Add(pdA);

            var pdB = registro.ObtenerClon("PD_Fuentes");
            pdB.Grupo = "B"; pdB.Salon = "C-202"; pdB.Fecha = "2024-11-06";
            examenes.Add(pdB);

            var pdC = registro.ObtenerClon("PD_Fuentes");
            pdC.Grupo = "C"; pdC.Salon = "C-203";
            pdC.Docente = "Ing. Roberto Ocampo Téllez";
            examenes.Add(pdC);

            var calcA = registro.ObtenerClon("CALC_Castillo");
            calcA.Grupo = "A"; calcA.Salon = "A-105";
            examenes.Add(calcA);

            var calcB = registro.ObtenerClon("CALC_Castillo");
            calcB.Grupo = "B"; calcB.Salon = "A-107";
            examenes.Add(calcB);

            var calcC = registro.ObtenerClon("CALC_Castillo");
            calcC.Grupo = "C"; calcC.Salon = "A-109";
            calcC.Docente = "Ing. Fermín Sandoval Quiroz";
            examenes.Add(calcC);

            var bdA = registro.ObtenerClon("BD_Ramos");
            bdA.Grupo = "A"; bdA.Salon = "B-310";
            examenes.Add(bdA);

            var bdB = registro.ObtenerClon("BD_Ramos");
            bdB.Grupo = "B"; bdB.Salon = "B-312";
            ((ExamenBasesDatos)bdB).MotorBD = "MySQL";
            examenes.Add(bdB);

            var fisA = registro.ObtenerClon("FISICA_Castillo");
            fisA.Grupo = "A"; fisA.Salon = "A-208";
            examenes.Add(fisA);

            var fisB = registro.ObtenerClon("FISICA_Castillo");
            fisB.Grupo = "B"; fisB.Salon = "A-210";
            fisB.Docente = "Ing. Tláloc Navarrete Huerta";
            ((ExamenFisica)fisB).Unidad = 3;
            examenes.Add(fisB);

            var pooA = registro.ObtenerClon("POO_Fuentes");
            pooA.Grupo = "A"; pooA.Salon = "C-202";
            examenes.Add(pooA);

            var pooB = registro.ObtenerClon("POO_Fuentes");
            pooB.Grupo = "B"; pooB.Salon = "C-203";
            examenes.Add(pooB);

            var ingA = registro.ObtenerClon("INGLES_Pedraza");
            ingA.Grupo = "A"; ingA.Salon = "D-102";
            examenes.Add(ingA);

            var ingB = registro.ObtenerClon("INGLES_Pedraza");
            ingB.Grupo = "B"; ingB.Salon = "D-104";
            ingB.Docente = "Mtra. Consuelo Bravo Leyva";
            examenes.Add(ingB);

            var redA = registro.ObtenerClon("REDES_Mixtega");
            redA.Grupo = "A"; redA.Salon = "B-201";
            examenes.Add(redA);

            var redB = registro.ObtenerClon("REDES_Mixtega");
            redB.Grupo = "B"; redB.Salon = "B-203";
            examenes.Add(redB);

            var iaA = registro.ObtenerClon("IA_Ibarra");
            iaA.Grupo = "A"; iaA.Salon = "C-305";
            examenes.Add(iaA);

            var iaN = registro.ObtenerClon("IA_Ibarra");
            iaN.Grupo = "N1"; iaN.Salon = "C-306";
            examenes.Add(iaN);

            var iaB = registro.ObtenerClon("IA_Ibarra");
            iaB.Grupo = "B"; iaB.Salon = "C-301";
            iaB.Docente = "Dr. Epifanio Montiel Zavala";
            examenes.Add(iaB);

            Console.WriteLine($"\n  Total exámenes generados por clonación: {examenes.Count}");

            Seccion($"PASO 3 : Detalle de los {examenes.Count} exámenes generados");

            foreach (var ex in examenes)
                Console.Write(ex.MostrarInfo());

            Seccion("PASO 4 : Verificación de independencia entre clones");

            var e1 = examenes[0];
            var e2 = examenes[1];
            var e3 = examenes[2];

            Console.WriteLine($"\n  Clon 1 → ID:{e1.IdExamen}  Grupo:{e1.Grupo}  Docente:{e1.Docente}");
            Console.WriteLine($"  Clon 2 → ID:{e2.IdExamen}  Grupo:{e2.Grupo}  Docente:{e2.Docente}");
            Console.WriteLine($"  Clon 3 → ID:{e3.IdExamen}  Grupo:{e3.Grupo}  Docente:{e3.Docente}");
            Console.WriteLine();
            Console.WriteLine($"  ¿Mismo objeto (1 y 2)?                    {ReferenceEquals(e1, e2),5}");
            Console.WriteLine($"  ¿IDs distintos?                           {e1.IdExamen != e2.IdExamen,5}   ✅");
            Console.WriteLine($"  ¿Misma clave materia? (dato protegido)    {e1.ClaveMateria == e3.ClaveMateria,5}   ← readonly ✅");
            Console.WriteLine($"  ¿Listas reactivos independientes?         {!ReferenceEquals(e1.Reactivos, e2.Reactivos),5}   ← deep copy ✅");
            Console.WriteLine($"  ¿TipoExamen es 'Ordinario' en todos?      {e1.TipoExamen == "Ordinario",5}   ← constante ✅");

            Seccion("PASO 5 : Resumen estadístico");

            var porMateria = new Dictionary<string, int>();
            var porDocente = new Dictionary<string, int>();
            var materiasPorDocente = new Dictionary<string, HashSet<string>>();

            foreach (var ex in examenes)
            {
                Contar(porMateria, ex.NombreAsignatura);
                Contar(porDocente, ex.Docente);

                if (!materiasPorDocente.ContainsKey(ex.Docente))
                    materiasPorDocente[ex.Docente] = new HashSet<string>();
                materiasPorDocente[ex.Docente].Add(ex.NombreAsignatura);
            }

            Console.WriteLine("\n  Grupos generados por materia:");
            foreach (var (k, v) in porMateria)
                Console.WriteLine($"    • {k,-42}  {v} grupo(s)");

            Console.WriteLine("\n  Grupos asignados por docente:");
            foreach (var (k, v) in porDocente)
                Console.WriteLine($"    • {k,-35}  {v} grupo(s)");

            Console.WriteLine("\n  Docentes que imparten MÁS DE UNA materia:");
            bool hayMulti = false;
            foreach (var (doc, mats) in materiasPorDocente)
            {
                if (mats.Count > 1)
                {
                    Console.WriteLine($"    ★  {doc}");
                    foreach (var m in mats)
                        Console.WriteLine($"         └─ {m}");
                    hayMulti = true;
                }
            }
            if (!hayMulti)
                Console.WriteLine("    (ninguno en este escenario)");

            Titulo("FIN DE LA DEMOSTRACIÓN");
            Console.WriteLine("\n  Presiona cualquier tecla para salir...");
            Console.ReadKey();
        }

        static void Titulo(string t)
        {
            string s = new string('═', 65);
            Console.WriteLine($"\n{s}\n   {t}\n{s}");
        }
        static void Seccion(string t)
        {
            string s = new string('─', 65);
            Console.WriteLine($"\n{s}\n  {t}\n{s}");
        }
        static void Contar(Dictionary<string, int> d, string k)
        {
            if (d.ContainsKey(k)) d[k]++; else d[k] = 1;
        }
    }
}