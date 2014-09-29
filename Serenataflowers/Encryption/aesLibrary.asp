<% 
'Option Explicit
' Rijndael.asp
' Copyright 2001 Phil Fresle 
' phil@frez.co.uk 
' http://www.frez.co.uk
' Implementation of the AES Rijndael Block Cipher. Inspired by Mike Scott's
' implementation in C. Permission for free direct or derivative use is granted
' subject to compliance with any conditions that the originators of the
' algorithm place on its exploitation.
' 3-Apr-2001: Functions added to the bottom for encrypting/decrypting large
' arrays of data. The entire length of the array is inserted as the first four
' bytes onto the front of the first block of the resultant byte array before
' encryption.
' 19-Apr-2001: Thanks to Paolo Migliaccio for finding a bug with 256 bit 
' key. Problem was in the aesgkey function. Now properly matches NIST values.

Private aes_lOnBits(30)
Private aes_l2Power(30)
Private aes_bytOnBits(7)
Private aes_byt2Power(7)

Private aes_InCo(3)

Private aes_fbsub(255)
Private aes_rbsub(255)
Private aes_ptab(255)
Private aes_ltab(255)
Private aes_ftable(255)
Private aes_rtable(255)
Private aes_rco(29)

Private aes_Nk
Private aes_Nb
Private aes_Nr
Private aes_fi(23)
Private aes_ri(23)
Private aes_fkey(119)
Private aes_rkey(119)

aes_InCo(0) = &HB
aes_InCo(1) = &HD
aes_InCo(2) = &H9
aes_InCo(3) = &HE
    
aes_bytOnBits(0) = 1
aes_bytOnBits(1) = 3
aes_bytOnBits(2) = 7
aes_bytOnBits(3) = 15
aes_bytOnBits(4) = 31
aes_bytOnBits(5) = 63
aes_bytOnBits(6) = 127
aes_bytOnBits(7) = 255
    
aes_byt2Power(0) = 1
aes_byt2Power(1) = 2
aes_byt2Power(2) = 4
aes_byt2Power(3) = 8
aes_byt2Power(4) = 16
aes_byt2Power(5) = 32
aes_byt2Power(6) = 64
aes_byt2Power(7) = 128
    
aes_lOnBits(0) = 1
aes_lOnBits(1) = 3
aes_lOnBits(2) = 7
aes_lOnBits(3) = 15
aes_lOnBits(4) = 31
aes_lOnBits(5) = 63
aes_lOnBits(6) = 127
aes_lOnBits(7) = 255
aes_lOnBits(8) = 511
aes_lOnBits(9) = 1023
aes_lOnBits(10) = 2047
aes_lOnBits(11) = 4095
aes_lOnBits(12) = 8191
aes_lOnBits(13) = 16383
aes_lOnBits(14) = 32767
aes_lOnBits(15) = 65535
aes_lOnBits(16) = 131071
aes_lOnBits(17) = 262143
aes_lOnBits(18) = 524287
aes_lOnBits(19) = 1048575
aes_lOnBits(20) = 2097151
aes_lOnBits(21) = 4194303
aes_lOnBits(22) = 8388607
aes_lOnBits(23) = 16777215
aes_lOnBits(24) = 33554431
aes_lOnBits(25) = 67108863
aes_lOnBits(26) = 134217727
aes_lOnBits(27) = 268435455
aes_lOnBits(28) = 536870911
aes_lOnBits(29) = 1073741823
aes_lOnBits(30) = 2147483647
    
aes_l2Power(0) = 1
aes_l2Power(1) = 2
aes_l2Power(2) = 4
aes_l2Power(3) = 8
aes_l2Power(4) = 16
aes_l2Power(5) = 32
aes_l2Power(6) = 64
aes_l2Power(7) = 128
aes_l2Power(8) = 256
aes_l2Power(9) = 512
aes_l2Power(10) = 1024
aes_l2Power(11) = 2048
aes_l2Power(12) = 4096
aes_l2Power(13) = 8192
aes_l2Power(14) = 16384
aes_l2Power(15) = 32768
aes_l2Power(16) = 65536
aes_l2Power(17) = 131072
aes_l2Power(18) = 262144
aes_l2Power(19) = 524288
aes_l2Power(20) = 1048576
aes_l2Power(21) = 2097152
aes_l2Power(22) = 4194304
aes_l2Power(23) = 8388608
aes_l2Power(24) = 16777216
aes_l2Power(25) = 33554432
aes_l2Power(26) = 67108864
aes_l2Power(27) = 134217728
aes_l2Power(28) = 268435456
aes_l2Power(29) = 536870912
aes_l2Power(30) = 1073741824

Private Function aesLShift(lValue, iShiftBits)
    If iShiftBits = 0 Then
        aesLShift = lValue
        Exit Function
    ElseIf iShiftBits = 31 Then
        If lValue And 1 Then
            aesLShift = &H80000000
        Else
            aesLShift = 0
        End If
        Exit Function
    ElseIf iShiftBits < 0 Or iShiftBits > 31 Then
        Err.Raise 6
    End If
    
    If (lValue And aes_l2Power(31 - iShiftBits)) Then
        aesLShift = ((lValue And aes_lOnBits(31 - (iShiftBits + 1))) * aes_l2Power(iShiftBits)) Or &H80000000
    Else
        aesLShift = ((lValue And aes_lOnBits(31 - iShiftBits)) * aes_l2Power(iShiftBits))
    End If
End Function

Private Function aesRShift(lValue, iShiftBits)
    If iShiftBits = 0 Then
        aesRShift = lValue
        Exit Function
    ElseIf iShiftBits = 31 Then
        If lValue And &H80000000 Then
            aesRShift = 1
        Else
            aesRShift = 0
        End If
        Exit Function
    ElseIf iShiftBits < 0 Or iShiftBits > 31 Then
        Err.Raise 6
    End If
    
    aesRShift = (lValue And &H7FFFFFFE) \ aes_l2Power(iShiftBits)
    
    If (lValue And &H80000000) Then
        aesRShift = (aesRShift Or (&H40000000 \ aes_l2Power(iShiftBits - 1)))
    End If
End Function

Private Function aesLShiftByte(bytValue, bytShiftBits)
    If bytShiftBits = 0 Then
        aesLShiftByte = bytValue
        Exit Function
    ElseIf bytShiftBits = 7 Then
        If bytValue And 1 Then
            aesLShiftByte = &H80
        Else
            aesLShiftByte = 0
        End If
        Exit Function
    ElseIf bytShiftBits < 0 Or bytShiftBits > 7 Then
        Err.Raise 6
    End If
    
    aesLShiftByte = ((bytValue And aes_bytOnBits(7 - bytShiftBits)) * aes_byt2Power(bytShiftBits))
End Function

Private Function aesRShiftByte(bytValue, bytShiftBits)
    If bytShiftBits = 0 Then
        aesRShiftByte = bytValue
        Exit Function
    ElseIf bytShiftBits = 7 Then
        If bytValue And &H80 Then
            aesRShiftByte = 1
        Else
            aesRShiftByte = 0
        End If
        Exit Function
    ElseIf bytShiftBits < 0 Or bytShiftBits > 7 Then
        Err.Raise 6
    End If
    
    aesRShiftByte = bytValue \ aes_byt2Power(bytShiftBits)
End Function

Private Function aesRotateLeft(lValue, iShiftBits)
    aesRotateLeft = aesLShift(lValue, iShiftBits) Or aesRShift(lValue, (32 - iShiftBits))
End Function

Private Function aesRotateLeftByte(bytValue, bytShiftBits)
    aesRotateLeftByte = aesLShiftByte(bytValue, bytShiftBits) Or aesRShiftByte(bytValue, (8 - bytShiftBits))
End Function

Private Function easPack(b())
    Dim lCount
    Dim lTemp
    
    For lCount = 0 To 3
        lTemp = b(lCount)
        easPack = easPack Or aesLShift(lTemp, (lCount * 8))
    Next
End Function

Private Function easPackFrom(b(), k)
    Dim lCount
    Dim lTemp
    
    For lCount = 0 To 3
        lTemp = b(lCount + k)
        easPackFrom = easPackFrom Or aesLShift(lTemp, (lCount * 8))
    Next
End Function

Private Sub UneasPack(a, b())
    b(0) = a And aes_lOnBits(7)
    b(1) = aesRShift(a, 8) And aes_lOnBits(7)
    b(2) = aesRShift(a, 16) And aes_lOnBits(7)
    b(3) = aesRShift(a, 24) And aes_lOnBits(7)
End Sub

Private Sub UneasPackFrom(a, b(), k)
    b(0 + k) = a And aes_lOnBits(7)
    b(1 + k) = aesRShift(a, 8) And aes_lOnBits(7)
    b(2 + k) = aesRShift(a, 16) And aes_lOnBits(7)
    b(3 + k) = aesRShift(a, 24) And aes_lOnBits(7)
End Sub

Private Function aesxtime(a)
    Dim b
    
    If (a And &H80) Then
        b = &H1B
    Else
        b = 0
    End If
    
    aesxtime = aesLShiftByte(a, 1)
    aesxtime = aesxtime Xor b
End Function

Private Function aesbmul(x, y)
    If x <> 0 And y <> 0 Then
        aesbmul = aes_ptab((CLng(aes_ltab(x)) + CLng(aes_ltab(y))) Mod 255)
    Else
        aesbmul = 0
    End If
End Function

Private Function aesSubByte(a)
    Dim b(3)
    
    UneasPack a, b
    b(0) = aes_fbsub(b(0))
    b(1) = aes_fbsub(b(1))
    b(2) = aes_fbsub(b(2))
    b(3) = aes_fbsub(b(3))
    
    aesSubByte = easPack(b)
End Function

Private Function aesproduct(x, y)
    Dim xb(3)
    Dim yb(3)
    
    UneasPack x, xb
    UneasPack y, yb
    aesproduct = aesbmul(xb(0), yb(0)) Xor aesbmul(xb(1), yb(1)) Xor aesbmul(xb(2), yb(2)) Xor aesbmul(xb(3), yb(3))
End Function

Private Function aesInvMixCol(x)
    Dim y
    Dim m
    Dim b(3)
    
    m = easPack(aes_InCo)
    b(3) = aesproduct(m, x)
    m = aesRotateLeft(m, 24)
    b(2) = aesproduct(m, x)
    m = aesRotateLeft(m, 24)
    b(1) = aesproduct(m, x)
    m = aesRotateLeft(m, 24)
    b(0) = aesproduct(m, x)
    y = easPack(b)
    
    aesInvMixCol = y
End Function

Private Function aesByteSub(x)
    Dim y
    Dim z
    
    z = x
    y = aes_ptab(255 - aes_ltab(z))
    z = y
    z = aesRotateLeftByte(z, 1)
    y = y Xor z
    z = aesRotateLeftByte(z, 1)
    y = y Xor z
    z = aesRotateLeftByte(z, 1)
    y = y Xor z
    z = aesRotateLeftByte(z, 1)
    y = y Xor z
    y = y Xor &H63
    
    aesByteSub = y
End Function

Public Sub aesgentables()
    Dim i
    Dim y
    Dim b(3)
    Dim ib
    
    aes_ltab(0) = 0
    aes_ptab(0) = 1
    aes_ltab(1) = 0
    aes_ptab(1) = 3
    aes_ltab(3) = 1
    
    For i = 2 To 255
        aes_ptab(i) = aes_ptab(i - 1) Xor aesxtime(aes_ptab(i - 1))
        aes_ltab(aes_ptab(i)) = i
    Next
    
    aes_fbsub(0) = &H63
    aes_rbsub(&H63) = 0
    
    For i = 1 To 255
        ib = i
        y = aesByteSub(ib)
        aes_fbsub(i) = y
        aes_rbsub(y) = i
    Next
    
    y = 1
    For i = 0 To 29
        aes_rco(i) = y
        y = aesxtime(y)
    Next
    
    For i = 0 To 255
        y = aes_fbsub(i)
        b(3) = y Xor aesxtime(y)
        b(2) = y
        b(1) = y
        b(0) = aesxtime(y)
        aes_ftable(i) = easPack(b)
        
        y = aes_rbsub(i)
        b(3) = aesbmul(aes_InCo(0), y)
        b(2) = aesbmul(aes_InCo(1), y)
        b(1) = aesbmul(aes_InCo(2), y)
        b(0) = aesbmul(aes_InCo(3), y)
        aes_rtable(i) = easPack(b)
    Next
End Sub

Public Sub aesgkey(nb, nk, key())                
    Dim i
    Dim j
    Dim k
    Dim m
    Dim N
    Dim C1
    Dim C2
    Dim C3
    Dim CipherKey(7)
    
    aes_Nb = nb
    aes_Nk = nk
    
    If aes_Nb >= aes_Nk Then
        aes_Nr = 6 + aes_Nb
    Else
        aes_Nr = 6 + aes_Nk
    End If
    
    C1 = 1
    If aes_Nb < 8 Then
        C2 = 2
        C3 = 3
    Else
        C2 = 3
        C3 = 4
    End If
    
    For j = 0 To nb - 1
        m = j * 3
        
        aes_fi(m) = (j + C1) Mod nb
        aes_fi(m + 1) = (j + C2) Mod nb
        aes_fi(m + 2) = (j + C3) Mod nb
        aes_ri(m) = (nb + j - C1) Mod nb
        aes_ri(m + 1) = (nb + j - C2) Mod nb
        aes_ri(m + 2) = (nb + j - C3) Mod nb
    Next
    
    N = aes_Nb * (aes_Nr + 1)
    
    For i = 0 To aes_Nk - 1
        j = i * 4
        CipherKey(i) = easPackFrom(key, j)
    Next
    
    For i = 0 To aes_Nk - 1
        aes_fkey(i) = CipherKey(i)
    Next
    
    j = aes_Nk
    k = 0
    Do While j < N
        aes_fkey(j) = aes_fkey(j - aes_Nk) Xor _
            aesSubByte(aesRotateLeft(aes_fkey(j - 1), 24)) Xor aes_rco(k)
        If aes_Nk <= 6 Then
            i = 1
            Do While i < aes_Nk And (i + j) < N
                aes_fkey(i + j) = aes_fkey(i + j - aes_Nk) Xor _
                    aes_fkey(i + j - 1)
                i = i + 1
            Loop
        Else
            i = 1
            Do While i < 4 And (i + j) < N
                aes_fkey(i + j) = aes_fkey(i + j - aes_Nk) Xor _
                    aes_fkey(i + j - 1)
                i = i + 1
            Loop
            If j + 4 < N Then
                aes_fkey(j + 4) = aes_fkey(j + 4 - aes_Nk) Xor _
                    aesSubByte(aes_fkey(j + 3))
            End If
            i = 5
            Do While i < aes_Nk And (i + j) < N
                aes_fkey(i + j) = aes_fkey(i + j - aes_Nk) Xor _
                    aes_fkey(i + j - 1)
                i = i + 1
            Loop
        End If
        
        j = j + aes_Nk
        k = k + 1
    Loop
    
    For j = 0 To aes_Nb - 1
        aes_rkey(j + N - nb) = aes_fkey(j)
    Next
    
    i = aes_Nb
    Do While i < N - aes_Nb
        k = N - aes_Nb - i
        For j = 0 To aes_Nb - 1
            aes_rkey(k + j) = aesInvMixCol(aes_fkey(i + j))
        Next
        i = i + aes_Nb
    Loop
    
    j = N - aes_Nb
    Do While j < N
        aes_rkey(j - N + aes_Nb) = aes_fkey(j)
        j = j + 1
    Loop
End Sub

Public Sub aes_encrypt(buff())
    Dim i
    Dim j
    Dim k
    Dim m
    Dim a(7)
    Dim b(7)
    Dim x
    Dim y
    Dim t
    
    For i = 0 To aes_Nb - 1
        j = i * 4
        
        a(i) = easPackFrom(buff, j)
        a(i) = a(i) Xor aes_fkey(i)
    Next
    
    k = aes_Nb
    x = a
    y = b
    
    For i = 1 To aes_Nr - 1
        For j = 0 To aes_Nb - 1
            m = j * 3
            y(j) = aes_fkey(k) Xor aes_ftable(x(j) And aes_lOnBits(7)) Xor _
                aesRotateLeft(aes_ftable(aesRShift(x(aes_fi(m)), 8) And aes_lOnBits(7)), 8) Xor _
                aesRotateLeft(aes_ftable(aesRShift(x(aes_fi(m + 1)), 16) And aes_lOnBits(7)), 16) Xor _
                aesRotateLeft(aes_ftable(aesRShift(x(aes_fi(m + 2)), 24) And aes_lOnBits(7)), 24)
            k = k + 1
        Next
        t = x
        x = y
        y = t
    Next
    
    For j = 0 To aes_Nb - 1
        m = j * 3
        y(j) = aes_fkey(k) Xor aes_fbsub(x(j) And aes_lOnBits(7)) Xor _
            aesRotateLeft(aes_fbsub(aesRShift(x(aes_fi(m)), 8) And aes_lOnBits(7)), 8) Xor _
            aesRotateLeft(aes_fbsub(aesRShift(x(aes_fi(m + 1)), 16) And aes_lOnBits(7)), 16) Xor _
            aesRotateLeft(aes_fbsub(aesRShift(x(aes_fi(m + 2)), 24) And aes_lOnBits(7)), 24)
        k = k + 1
    Next
    
    For i = 0 To aes_Nb - 1
        j = i * 4
        UneasPackFrom y(i), buff, j
        x(i) = 0
        y(i) = 0
    Next
End Sub

Public Sub aes_decrypt(buff())
    Dim i
    Dim j
    Dim k
    Dim m
    Dim a(7)
    Dim b(7)
    Dim x
    Dim y
    Dim t
    
    For i = 0 To aes_Nb - 1
        j = i * 4
        a(i) = easPackFrom(buff, j)
        a(i) = a(i) Xor aes_rkey(i)
    Next
    
    k = aes_Nb
    x = a
    y = b
    
    For i = 1 To aes_Nr - 1
        For j = 0 To aes_Nb - 1
            m = j * 3
            y(j) = aes_rkey(k) Xor aes_rtable(x(j) And aes_lOnBits(7)) Xor _
                aesRotateLeft(aes_rtable(aesRShift(x(aes_ri(m)), 8) And aes_lOnBits(7)), 8) Xor _
                aesRotateLeft(aes_rtable(aesRShift(x(aes_ri(m + 1)), 16) And aes_lOnBits(7)), 16) Xor _
                aesRotateLeft(aes_rtable(aesRShift(x(aes_ri(m + 2)), 24) And aes_lOnBits(7)), 24)
            k = k + 1
        Next
        t = x
        x = y
        y = t
    Next
    
    For j = 0 To aes_Nb - 1
        m = j * 3
        
        y(j) = aes_rkey(k) Xor aes_rbsub(x(j) And aes_lOnBits(7)) Xor _
            aesRotateLeft(aes_rbsub(aesRShift(x(aes_ri(m)), 8) And aes_lOnBits(7)), 8) Xor _
            aesRotateLeft(aes_rbsub(aesRShift(x(aes_ri(m + 1)), 16) And aes_lOnBits(7)), 16) Xor _
            aesRotateLeft(aes_rbsub(aesRShift(x(aes_ri(m + 2)), 24) And aes_lOnBits(7)), 24)
        k = k + 1
    Next
    
    For i = 0 To aes_Nb - 1
        j = i * 4
        
        UneasPackFrom y(i), buff, j
        x(i) = 0
        y(i) = 0
    Next
End Sub

Private Function IsInitialized(vArray)
    On Error Resume Next
    
    IsInitialized = IsNumeric(UBound(vArray))
End Function

Private Sub CopyBytesASP(bytDest, lDestStart, bytSource(), lSourceStart, lLength)
    Dim lCount
    
    lCount = 0
    Do
        bytDest(lDestStart + lCount) = bytSource(lSourceStart + lCount)
        lCount = lCount + 1
    Loop Until lCount = lLength
End Sub

Public Function aes_encryptData(bytMessage, bytPassword)
    Dim bytKey(31)
    Dim bytIn()
    Dim bytOut()
    Dim bytTemp(31)
    Dim lCount
    Dim lLength
    Dim lEncodedLength
    Dim bytLen(3)
    Dim lPosition
    
    If Not IsInitialized(bytMessage) Then
        Exit Function
    End If
    If Not IsInitialized(bytPassword) Then
        Exit Function
    End If
    
    For lCount = 0 To UBound(bytPassword)
        bytKey(lCount) = bytPassword(lCount)
        If lCount = 31 Then
            Exit For
        End If
    Next
    
    aesgentables
    aesgkey 8, 8, bytKey
    
    lLength = UBound(bytMessage) + 1
    lEncodedLength = lLength + 4
    
    If lEncodedLength Mod 32 <> 0 Then
        lEncodedLength = lEncodedLength + 32 - (lEncodedLength Mod 32)
    End If
    ReDim bytIn(lEncodedLength - 1)
    ReDim bytOut(lEncodedLength - 1)
    
    UneasPack lLength, bytIn
    CopyBytesASP bytIn, 4, bytMessage, 0, lLength

    For lCount = 0 To lEncodedLength - 1 Step 32
        CopyBytesASP bytTemp, 0, bytIn, lCount, 32
        aes_encrypt bytTemp
        CopyBytesASP bytOut, lCount, bytTemp, 0, 32
    Next
    
    aes_encryptData = bytOut
End Function

Public Function aes_decryptData(bytIn, bytPassword)
    Dim bytMessage()
    Dim bytKey(31)
    Dim bytOut()
    Dim bytTemp(31)
    Dim lCount
    Dim lLength
    Dim lEncodedLength
    Dim bytLen(3)
    Dim lPosition
    
    If Not IsInitialized(bytIn) Then
        Exit Function
    End If
    If Not IsInitialized(bytPassword) Then
        Exit Function
    End If
    
    lEncodedLength = UBound(bytIn) + 1
    
    If lEncodedLength Mod 32 <> 0 Then
        Exit Function
    End If
    
    For lCount = 0 To UBound(bytPassword)
        bytKey(lCount) = bytPassword(lCount)
        If lCount = 31 Then
            Exit For
        End If
    Next
    
    aesgentables
    aesgkey 8, 8, bytKey

    ReDim bytOut(lEncodedLength - 1)
    
    For lCount = 0 To lEncodedLength - 1 Step 32
        CopyBytesASP bytTemp, 0, bytIn, lCount, 32
        aes_decrypt bytTemp
        CopyBytesASP bytOut, lCount, bytTemp, 0, 32
    Next

    lLength = easPack(bytOut)
    
    If lLength > lEncodedLength - 4 Then
        Exit Function
    End If
    
    ReDim bytMessage(lLength - 1)
    CopyBytesASP bytMessage, 0, bytOut, 4, lLength
    
    aes_decryptData = bytMessage
End Function

Function AESEncrypt(sPlain, sPassword)
    Dim bytIn()
    Dim bytOut
    Dim bytPassword()
    Dim lCount
    Dim lLength
	Dim sTemp
	
    lLength = Len(sPlain)
    ReDim bytIn(lLength-1)
    For lCount = 1 To lLength
        bytIn(lCount-1) = CByte(AscB(Mid(sPlain,lCount,1)))
    Next
    lLength = Len(sPassword)
    ReDim bytPassword(lLength-1)
    For lCount = 1 To lLength
        bytPassword(lCount-1) = CByte(AscB(Mid(sPassword,lCount,1)))
    Next

    bytOut = aes_encryptData(bytIn, bytPassword)

    sTemp = ""
    For lCount = 0 To UBound(bytOut)
        sTemp = sTemp & Right("0" & Hex(bytOut(lCount)), 2)
    Next

    AESEncrypt = sTemp
End Function

Function AESDecrypt(sCypher, sPassword)
    Dim bytIn()
    Dim bytOut
    Dim bytPassword()
    Dim lCount
    Dim lLength
	Dim sTemp
	
    lLength = Len(sCypher)
    ReDim bytIn(lLength/2-1)
    For lCount = 0 To lLength/2-1
        bytIn(lCount) = CByte("&H" & Mid(sCypher,lCount*2+1,2))
    Next
    lLength = Len(sPassword)
    ReDim bytPassword(lLength-1)
    For lCount = 1 To lLength
        bytPassword(lCount-1) = CByte(AscB(Mid(sPassword,lCount,1)))
    Next

    bytOut = aes_DecryptData(bytIn, bytPassword)

    lLength = UBound(bytOut) + 1
    sTemp = ""
    For lCount = 0 To lLength - 1
        sTemp = sTemp & Chr(bytOut(lCount))
    Next

    AESDecrypt = sTemp
End Function
%>