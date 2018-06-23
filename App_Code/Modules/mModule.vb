Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Text.RegularExpressions

Public Module mModule
    Function GetDivision(ByVal sPageNo As String) As String
        Dim sHtml As String = ""
        Dim sSQL As String
        Dim sUrl As String = GetUrlMobile()
        sSQL = " SELECT * FROM [Good Menu] WHERE [Menu Type] = 0 AND Published = 1 AND [Page No_] = '" & sPageNo & "' ORDER BY [Menu Order] "
        Dim tData As DataTable
        tData = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, sSQL).Tables(0)
        sUrl &= sPageNo & "/" & "sp/"
        Dim nIJ As Integer, kIJ As Integer = 1
        sHtml &= ""
        For nIJ = 0 To tData.Rows.Count - 1
            sHtml &= "<a href=""" & sUrl & tData.Rows(nIJ).Item("Link Display") & "/"">" & vbCrLf
            sHtml &= "<div class=""menu-icon"" style=""background-color:#8D8D8D"
            sHtml &= ";background-position: center 10px;background-image: url('" & GetUrl() & "/Images/ProductGroup/" & tData.Rows(nIJ).Item("Images URL") & "');background-repeat: no-repeat;"">" & vbCrLf

            sHtml &= "<div class=""descreption"">" & vbCrLf
            sHtml &= tData.Rows(nIJ).Item("name") & vbCrLf
            sHtml &= "</div>" & vbCrLf
            sHtml &= "</div>" & vbCrLf
            sHtml &= "</a>" & vbCrLf
        Next
        Return sHtml
    End Function

    Function GetMenuByDivForMobile(ByVal sDivisionNo As String) As String
        Dim SQL As String, nIJ As Integer, sHtml As String = ""
        Dim sUrl As String = GetUrl()
        Dim sProducUrl As String = sUrl
        Dim pTbl As DataTable
        Dim objGmn As New adv.Business.GoodMenu
        objGmn.Load(sDivisionNo)

        sProducUrl &= "m/cat/"

        SQL = " SELECT * FROM [Good Menu] WHERE [Parent No_] = '" & sDivisionNo & "' AND Published = 1 AND [Web Mobile] = 1 ORDER BY [Menu Order] "

        pTbl = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, SQL).Tables(0)
        For nIJ = 0 To pTbl.Rows.Count - 1
            sHtml &= "<a href=""" & sProducUrl & pTbl.Rows(nIJ).Item("Link Display") & "/"">"
            sHtml &= "<div class=""menu-icon"" style=""background-color:" & objGmn.BackgroundColor & ";"">" & vbCrLf
            sHtml &= "<div><center>" & vbCrLf
            sHtml &= "<img alt=""" & pTbl.Rows(nIJ).Item("Name") & """ src=""" & sUrl & "Images/ProductGroup/" & pTbl.Rows(nIJ).Item("Images URL") & """ width=""80px"" border=""0""/>" & vbCrLf
            sHtml &= "</center></div>" & vbCrLf
            sHtml &= "<div class=""descreption"">" & vbCrLf
            sHtml &= pTbl.Rows(nIJ).Item("name") & vbCrLf
            sHtml &= "</div>" & vbCrLf
            sHtml &= "</div>" & vbCrLf
            sHtml &= "</a>"

        Next
        Return sHtml
    End Function

    Function GetMenuMobile(ByVal sPage As String) As String
        Dim sMenu As String = ""
        sMenu &= "<div class = ""window"">" & vbCrLf
        sMenu &= "<div id=""nav"">" & vbCrLf
        sMenu &= "<ul>" & vbCrLf
        sMenu &= "<li>" & vbCrLf
        sMenu &= "<span tabindex=""1"" >" & vbCrLf
        sMenu &= "<img scr=""" & GetUrlMobile() & "m/Images/template/list.png"">"
        sMenu &= "</span>" & vbCrLf
        sMenu &= "<ul>" & vbCrLf
        sMenu &= "<div >" & vbCrLf
        sMenu &= GetDivision(sPage)
        sMenu &= "</div>" & vbCrLf
        sMenu &= "</ul>" & vbCrLf
        sMenu &= "</li>" & vbCrLf
        sMenu &= "</ul>" & vbCrLf
        sMenu &= "</div>" & vbCrLf
        sMenu &= "</div>" & vbCrLf
        Return sMenu
    End Function

    Function GetDivisionSmall() As String
        Dim sHtml As String = ""
        Dim sSQL As String
        Dim sUrl As String = GetUrlMobile()
        sSQL = " SELECT * FROM [Good Menu] WHERE [Menu Type] = 0 AND Published = 1 ORDER BY [Menu Order] "
        Dim tData As DataTable
        tData = DBHelper.ExecuteDataset(GetConnectString, CommandType.Text, sSQL).Tables(0)
        sUrl &= "sp/"
        Dim nIJ As Integer, kIJ As Integer = 1
        sHtml &= ""
        For nIJ = 0 To tData.Rows.Count - 1
            sHtml &= "<a href=""" & sUrl & tData.Rows(nIJ).Item("Link Display") & "/"">" & vbCrLf
            sHtml &= "<div class=""menu-iconsmall"" style=""background-color:" & tData.Rows(nIJ).Item("Background Color") & ";"">" & vbCrLf
            sHtml &= "<div class=""descreption"">" & vbCrLf
            sHtml &= tData.Rows(nIJ).Item("name") & vbCrLf
            sHtml &= "</div>" & vbCrLf
            sHtml &= "</div>" & vbCrLf
            sHtml &= "</a>" & vbCrLf
        Next
        Return sHtml
    End Function
End Module

