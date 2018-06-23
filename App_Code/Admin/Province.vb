Imports Microsoft.VisualBasic
Imports System.Data

Namespace adv.Business

    Public Class Province
#Region "Khai báo"
        Private mNo_ As String = ""
        Private mName As String = ""
        Private mType As Integer = 0
        Private mParentNo_ As String = ""
        Private mPublished As Integer = 0
        Private mBigCity As Integer = 0
        Private mReadytoDelivery As Integer = 0
        Private mZoneCode As String = ""
        Private mNumofDayFrom As Integer = 0
        Private mNumofDayTo As Integer = 0

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
        Property Type() As Integer
            Get
                Return mType
            End Get
            Set(ByVal value As Integer)
                mType = value
            End Set
        End Property
        Property ParentNo_() As String
            Get
                Return mParentNo_
            End Get
            Set(ByVal value As String)
                mParentNo_ = value
            End Set
        End Property
        Property Published() As Integer
            Get
                Return mPublished
            End Get
            Set(ByVal value As Integer)
                mPublished = value
            End Set
        End Property
        Property BigCity() As Integer
            Get
                Return mBigCity
            End Get
            Set(ByVal value As Integer)
                mBigCity = value
            End Set
        End Property
        Property ReadytoDelivery() As Integer
            Get
                Return mReadytoDelivery
            End Get
            Set(ByVal value As Integer)
                mReadytoDelivery = value
            End Set
        End Property
        Property ZoneCode() As String
            Get
                Return mZoneCode
            End Get
            Set(ByVal value As String)
                mZoneCode = value
            End Set
        End Property
        Property NumofDayFrom As Integer
            Get
                Return mNumofDayFrom
            End Get
            Set(ByVal value As Integer)
                mNumofDayFrom = value
            End Set
        End Property
        Property NumofDayTo As Integer
            Get
                Return mNumofDayTo
            End Get
            Set(ByVal value As Integer)
                mNumofDayTo = value
            End Set
        End Property
#End Region
#Region "Function"
        Public Function Load(ByVal sNo As String) As Boolean
            Dim StrSQL As String
            Dim SqlReader As IDataReader
            Try
                StrSQL = " SELECT * FROM [Province] WHERE No_ = '" & sNo & "' "
                SqlReader = DBHelper.ExecuteReader(GetConnectString, CommandType.Text, StrSQL)
                If SqlReader.Read() Then
                    mNo_ = GetValue(SqlReader, 0, typeOfColumn.GetString)
                    mName = GetValue(SqlReader, 1, typeOfColumn.GetString)
                    mType = GetValue(SqlReader, 2, typeOfColumn.GetInt32)
                    mParentNo_ = GetValue(SqlReader, 3, typeOfColumn.GetString)
                    mPublished = GetValue(SqlReader, 4, typeOfColumn.GetInt32)
                    mBigCity = GetValue(SqlReader, 5, typeOfColumn.GetInt32)
                    mReadytoDelivery = GetValue(SqlReader, 6, typeOfColumn.GetInt32)
                    mZoneCode = GetValue(SqlReader, 7, typeOfColumn.GetString)
                    mNumofDayFrom = GetValue(SqlReader, 8, typeOfColumn.GetInt32)
                    mNumofDayTo = GetValue(SqlReader, 9, typeOfColumn.GetInt32)
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
        Public Function Save(ByVal sOldNo As String) As Boolean
            Dim arParams() As IDataParameter = New IDataParameter(10) {}
            Try
                arParams(0) = DBHelper.createParameter("@No_", SqlDbType.NVarChar, ParameterDirection.Input, mNo_)
                arParams(1) = DBHelper.createParameter("@Name", SqlDbType.NVarChar, ParameterDirection.Input, mName)
                arParams(2) = DBHelper.createParameter("@Type", SqlDbType.Int, ParameterDirection.Input, mType)
                arParams(3) = DBHelper.createParameter("@ParentNo_", SqlDbType.NVarChar, ParameterDirection.Input, mParentNo_)
                arParams(4) = DBHelper.createParameter("@Published", SqlDbType.Int, ParameterDirection.Input, mPublished)
                arParams(5) = DBHelper.createParameter("@BigCity", SqlDbType.Int, ParameterDirection.Input, mBigCity)
                arParams(6) = DBHelper.createParameter("@ReadytoDelivery", SqlDbType.Int, ParameterDirection.Input, mReadytoDelivery)
                arParams(7) = DBHelper.createParameter("@ZoneCode", SqlDbType.NVarChar, ParameterDirection.Input, mZoneCode)
                arParams(8) = DBHelper.createParameter("@NumofDayFrom", SqlDbType.Int, ParameterDirection.Input, mNumofDayFrom)
                arParams(9) = DBHelper.createParameter("@NumofDayTo", SqlDbType.Int, ParameterDirection.Input, mNumofDayTo)
                arParams(10) = DBHelper.createParameter("@OldNo", SqlDbType.NVarChar, ParameterDirection.Input, sOldNo)
                DBHelper.ExecuteNonQuery(GetConnectString, CommandType.StoredProcedure, "SP_Province", arParams)
                Return True
            Catch ex As Exception

                Return False
            End Try
        End Function
#End Region


    End Class

End Namespace