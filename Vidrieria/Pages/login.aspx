<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Vidrieria.Pages.login" %>

<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title>TECRISI | Login</title>
    <link rel="stylesheet" href="../public/css/login-custom.css">
</head>
<body class="login-page">
    <form id="frmAcceso" runat="server" class="login-box">
        <div class="logo-container">
            <img src="../public/img/logo.jpg" alt="Logo" class="logo">
        </div>
        <div class="form">
            <p class="heading">Acceso</p>
            <asp:TextBox ID="txtUsuario" runat="server" CssClass="input" placeholder="Usuario"></asp:TextBox>
            <asp:TextBox ID="txtClave" runat="server" CssClass="input" TextMode="Password" placeholder="Contraseña"></asp:TextBox>
            <asp:Button ID="btnIngresar" runat="server" Text="Ingresar" CssClass="btn" OnClick="btnIngresar_Click" />
        </div>
    </form>
</body>
</html>
