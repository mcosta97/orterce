﻿using ProyectoTallerData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Logout : Page {

    protected void Page_Load(object sender, EventArgs e) {
        Session.Clear();
        Response.Redirect("Principal.aspx");
    }

}