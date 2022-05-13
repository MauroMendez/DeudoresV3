using Deudoresv3.Data.Sql;
using Npgsql;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Net;
using System.Threading;
using System.Globalization;

namespace Deudoresv3
{
    internal class Program
    {


        static bool mailSent = false;
        static int ADContador = 0, RDContador = 0, rdid = 0, convenio = 0, ccanio = 0, ccperiodo = 0, dbid = 0;
        static double beinscrip = 0.0, beparcialidades = 0.0;
        static string semestrenum = "", semestredesc = "", rdemailt = "", email = "", rdnoconceptos = "", bcid = "", bcdesc = "", tutor = "", aacgrupo = "", convfeinicio = "", convfefin = "", aluestatus = "", descconceptos = "";
        NpgsqlConnection Conexion;
        MailMessage emailEnvio, emailConvenio;


        static void Main(string[] args)
        {
            Program obj = new Program();
            obj.AlumnosAdeudo_FechaVencida(obj);

        }
        public void AlumnosAdeudo_FechaVencida(Program obj)
        {
            string cmdTXT;
            int contador = 0;
            NpgsqlDataReader Alumnos;
            NpgsqlCommand comando;
            Conexion = obj.ConexionPostgres();

            try
            {
                Conexion.Close();
                Console.WriteLine("Conexión abierta postgress");

                using (var _sql_context = new sqlContext())
                {
                    Rdeudores rdeudores = new Rdeudores();
                    rdeudores.Rdfecreg = DateTime.Now;
                    rdeudores.Rdtimereg = DateTime.Now;
                    rdeudores.Rdtimefin = DateTime.Now;

                    //_sql_context.Rdeudores.Add(rdeudores);
                    //_sql_context.SaveChanges();

                    string semestre = "";
                    string grupo = "";
                    List<ViewDeudores> deudoresList = (from VD in _sql_context.ViewDeudores select VD).ToList();


                    foreach (ViewDeudores vd in deudoresList)
                    {
                        if (vd.NivelNombre.Trim() == "BAC")
                            // AQui esta el metodo de la versión 2
                            GrupoBachiller((int)vd.AlId);
                        // Acá se pone el metodo de la versión 3 y se elimina el de ariba

                        else
                        {
                            semestre = vd.AlSemestre.ToString();
                            if (vd.NivelNombre.Trim() == "EDC" || vd.NivelNombre.Trim() == "DOC")
                            {
                                // se obtiene el grupo
                            }
                            else
                            {
                                // el grupo se queda en vacio
                                grupo = "";
                            }

                        }

                        // obtenemos la info de la beca de la versión 2
                        infoBeca(obj, vd.AlMatricula, vd.NivelNombre.Trim());
                        // obtenemos la info de la beca de la versión 3 cuando se migren las becas a la versión 3 se declara el metodo acá


                        // obtenemos el convenio de la versión 2 y enviamos correo 
                        obj.Convenio(obj, vd.AlMatricula);
                        // aqui va el metodo para convenios de la versión 3


                        Rdeudoresdiario rdeudoresdiario = new Rdeudoresdiario();
                        if (convenio == 1)
                        {
                            rdeudoresdiario.Rdaluid = (int)vd.AlId;
                            rdeudoresdiario.Rdpedindiente = (decimal)vd.Pendiente;
                            rdeudoresdiario.Rddivision = vd.NivelId;
                            rdeudoresdiario.Div = vd.NivelNombre;
                            rdeudoresdiario.Rdapaterno = vd.AlApp;
                            rdeudoresdiario.Rdamaterno = vd.AlApm;
                            rdeudoresdiario.Rdnombre = vd.AlNombre;
                            rdeudoresdiario.Rdemail = vd.AlCorreoInst;
                            rdeudoresdiario.Rdcarid = vd.Idcarrera;
                            rdeudoresdiario.Carrera = vd.CarreraClave;
                            rdeudoresdiario.Rdnocontrol = vd.AlMatricula;
                            rdeudoresdiario.Rdgrado = "";
                            rdeudoresdiario.Rdgrupo = "";
                            rdeudoresdiario.RdemailT = vd.GaCorreoAlternativo;
                            rdeudoresdiario.RdnoConceptos = vd.NoCon;
                            rdeudoresdiario.Rdbcid = 2; 
                            //rdeudoresdiario.Rdbcid = Convert.ToInt32(bcid);
                            rdeudoresdiario.Beca = bcdesc;
                            rdeudoresdiario.Rdbeinscrip = Convert.ToDecimal(beinscrip);
                            rdeudoresdiario.Rdbeparcialidades = Convert.ToDecimal(beparcialidades);
                            rdeudoresdiario.Rdbedesc = bcdesc;
                            rdeudoresdiario.Rdtutor = tutor;
                            rdeudoresdiario.Rdconvenio = Convert.ToDateTime(convfeinicio.Trim());
                            rdeudoresdiario.Rdconvinicio = Convert.ToDateTime(convfeinicio.Trim());
                            rdeudoresdiario.Rdconvfin = Convert.ToDateTime(convfefin.Trim());
                            rdeudoresdiario.Rdtelefono = vd.GaTelefonoTutor;
                            rdeudoresdiario.Rdestatus = vd.AlStatusActual;
                            rdeudoresdiario.Estatus = vd.SlDescripcion;
                            rdeudoresdiario.Rdconceptosdesc = descconceptos.Trim();



                        }
                        else
                        {
                            rdeudoresdiario.Rdaluid = (int)vd.AlId;
                            rdeudoresdiario.Rdpedindiente = (decimal)vd.Pendiente;
                            rdeudoresdiario.Rddivision = vd.NivelId;
                            rdeudoresdiario.Div = vd.NivelNombre;
                            rdeudoresdiario.Rdapaterno = vd.AlApp;
                            rdeudoresdiario.Rdamaterno = vd.AlApm;
                            rdeudoresdiario.Rdnombre = vd.AlNombre;
                            rdeudoresdiario.Rdemail = vd.AlCorreoInst;
                            rdeudoresdiario.Rdcarid = vd.Idcarrera;
                            rdeudoresdiario.Carrera = vd.CarreraClave;
                            rdeudoresdiario.Rdnocontrol = vd.AlMatricula;
                            rdeudoresdiario.Rdgrado = "";
                            rdeudoresdiario.Rdgrupo = "";
                            rdeudoresdiario.RdemailT = vd.GaCorreoAlternativo;
                            rdeudoresdiario.RdnoConceptos = vd.NoCon;
                            //rdeudoresdiario.Rdbcid = Convert.ToInt32(bcid);
                            rdeudoresdiario.Rdbcid = 2;
                            rdeudoresdiario.Beca = bcdesc;
                            rdeudoresdiario.Rdbeinscrip = Convert.ToDecimal(beinscrip);
                            rdeudoresdiario.Rdbeparcialidades = Convert.ToDecimal(beparcialidades);
                            rdeudoresdiario.Rdbedesc = bcdesc;
                            rdeudoresdiario.Rdtutor = tutor;

                            rdeudoresdiario.Rdtelefono = vd.GaTelefonoTutor;
                            rdeudoresdiario.Rdestatus = vd.AlStatusActual;
                            rdeudoresdiario.Estatus = vd.SlDescripcion;
                            rdeudoresdiario.Rdconceptosdesc = descconceptos.Trim();
                        }



                        _sql_context.Rdeudoresdiario.Add(rdeudoresdiario);
                        _sql_context.SaveChanges();

                        if (convenio == 0)
                        {

                            //Verifica la direccion de correo del destinatario
                            VerificaCorreo(ref emailEnvio, email, rdemailt);
                            //Verifica el numero de direcciones en el correo
                            if (emailEnvio.Bcc.Count() >= 50)
                            {
                                Console.WriteLine("Envio de correo deudor dentro while direcciones incluidas " + emailEnvio.Bcc.Count().ToString());
                                emailEnvio.Subject = "***ATENTA INVITACIÓN FAVOR DE PASAR A LIQUIDAR ATRASOS***";
                                emailEnvio.Body = "<p class='MsoNormal'><strong>Estimado Padre de Familia y/o Alumno (a),</strong></p><p class='MsoNormal'></p><p class='MsoNormal'>Su fecha límite de pago ha vencido lo invitamos a cubrir sus pagos atrasados. Los montos pendientes <strong>vencidos</strong> al día de hoy <strong>" + DateTime.Now.Day.ToString() + " de " + MonthName(DateTime.Now.Month) + "</strong> ya cuentan con recargo este no se condona, no se aplaza y mucho menos se cancela.</p><p class='MsoNormal'>Le recordamos que las fechas de vencimiento de colegiaturas bajo reglamento, son los primeros <strong>CINCO DÍAS DEL MES.</strong> Lo invitamos a cumplir con el reglamento y liquidar sus pagos atrasados para evitar cualquier inconveniente con el acceso a la <strong>plataforma estudiantil</strong>.</p><p class='MsoNormal'></p><p class='MsoNormal'>Para consultar sus conceptos, fechas de vencimiento o realizar un pago, ingrese al sistema estudiantil en el perfil del alumno en la sección de pagos.</p><p class='MsoNormal'></p><p class='MsoNormal'><strong>IMPORTANTE: Los pagos que se realizan en sucursales de OXXO se ven reflejados de 24 a 48 hrs.</strong></p><p class='MsoNormal'></p><p class='MsoNormal'><strong>ATENTAMENTE</strong></p><p class='MsoNormal'><strong>DEPTO. DE PAGOS DE LA UNIVERSIDAD INTERAMERICANA.</strong></p> <p class='MsoNormal'><strong>**ESTE CORREO ES ENVIADO DE MANERA AUTOMÁTICA, NO ES NECESARIO RESPONDER**</strong></p>";
                                EnvioCorreo(emailEnvio);
                                emailEnvio.Bcc.Clear();
                                emailEnvio = new MailMessage();
                            }
                            //else
                            //    Console.WriteLine("Direcciones de correo vacias");
                        }
                        else
                        {
                            Console.WriteLine("Tiene convenio no se envia correo");
                        }





                    }

                    Rdeudores rdeudoresLast = (from RD in _sql_context.Rdeudores orderby RD.Rd descending select RD).FirstOrDefault();

                    rdeudoresLast.Rdtimefin = DateTime.Now;
                    _sql_context.SaveChanges();

                    if (emailEnvio.Bcc.Count() != 0)
                    {
                        Console.WriteLine("Envia correo al terminar while direcciones incluidas " + emailEnvio.Bcc.Count().ToString());
                        emailEnvio.Subject = "***ATENTA INVITACIÓN FAVOR DE PASAR A LIQUIDAR ATRASOS***";
                        emailEnvio.Body = "<p class='MsoNormal'><strong>Estimado Padre de Familia y/o Alumno (a),</strong></p><p class='MsoNormal'></p><p class='MsoNormal'>Su fecha límite de pago ha vencido lo invitamos a cubrir sus pagos atrasados. Los montos pendientes <strong>vencidos</strong> al día de hoy <strong>" + DateTime.Now.Day.ToString() + " de " + MonthName(DateTime.Now.Month) + "</strong> ya cuentan con recargo este no se condona, no se aplaza y mucho menos se cancela.</p><p class='MsoNormal'>Le recordamos que las fechas de vencimiento de colegiaturas bajo reglamento, son los primeros <strong>CINCO DÍAS DEL MES.</strong> Lo invitamos a cumplir con el reglamento y liquidar sus pagos atrasados para evitar cualquier inconveniente con el acceso a la <strong>plataforma estudiantil</strong>.</p><p class='MsoNormal'></p><p class='MsoNormal'>Para consultar sus conceptos, fechas de vencimiento o realizar un pago, ingrese al sistema estudiantil en el perfil del alumno en la sección de pagos.</p><p class='MsoNormal'></p><p class='MsoNormal'><strong>IMPORTANTE: Los pagos que se realizan en sucursales de OXXO se ven reflejados de 24 a 48 hrs.</strong></p><p class='MsoNormal'></p><p class='MsoNormal'><strong>ATENTAMENTE</strong></p><p class='MsoNormal'><strong>DEPTO. DE PAGOS DE LA UNIVERSIDAD INTERAMERICANA.</strong></p> <p class='MsoNormal'><strong>**ESTE CORREO ES ENVIADO DE MANERA AUTOMÁTICA, NO ES NECESARIO RESPONDER**</strong></p>";
                        EnvioCorreo(emailEnvio);
                        emailEnvio.Bcc.Clear();
                    }






                }




            }
            catch (Exception oEx)
            {
                Console.WriteLine(oEx.Message);
            }
            finally
            {
                Conexion.Close();
                Console.WriteLine("Conexión cerrada");
            }



        }


        public NpgsqlConnection ConexionPostgres()
        {
            string cadenaAcademia = "Server = interpue.com.mx; Port = 5435; Database = InterERP; User Id = pruebas; Password = Root.inter19!; Trust Server Certificate=true; Include Error Detail = true;";
            NpgsqlConnection conAcademia = new NpgsqlConnection(cadenaAcademia);
            return conAcademia;
        }




        private void GrupoBachiller(int aluid)
        {
            string cadenaBachiller = "Server = interpue.com.mx; Port = 5435; Database = InterERP; User Id = pruebas; Password = Root.inter19!; Trust Server Certificate=true; Include Error Detail = true;", consulta = "", amaiid = "", semestreid = "";
            NpgsqlConnection conBachiller = new NpgsqlConnection(cadenaBachiller);
            semestrenum = "";
            semestredesc = "";
            tutor = "";
            NpgsqlCommand cmd;
            try
            {
                conBachiller.Open();
                consulta = "select amaiid from tamateriaimpartir, taconfiguracion where tamateriaimpartir.amaianio = taconfiguracion.aconanio and tamateriaimpartir.amaiperiodo = taconfiguracion.aconperiodo";
                cmd = new NpgsqlCommand(consulta, conBachiller);
                NpgsqlDataReader DRPeriodo = cmd.ExecuteReader();
                while (DRPeriodo.Read())
                    amaiid = DRPeriodo[0].ToString().Trim();

                DRPeriodo.Close();

                //consulta = "select tagrupo.agruid, agraid, aluid, amatclave, amaiid, tagrupo.semestreid, tasemestre.semestredesc, tasemestre.semestrenum from tagrupotagrupoalumnos inner join tagrupo on tagrupotagrupoalumnos.agruid = tagrupo.agruid inner join tasemestre on tasemestre.semestreid = tagrupo.semestreid where aluid=" + aluid.ToString().Trim() + " and amaiid=" + amaiid.Trim() + " limit 1;";
                consulta = "select tagrupo.agruid, agraid, aluid, amatclave, amaiid, tagrupo.semestreid, tasemestre.semestredesc, tasemestre.semestrenum from tagrupotagrupoalumnos inner join tagrupo on tagrupotagrupoalumnos.agruid = tagrupo.agruid inner join tasemestre on tasemestre.semestreid = tagrupo.semestreid where aluid=" + aluid.ToString().Trim() + " and amaiid=" + amaiid.Trim() + " and tasemestre.semestrenum != 99 limit 1;";
                Console.WriteLine(consulta.ToString());
                cmd = new NpgsqlCommand(consulta, conBachiller);
                NpgsqlDataReader DRSemestre = cmd.ExecuteReader();
                semestreid = "";
                semestredesc = "";
                semestrenum = "";
                while (DRSemestre.Read())
                {
                    semestreid = DRSemestre[5].ToString().Trim();
                    semestredesc = DRSemestre[6].ToString().Trim();
                    semestrenum = DRSemestre[7].ToString().Trim();
                }
                DRSemestre.Close();

                tutor = "";
                if (semestreid != "")
                {
                    consulta = "select * from tatutorsemestre  inner join tatutor on tatutorsemestre.tclave=tatutor.tclave where tatutor.tclave != 'CY.CHAVEZ' and tatutor.tclave != 'CA.LOPEZ' and semestreid=" + semestreid.Trim() + ";";
                    Console.WriteLine(consulta.ToString());

                    cmd = new NpgsqlCommand(consulta, conBachiller);
                    NpgsqlDataReader DRTutor = cmd.ExecuteReader();
                    while (DRTutor.Read())
                    {
                        tutor = DRTutor[4].ToString().Trim();
                        Console.WriteLine(tutor.ToString());
                    }
                    DRTutor.Close();
                }
            }
            catch (Exception oEx)
            {
                Console.WriteLine(consulta.ToString());
                Console.WriteLine(tutor.ToString());
                Console.WriteLine(oEx.ToString());
            }
            finally
            {
                conBachiller.Close();
            }
        }


        public void infoBeca(Program obj, string nocontorl, string division)
        {
            string cmdTXT;
            NpgsqlDataReader ABeca;
            NpgsqlCommand comando;
            NpgsqlConnection Conexion = obj.ConexionPostgres();
            try
            {
                Conexion.Open();
                Console.WriteLine("Conexión abierta");

                infoPeriodo(obj);

                //Información de becas de deudores diarios.
                if (division == "BAC" || division == "LIC")
                    cmdTXT = "select ccbecacon.bcid, bcdesc, beinscrip, beparcialidades from ccbeca inner join ccbecacon on ccbecacon.bcid = ccbeca.bcid where beestatus='ACT' and beconfirma = 1 and beusario='" + nocontorl.Trim().ToUpper() + "' and beanio= '" + ccanio + "' and beperiodo='" + ccperiodo + "';";
                else
                    cmdTXT = "select ccbecacon.bcid, bcdesc, beinscrip, beparcialidades from ccbeca inner join ccbecacon on ccbecacon.bcid = ccbeca.bcid where beestatus='ACT' and beconfirma = 1 and beusario='" + nocontorl.Trim().ToUpper() + "';";

                Console.WriteLine(cmdTXT);

                comando = new NpgsqlCommand(cmdTXT, Conexion);
                ABeca = comando.ExecuteReader();
                bcid = "";
                bcdesc = "";
                beinscrip = 0.0;
                beparcialidades = 0.0;
                while (ABeca.Read())
                {
                    bcid = ABeca[0].ToString().Trim();
                    bcdesc = ABeca[1].ToString().Trim();
                    beinscrip = Convert.ToDouble(ABeca[2]);
                    beparcialidades = Convert.ToDouble(ABeca[3]);
                }


                ABeca.Close();
            }
            catch (Exception oEx)
            {
                Console.WriteLine(oEx.ToString());
            }
            finally
            {
                Conexion.Close();
            }
        }
        public void infoPeriodo(Program obj)
        {
            string cmdTXT;
            NpgsqlDataReader Config;
            NpgsqlCommand comando;
            NpgsqlConnection Conexion = obj.ConexionPostgres();
            try
            {
                Conexion.Open();
                Console.WriteLine("Conexión abierta");

                //Año y periodo actual.
                cmdTXT = "select ccconfiguracionanio, ccconfiguracionperiodo from ccconfiguracion;";

                Console.WriteLine(cmdTXT);

                comando = new NpgsqlCommand(cmdTXT, Conexion);
                Config = comando.ExecuteReader();
                ccanio = 0;
                ccperiodo = 0;
                while (Config.Read())
                {
                    ccanio = Convert.ToInt32(Config[0].ToString());
                    ccperiodo = Convert.ToInt32(Config[1].ToString());
                }
                //Console.WriteLine("ccanio:"+ccanio.ToString()+" ccperiodo:"+ccperiodo.ToString());
                Config.Close();
            }
            catch (Exception oEx)
            {
                Console.WriteLine(oEx.ToString());
            }
            finally
            {
                Conexion.Close();
            }
        }


        public void VerificaCorreo(ref MailMessage email, string mail, string mailt)
        {
            //Verifica la direccion de correo del destinatario
            if (mail.Equals(mailt))
            {
                if (!string.IsNullOrEmpty(mail))
                {
                    Console.WriteLine("correo: " + mail.ToString());
                    if (IsValidEmail(mail))
                    {
                        email.Bcc.Add(new MailAddress(mail));
                    }
                    else
                        Console.WriteLine("correo no valido: " + mail.ToString());
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(mail))
                {
                    Console.WriteLine("correo alumno: " + mail.ToString());
                    if (IsValidEmail(mail))
                    {
                        email.Bcc.Add(new MailAddress(mail));
                    }
                    else
                        Console.WriteLine("correo no valido: " + mail.ToString());
                }
                if (!string.IsNullOrEmpty(rdemailt))
                {
                    Console.WriteLine("correo tutor: " + mailt.ToString());
                    if (IsValidEmail(mailt))
                    {
                        email.Bcc.Add(new MailAddress(mailt));
                    }
                    else
                        Console.WriteLine("correo no valido: " + mailt.ToString());
                }
            }
        }

        public void Convenio(Program obj, string rdnocontrol)
        {
            string cmdTXT;
            NpgsqlDataReader Convenio;
            NpgsqlCommand comando;
            NpgsqlConnection Conexion = obj.ConexionPostgres();
            convenio = 0;
            convfefin = "";
            convfeinicio = "";
            try
            {
                Conexion.Open();

                //Obtener convenios
                cmdTXT = "select convfeinicio, convfefin from ccconvenios where convusuario='" + rdnocontrol.Trim() + "' and '" + DateTime.Today.ToString("yyyy-MM-dd") + "'>=convfeinicio and '" + DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd") + "'<= convfefin;";
                comando = new NpgsqlCommand(cmdTXT, Conexion);
                Convenio = comando.ExecuteReader();
                while (Convenio.Read())
                {
                    convenio = 1;
                    DateTime auxFecha = Convert.ToDateTime(Convenio[0]);
                    convfeinicio = auxFecha.Year.ToString().Trim() + '-' + auxFecha.Month.ToString().Trim() + '-' + auxFecha.Day.ToString().Trim();
                    auxFecha = Convert.ToDateTime(Convenio[1]);
                    convfefin = auxFecha.Year.ToString().Trim() + '-' + auxFecha.Month.ToString().Trim() + '-' + auxFecha.Day.ToString().Trim();

                    //identifica los dias restantes del convenio
                    TimeSpan restante = auxFecha.Date - DateTime.Now.Date;
                    int dias = restante.Days;
                    Console.WriteLine("dias restantes" + dias.ToString());

                    //Se valida si esta activo el envio de correo
                    if (false)
                    {
                        emailConvenio = new MailMessage();
                        //Verifica la direccion de correo del destinatario
                        VerificaCorreoC(ref emailConvenio, email, rdemailt);

                        if (dias == 1 && aluestatus != "55")
                        {
                            Console.WriteLine("convenio proximo a vencer" + dias.ToString());

                            //Verifica que las direcciones de correo sean correctas
                            if (emailConvenio.To.Count() > 0)
                            {
                                Console.WriteLine("Envio de correo convenio proximo");
                                emailConvenio.Subject = "RECORDATORIO DE CIERRE DE CONVENIO";
                                emailConvenio.Body = "<p class='MsoNormal'><strong>Estimado Padre de Familia y/o Alumno (a),</strong></p><p class='MsoNormal'></p><p class='MsoNormal'>Le recordamos que la fecha limite para cubrir su convenio esta próxima a vencer, lo invitamos a realizar el pago correspondiente al monto acordado para evitar cualquier inconveniente con el acceso a la <strong>plataforma estudiantil</strong>.</p><p class='MsoNormal'>Para consultar sus conceptos, fechas de vencimiento o realizar un pago, ingrese al sistema estudiantil en el perfil del alumno en la sección de pagos.</p><p class='MsoNormal'></p><p class='MsoNormal'><strong>IMPORTANTE: Los pagos que se realizan en sucursales de OXXO se ven reflejados de 24 a 48 hrs.</strong></p><p class='MsoNormal'></p><p class='MsoNormal'><strong>ATENTAMENTE</strong></p><p class='MsoNormal'><strong>DEPTO. DE PAGOS DE LA UNIVERSIDAD INTERAMERICANA.</strong></p><p class='MsoNormal'><strong>**Este correo es enviado de manera automatica, no es necesario responder**</strong></p>";
                                EnvioCorreo(emailConvenio);
                            }
                            else
                                Console.WriteLine("Direcciones de correo vacias");
                        }
                        else
                        {
                            if (dias == -1 && aluestatus != "55")
                            {
                                convenio = 0;
                                Console.WriteLine("convenio vencido" + dias.ToString());

                                //Verifica que las direcciones de correo sean correctas
                                if (emailConvenio.To.Count() > 0)
                                {
                                    Console.WriteLine("Envio de correo convenio vencido");
                                    emailConvenio.Subject = "RECORDATORIO DE CONVENIO VENCIDO";
                                    emailConvenio.Body = "<p class='MsoNormal'><strong>Estimado Padre de Familia y/o Alumno (a),</strong></p><p class='MsoNormal'></p><p class='MsoNormal'><strong>La fecha para cubrir su convenio registrado a vencido</strong>. Los montos pendientes <strong>vencidos</strong> al día de hoy <strong>" + DateTime.Now.Day.ToString() + " de " + MonthName(DateTime.Now.Month) + "</strong> ya cuentan con recargo este no se condona, no se aplaza y mucho menos se cancela.</p><p class='MsoNormal'>Lo invitamos a liquidar pagos atrasados para evitar cualquier inconveniente con el acceso a la <strong>plataforma estudiantil</strong>.</p><p class='MsoNormal'></p><p class='MsoNormal'>Para consultar sus conceptos, fechas de vencimiento o realizar un pago, ingrese al sistema estudiantil en el perfil del alumno en la sección de pagos.</p><p class='MsoNormal'></p><p class='MsoNormal'><strong>IMPORTANTE: Los pagos que se realizan en sucursales de OXXO se ven reflejados de 24 a 48 hrs.</strong></p><p class='MsoNormal'></p><p class='MsoNormal'><strong>ATENTAMENTE</strong></p><p class='MsoNormal'><strong>DEPTO. DE PAGOS DE LA UNIVERSIDAD INTERAMERICANA.</strong></p><p class='MsoNormal'><strong>**Este correo es enviado de manera automatica, no es necesario responder**</strong></p>";
                                    EnvioCorreo(emailConvenio);
                                }
                                else
                                    Console.WriteLine("Direcciones de correo vacias");
                            }
                        }
                    }

                    //Convenio[0].ToString().Trim().Substring(5, 4) + '-' + Convenio[0].ToString().Trim().Substring(3, 2) + '-' + Convenio[0].ToString().Trim().Substring(0, 2);
                    //convfefin = Convenio[1].ToString().Trim().Substring(6, 4) + '-' + Convenio[1].ToString().Trim().Substring(3, 2) + '-' + Convenio[1].ToString().Trim().Substring(0, 2);

                }

                Convenio.Close();

            }
            catch (Exception oEx)
            {
                Console.WriteLine(oEx.ToString());
            }
            finally
            {
                Conexion.Close();
            }
        }

        public string MonthName(int month)
        {
            DateTimeFormatInfo dtinfo = new CultureInfo("es-ES", false).DateTimeFormat;
            return dtinfo.GetMonthName(month).ToUpper();
        }

        public void VerificaCorreoC(ref MailMessage email, string mail, string mailt)
        {
            //Verifica la direccion de correo del destinatario
            if (mail.Equals(mailt))
            {
                if (!string.IsNullOrEmpty(mail))
                {
                    Console.WriteLine("correo: " + mail.ToString());
                    if (IsValidEmail(mail))
                    {
                        email.To.Add(new MailAddress(mail));
                    }
                    else
                        Console.WriteLine("correo no valido: " + mail.ToString());
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(mail))
                {
                    Console.WriteLine("correo alumno: " + mail.ToString());
                    if (IsValidEmail(mail))
                    {
                        email.To.Add(new MailAddress(mail));
                    }
                    else
                        Console.WriteLine("correo no valido: " + mail.ToString());
                }
                if (!string.IsNullOrEmpty(rdemailt))
                {
                    Console.WriteLine("correo tutor: " + mailt.ToString());
                    if (IsValidEmail(mailt))
                    {
                        email.To.Add(new MailAddress(mailt));
                    }
                    else
                        Console.WriteLine("correo no valido: " + mailt.ToString());
                }
            }
        }

        public static bool IsValidEmail(string email)
        {
            // Return true if strIn is in valid e-mail format.
            string expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";

            return Regex.IsMatch(email,
                   @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))" +
                   @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");

            //if (Regex.IsMatch(email, expresion))
            //{
            //    if (Regex.Replace(email, expresion, String.Empty).Length == 0)
            //    { return true; }
            //    else
            //    { return false; }
            //}
            //else
            //{ return false; }
        }

        private static void EnvioCorreo(MailMessage mail)
        {
            //MailMessage email = new MailMessage();
            //email.To.Add(new MailAddress("example@example.com"));
            //email.Subject = "Asunto ( " + DateTime.Now.ToString("dd / MMM / yyy hh:mm:ss") + " ) ";
            //email.Body = "Cualquier contenido en <b>HTML</b> para enviarlo por correo electrónico.";

            mail.From = new MailAddress("depto.pagos@lainter.edu.mx", "Departamento de pagos"); //Pass: Quechol.15
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.Normal;

            SmtpClient smtp = new SmtpClient(); //Objeto smtp
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            //Credenciales de inicio de sesion
            smtp.Credentials = new NetworkCredential("notificaciones@lainter.edu.mx", "Root.inter20");

            string output = null;

            try
            {
                smtp.Send(mail);
                output = "Correo electrónico fue enviado satisfactoriamente.";
                Thread.Sleep(15000);
            }
            catch (Exception ex)
            {
                output = "Error enviando correo electrónico: " + ex.Message;
            }
            finally
            {
                mail.Dispose();
            }

            Console.WriteLine(output);

        }
    }
}
