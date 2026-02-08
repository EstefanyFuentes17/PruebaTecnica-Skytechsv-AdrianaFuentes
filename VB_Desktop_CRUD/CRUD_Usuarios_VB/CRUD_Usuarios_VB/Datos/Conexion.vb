Imports System.Data.SqlClient
Imports System.Configuration

Public Class Conexion
    Private Shared cadenaConexion As String = "Data Source=;Initial Catalog=DB_Skytechsv;Integrated Security=True"

    Public Function ObtenerConexion() As SqlConnection
        Dim conexion As New SqlConnection(cadenaConexion)
        Return conexion
    End Function

End Class
