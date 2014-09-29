
<!--#include file="aesLibrary.asp"-->

<%
dim fileNameOrId,filetype,xmlStr,objDOM
'logError("Encryption Execution Called")
fileNameOrId = Request.QueryString("fn")
filetype = Request.QueryString("t")
'logError(" ")
'logError(" ")
'logError("-----------------------------------------------------------------------------")
'logError("*********Start*******")
'logError("Encryption Execution Started")
'logError("Request Type: " & filetype)
'logError("Request String: " & fileNameOrId)

if(Request.QueryString <>"" or Request.QueryString <> null) then

If filetype = "oId" then
    'filename="orderId.txt"
 On Error Resume Next
    xmlStr= AESEncrypt(fileNameOrId, "testpage")
    'logError("Order id encypted Result - " & xmlStr)
    response.Write xmlStr
    'xmlStr= AESDecrypt(xmlStr, "testpage")
    'logError("Order id decrypted Result - " & xmlStr)
    
    If Err.Number <> 0 Then
    'logError("Error in OrderId Encrypiton file. Err Description:" & Err.Description)
    End If
 On Error GoTo 0
end If
If filetype = "orderxml" Then
 On Error Resume Next
    'Create an XML DOM object from the XML file
    Set objDOM  = CreateObject("Microsoft.XMLDOM")   
    objDOM.async = false   
    'response.Write Server.MapPath("order.xml")
    'response.End
    objDOM.load(Server.MapPath("xml\" & fileNameOrId))
    xmlStr = objDOM.xml

    xmlStr= AESEncrypt(xmlStr, "testpage")
    'logError("Order xml encrypted Result - " & xmlStr)
    response.Write xmlStr

    'xmlStr= AESDecrypt(xmlStr, "testpage")
    'logError("Order xml decrypted Result - " & xmlStr)

    If Err.Number <> 0 Then
    'logError("Error in Order.xml file. Err Description:" & Err.Description)
    End If
 On Error GoTo 0
	  'response.End
	 'response.Write "<br>"
	 'xmlStr= AESDecrypt(xmlStr, "testpage")
	 'response.Write xmlStr
	 'response.End
End IF
If filetype = "configxml" Then
    'filename="configxml.txt"
 
' Check for errors as a result of the division.
On Error Resume Next
    'Create an XML DOM object from the XML file
    Set objDOM  = CreateObject("Microsoft.XMLDOM")   
    objDOM.async = false   
    objDOM.load(Server.MapPath("xml\" & fileNameOrId))
    xmlStr = objDOM.xml
    'response.Write Server.MapPath("order.xml")
    'response.End
    'objDOM.loadXML(xmlStr)
    'xmlStr = objDOM.xml

    xmlStr= AESEncrypt(xmlStr, "testpage")
    'logError("Config xml Encrypted Result - " & xmlStr)
    response.Write xmlStr

    'xmlStr= AESDecrypt(xmlStr, "testpage")
    'logError("Config xml decrypted Result - " & xmlStr)

    If Err.Number <> 0 Then
    'logError("Error in Config.xml file. Err Description:" & Err.Description)
    End If
On Error GoTo 0
    'response.End
	 'response.Write "<br>"
	 ''xmlStr= AESDecrypt(xmlStr, "testpage")
	 'response.Write xmlStr
	 'response.End
End If

If filetype = "dId" then
    'filename="orderId.txt"
 On Error Resume Next
   xmlStr= AESDecrypt(fileNameOrId, "testpage")
    'logError("Order id encypted Result - " & xmlStr)
   response.Write xmlStr
    'xmlStr= AESDecrypt(xmlStr, "testpage")
    'logError("Order id decrypted Result - " & xmlStr)
    
    If Err.Number <> 0 Then
    'logError("Error in OrderId Encrypiton file. Err Description:" & Err.Description)
    End If
 On Error GoTo 0
end If


End if
'logError("*********End*******")
'logError("-----------------------------------------------------------------------------")
Public Function logError(erroString)

Dim objFSO, objFolder, objShell, objTextFile, objFile
Dim strDirectory, strFile, strText
strDirectory = "D:\SERENATAMOBILEWEBSITE\Serenataflowers\Encryption\ErroLog"
strFile = "\Summer.txt"
'strText = "Book Another Holiday"

' Create the File System Object
Set objFSO = CreateObject("Scripting.FileSystemObject")

' Check that the strDirectory folder exists
If objFSO.FolderExists(strDirectory) Then
   Set objFolder = objFSO.GetFolder(strDirectory)
Else
   Set objFolder = objFSO.CreateFolder(strDirectory)
   'WScript.Echo "Just created " & strDirectory
End If

If objFSO.FileExists(strDirectory & strFile) Then
   Set objFolder = objFSO.GetFolder(strDirectory)
Else
   Set objFile = objFSO.CreateTextFile(strDirectory & strFile)
   'Wscript.Echo "Just created " & strDirectory & strFile
End If

set objFile = nothing
set objFolder = nothing
' OpenTextFile Method needs a Const value
' ForAppending = 8 ForReading = 1, ForWriting = 2
Const ForAppending = 8

Set objTextFile = objFSO.OpenTextFile _
(strDirectory & strFile, ForAppending, True)

Dim logTime
logTime = Now
'logTime =Replace(logTime,":","")
'logTime = Replace(logTime,"/","")
' Writes strText every time you run this VBScript
objTextFile.WriteLine(logTime &" " &erroString)
objTextFile.Close



'dim fs,fo,tfile, filename
'filename = Now
'filename =Replace(filename,":","")
'filename = Replace(filename,"/","")
'Set fs=Server.CreateObject("Scripting.FileSystemObject")
'Set fo=fs.GetFolder(Server.MapPath("."))
'Set tfile=fo.CreateTextFile(filename& ".txt",true)
'tfile.WriteLine(erroString)
'tfile.Close
'set tfile=nothing
'set fo=nothing
'set fs=nothing
'set objDOM=nothing-->
End Function



'Display the original XML values
'txtOrig.value = objDOM.xml
'response.write(xmlStr)
'domainName = "serenataflowers.com"
'thawteCode = "GBSERE8"
'encryptedOrderID = AESEncrypt(idOrder, "testpage")

'configXML = getPaymentGatewayConfigXML(domainName, thawteCode, encryptedOrderID)
'configXML = AESEncrypt(configXML, "testpage")
%>