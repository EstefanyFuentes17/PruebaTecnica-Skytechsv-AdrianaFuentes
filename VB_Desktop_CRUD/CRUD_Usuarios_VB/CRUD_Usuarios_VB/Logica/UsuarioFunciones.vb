Imports System.Data.SqlClient
Imports System.Data

Public Class UsuarioFunciones

    Public Function ObtenerUsuarios(Optional TextBusqueda As String = "") As DataTable
        Dim dt As New DataTable()
        Dim conexion As New Conexion()
        Using conn As SqlConnection = conexion.ObtenerConexion()
            If conn IsNot Nothing Then
                Try
                    Dim cmd As New SqlCommand("sp_ListarUsuarios", conn)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@Filtro", TextBusqueda)

                    Dim adapter As New SqlDataAdapter(cmd)
                    adapter.Fill(dt)
                Catch ex As Exception
                    Throw New Exception("Error al conectar a la base de datos: " & ex.Message)
                    End
                End Try
            End If
        End Using
        Return dt
    End Function

    Public Function AgregarUsuario(nombre As String, username As String, password As String) As Boolean
        Dim conexion As New Conexion()
        Using conn As SqlConnection = conexion.ObtenerConexion()
            If conn IsNot Nothing Then
                Try
                    Dim cmd As New SqlCommand("sp_InsertarUsuario", conn)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@NombreCompleto", nombre)
                    cmd.Parameters.AddWithValue("@Username", username)
                    cmd.Parameters.AddWithValue("@Password", password)
                    conn.Open()
                    cmd.ExecuteNonQuery()
                    Return True
                Catch ex As Exception
                    Throw New Exception("Error al conectar a la base de datos: " & ex.Message)
                End Try
            End If
        End Using
        Return False
    End Function

    Public Function EditarUsuario(id As Integer, nombre As String, username As String, password As String, estado As Boolean) As Boolean
        Dim conexion As New Conexion()
        Using conn As SqlConnection = conexion.ObtenerConexion()
            If conn IsNot Nothing Then
                Try
                    Dim cmd As New SqlCommand("sp_ActualizarUsuario", conn)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@ID", id)
                    cmd.Parameters.AddWithValue("@NombreCompleto", nombre)
                    cmd.Parameters.AddWithValue("@Username", username)
                    cmd.Parameters.AddWithValue("@Password", password)
                    cmd.Parameters.AddWithValue("@Estado", estado)
                    conn.Open()
                    cmd.ExecuteNonQuery()
                    Return True
                Catch ex As Exception
                    Throw New Exception("Error al conectar a la base de datos: " & ex.Message)
                End Try
            End If
        End Using
        Return False
    End Function

    Public Function EliminarUsuario(id As Integer) As Boolean
        Dim conexion As New Conexion()
        Using conn As SqlConnection = conexion.ObtenerConexion()
            If conn IsNot Nothing Then
                Try
                    Dim cmd As New SqlCommand("sp_EliminarUsuario", conn)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@ID", id)
                    conn.Open()
                    cmd.ExecuteNonQuery()
                    Return True
                Catch ex As Exception
                    Throw New Exception("Error al conectar a la base de datos: " & ex.Message)
                End Try
            End If
        End Using
        Return False
    End Function

End Class
