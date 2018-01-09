﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proyecto
{
    public partial class Login : System.Web.UI.Page
    {
        //localhost.WebServiceProyecto sr = new localhost.WebServiceProyecto();
        ServiceReference1.WebServiceProyectoSoapClient sr = new ServiceReference1.WebServiceProyectoSoapClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Nombre"] != null)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertIns", "alert('" + Session["Nombre"].ToString() + "');", true);
            }
            if (!IsPostBack)
            {
                this.form1.Attributes.Add("autocomplete", "off");
            }


        }

        protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
        {
            if (Login1.UserName == "admin" && Login1.Password == "1234")
            {
                e.Authenticated = true;
                Session["Nombre"] = "admin";
                Response.Redirect("Inicio.aspx");
            }
            else
            {
                //localhost.WebServiceProyecto sr = new localhost.WebServiceProyecto();
                
                ServiceReference1.WebServiceProyectoSoapClient sr = new ServiceReference1.WebServiceProyectoSoapClient();
                if (sr.Validar(Login1.UserName, Login1.Password))
                {
                    e.Authenticated = true;
                    Session["Nombre"] = Login1.UserName;
                    Response.Redirect("Inicio.aspx");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertIns", "alert('El usuario o el password son incorrectos. Verifique sus datos');", true);
                    e.Authenticated = false;
                }
            }
        }
    }
}