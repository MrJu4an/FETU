<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Salir.aspx.cs" Inherits="FETU.Salir" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>FETU</title>
    <script>
        function removeLocalStorage(key){
            localStorage.removeItem(key);
        };
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
    <script type="text/javascript">
        window.open("Login.aspx","_top")
    </script>
</body>
</html>
