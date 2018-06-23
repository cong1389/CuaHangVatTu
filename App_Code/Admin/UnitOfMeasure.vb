Imports System.Data
Imports Microsoft.VisualBasic

Namespace adv.Business

    Public Class UnitOfMeasure
#Region "Khai báo"
        Private mNo_ As String = ""
        Private mName As String = ""

        Property No_() As String
            Get
                Return mNo_
            End Get
            Set(ByVal value As String)
                mNo_ = value
            End Set
        End Property
        Property Name() As String
            Get
                Return mName
            End Get
            Set(ByVal value As String)
                mName = value
            End Set
        End Property

#End Region
#Region "Function"
        Public Function Load(ByVal sNo_ As String) As Boolean
            Dim StrSQL As String
            Dim SqlReader As IDataReader
            Try
                StrSQL = " SELECT * FROM [Unit Of Measure] WHERE No_ = '" & sNo_ & "' "
                SqlReader = DBHelper.ExecuteReader(GetConnectString, CommandType.Text, StrSQL)
                If SqlReader.Read() Then
                    mNo_ = GetValue(SqlReader, 0, typeOfColumn.GetString)
                    mName = GetValue(SqlReader, 1, typeOfColumn.GetString)
                    SqlReader.Close()
                    Return True
                Else
                    SqlReader.Close()
                    Return False
                End If
            Catch ex As Exception

                Return False
            End Try
        End Function
        Public Function Save(ByVal sOldNo_ As String) As Boolean
            Dim arParams() As IDataParameter = New IDataParameter(2) {}
            Try
                arParams(0) = DBHelper.createParameter("@No_", SqlDbType.NVarChar, ParameterDirection.Input, mNo_)
                arParams(1) = DBHelper.createParameter("@Name", SqlDbType.NVarChar, ParameterDirection.Input, mName)
                arParams(2) = DBHelper.createParameter("@OldNo_", SqlDbType.NVarChar, ParameterDirection.Input, sOldNo_)
                DBHelper.ExecuteNonQuery(GetConnectString, CommandType.StoredProcedure, "SP_UnitOfMeasure", arParams)
                Return True
            Catch ex As Exception

                Return False
            End Try
        End Function
#End Region

    End Class

End Namespace