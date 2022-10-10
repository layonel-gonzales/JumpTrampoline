<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Jump_Trampoline.Paginas.WebForm1" %>

<!DOCTYPE html>



<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">




<script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyD0hh2YxvxcanujaunpOm9JQcWIuh_l6EM&callback=initMap&libraries=places"></script>





    <script language="javascript">




        function initialize() {
            var input = document.getElementById('TxtDir');
            var autocomplete = new google.maps.places.Autocomplete(input);
            alert(autocomplete);
            google.maps.event.addListener(autocomplete, 'place_changed', function () {
                var place = autocomplete.getPlace();
                document.getElementById('TxtDir').value = place.name;
                //document.getElementById('cityLat').value = place.geometry.location.lat();
                //document.getElementById('cityLng').value = place.geometry.location.lng();
            });
        }
        
        google.maps.event.addDomListener(window, 'load', initialize);



    </script>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
        <asp:TextBox ID="TxtDir" runat="server"></asp:TextBox>
    </form>
</body>
</html>
