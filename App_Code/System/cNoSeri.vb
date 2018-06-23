Imports Microsoft.VisualBasic
Imports System.Data

Namespace adv.Business
    Public Class cNoSeri
        Dim mNoSeriType As String = ""
        Dim mPrefix As String = ""
        Dim mSuffix As String = ""
        Dim mType As String = ""
        Dim mLen As Integer = 10
        Dim mNo_ As Double

        Property NoSeriType() As String
            Get
                Return mNoSeriType
            End Get
            Set(ByVal value As String)
                mNoSeriType = value
            End Set
        End Property

        Property No_() As Double
            Get
                Return mNo_
            End Get
            Set(ByVal value As Double)
                mNo_ = value
            End Set
        End Property

        Property Prefix() As String
            Get
                Return mPrefix
            End Get
            Set(ByVal value As String)
                mPrefix = value
            End Set
        End Property

        Public Function Load(ByVal pNoSeriType As String) As Boolean
            Dim StrSQL As String
            Dim SqlReader As IDataReader
            Dim arParams() As IDataParameter = New IDataParameter(1) {}
            Try
                StrSQL = "SELECT * FROM [No_ Seri] WHERE Rtrim(NoSeriType) = '" & pNoSeriType.Trim & "'"
                SqlReader = DBHelper.ExecuteReader(GetConnectString, CommandType.Text, StrSQL, arParams)
                If SqlReader.Read() Then
                    mNoSeriType = GetValue(SqlReader, 0, typeOfColumn.GetString)
                    mPrefix = GetValue(SqlReader, 1, typeOfColumn.GetString)
                    mSuffix = GetValue(SqlReader, 2, typeOfColumn.GetString)
                    mType = GetValue(SqlReader, 3, typeOfColumn.GetString)
                    mLen = GetValue(SqlReader, 4, typeOfColumn.GetInt32)
                End If
                SqlReader.Close()
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function

        Function TinhSuffix(ByVal strType$, ByVal pYYMM$) As String
            Dim pSuffix As String = ""
            Dim pThang As Integer
            pThang = CInt(sRight(pYYMM, 2))
            Select Case strType
                Case "Y"
                    pSuffix = "-" & sMid(pYYMM, 3, 2)
                Case "H"
                    If pThang <= 6 Then
                        pSuffix = "-01" & sMid(pYYMM, 3, 2)
                    Else
                        pSuffix = "-02" & sMid(pYYMM, 3, 2)
                    End If
                Case "Q"
                    If pThang >= 1 And pThang <= 3 Then
                        pSuffix = "-01" & sMid(pYYMM, 3, 2)
                    End If
                    If pThang >= 4 And pThang <= 6 Then
                        pSuffix = "-02" & sMid(pYYMM, 3, 2)
                    End If
                    If pThang >= 7 And pThang <= 9 Then
                        pSuffix = "-03" & sMid(pYYMM, 3, 2)
                    End If
                    If pThang >= 10 And pThang <= 12 Then
                        pSuffix = "-04" & sMid(pYYMM, 3, 2)
                    End If
                Case "M"
                    pSuffix = sMid(pYYMM, 3, 2) & sRight("00" & pThang, 2)
            End Select
            Return pSuffix
        End Function

        Public Function CreateNoSeri(ByVal pYYMM As String) As String
            Dim strValue As String
            Dim sSuffix As String = ""
            Dim arParams() As IDataParameter = New IDataParameter(2) {}
            arParams(0) = DBHelper.createParameter("@NoSeriType", SqlDbType.NVarChar, ParameterDirection.Input, mNoSeriType.Trim)
            arParams(1) = DBHelper.createParameter("@YM", SqlDbType.NVarChar, ParameterDirection.Input, pYYMM)
            arParams(2) = DBHelper.createParameter("@No_", SqlDbType.Decimal, ParameterDirection.Output, 0)
            DBHelper.ExecuteNonQuery(GetConnectString, CommandType.StoredProcedure, "W_IssuedNoSeri", arParams)
            mNo_ = arParams(2).Value
            sSuffix = TinhSuffix(mType, pYYMM)
            strValue = sSuffix & sRight("000000000000000" & arParams(2).Value.ToString, mLen - Trim(mPrefix).Length - sSuffix.Length)
            strValue = Trim(mPrefix) & strValue
            Return strValue.ToString
        End Function

        Public Function LoadNoSeri(ByVal strType As String, ByVal strYM As String, Optional ByVal strCondition As String = "") As DataTable
            Dim pSQL As String = ""
            Dim pWhere As String = ""
            Try
                If strType = "M" Then
                    pWhere = " WHERE TG.Thang = '" & strYM & "'"
                End If
                If strType = "Q" Then
                    pWhere = " WHERE TG.Quy = '" & Month2Quarter(strYM) & "'"
                End If
                If strType = "Y" Then
                    pWhere = " WHERE TG.Nam = '" & sLeft(strYM, 4) & "'"
                End If
                If strCondition.Trim <> "" Then
                    pWhere = " And " & strCondition
                End If
                pSQL = " SELECT CT.NoSeriType, CT.Prefix, CT.Len, CT.DienGiai, TG.No_ " & _
                    " FROM [No_ Seri] CT LEFT join [POS Issued NoSeri] TG on CT.NoSeriType = TG.NoSeriType " & _
                    pWhere
                Return DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, pSQL).Tables(0)
            Catch ex As Exception

                Return Nothing
            End Try
        End Function

    End Class
End Namespace