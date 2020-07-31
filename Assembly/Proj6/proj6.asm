;******************************************************************************************
;Program Name: proj6.asm
;Programmer:   Justin Hopkins
;Class:        CSCI 2160-001
;Date:         DEC,08 2017 at 09:00 AM
;Purpose:
;	a driver to test the ConvertMethods.asm
;******************************************************************************************

	.486
	.model flat
	.stack 100h
	
	ExitProcess PROTO Near32 stdcall, dwExitCode:dword
	String_length	proto Near32 stdcall, lpString:dword
	memoryallocBailey proto near32 stdCall, numBytes:dword
	intasc32Comma	proto Near32 stdcall, lpStringToHold:dword, dval:dword
	intasc32	PROTO Near32 stdcall, lpStringToHold:dword, dval:dword
	hexToChar PROTO Near32 stdcall,lpDestStr:dword,lpSourceStr:dword,dLen:dword
	putstring 	PROTO Near32 stdcall, lpStringToPrint:dword
	getstring	PROTO Near32 stdcall, lpStringToGet:dword, dlength:dword
	
	hexToCharacter proto near32 stdCall,lpDestination:dword,lpSource:dWord,numBytes:dword
	charTo4HexDigits proto near32 stdCall, lpSourceString:dword
	encrypt32Bit proto near32 stdCall,lpSourceString:dword,dMask:dword,numBytes:dword
	.data
strHeading	byte	10,13,10,13,9," Name: Justin Hopkins"
			byte	10,13,9,"Class: CSCI 2160-001"
			byte	10,13,9," Date: DEC,08 2017 at 09:00 AM"
			byte	10,13,9,"  Lab: Proj6",10,13,0
iMask		dWord	?			;will hold the dword mask
ihexTest    dword	1234ABCDh
strEnter	byte	10,13,9,"Enter 8-DigitMask: ",0
strMask		byte	10,13,9,"Mask: ",0
strPromptasc byte	10,13,9,"ASCII Chars to display hex version: ",0
strEnc	    byte	10,13,9, "String To Encrypt",0
strHexV		byte	10,13,9,"Hex Version: ",0
strEncV		byte	10,13,9,"Encrypted: ",0
strUnEnc	byte	10,13,9,"UnEncrypted: ",0
strConvBack byte	10,13,9,"onverted back To ascii: ",0
strInvalid  byte 	10,13,9,"You entered an invalid mask. Please try again.",0
strasciiEx  byte    "ABC 123 &$!",0
strArrow	byte	" --> ",0
lpHexTest	dword   ihexTest
bTest		byte    "1234ABCD",0				;test hexToChar
strGW		byte	"GeOrGe WaShInGtOn",0
strResult   byte	80 dup(?)				;hold the returned string
iResult2  dWord	?						;will hold a returned address
crlf		byte	10,13,0					;newLine

;************************************************************************************
; name:  puts strToPrint
; date: 4/7/2017
; purpose:  display a string to the user 
;************************************************************************************
puts	macro	strToPrint
	push ebx
	lea ebx,[strToPrint]
	INVOKE putstring, ebx
	pop ebx
	endm
;-------------------------------		
	.code	
_start: 
	mov eax,0								;dummy executable statement for debugging.
COMMENT%
************************************************************************************
 name:  main
 date: 12/08/2017
 purpose: driver for testing encryption and hexToCharacter methods 

************************************************************************************%	
main proc
	puts strHeading							;display programmer info
	puts crlf								;new line
maskValid:									;test to see if user entered a valid mask
	puts strEnter							;prompt user for input
	INVOKE getString, addr strResult,8		;get mask from user
	INVOKE charTo4HexDigits,addr strResult	;test and build the mask
	.if eax == 0FFFFFFFFh					;if eax is negative 1 the mask is invalid
		puts strInvalid						;prompt user to re enter the string
		jmp maskValid						;get the mask again
	.endif									;end if eax == -1
	mov iMask,eax							;store the mask
	puts strMask							;display "Mask: "
	puts strResult							;display the mask the user entered
	puts strPromptasc						;display prompt to user
	puts crlf								;new line
	puts strasciiEx							;display ABC 123 &$!
	puts strArrow							;display arrow
	Invoke hexToCharacter,addr strResult, addr strAsciiex, 12 ;convert to display asscii 
	puts strResult											  ;display the hex values
	puts strEnc												  ;display prompt to user
	puts strArrow											  ;display arrow
	puts strGW												  ;display george washington
	puts strHexV											  ;display hex Version
	Invoke hexToCharacter,addr strResult, addr strGW, 18      ;hex values for the string
	puts strResult											  ;display the hex values
	puts strEncV											  ;display encrypted
	INVOKE encrypt32Bit, addr strGW, iMask, 18 				  ;encryp the string
	mov iResult2,eax										  ;store the encrypted string
	INVOKE hexToCharacter,addr strResult,iResult2,18		  ;displayable encrypted sring
	puts strResult											  ;display the encrypted string
	puts crlf												  ;new line
	puts strUnEnc										   	  ;"UnEncrypted: "
	INVOKE encrypt32Bit, iresult2, iMask, 18				  ;decrypt the string
	mov iResult2,eax										  ;strore the returned string
	INVOKE hexToCharacter,addr strResult,iResult2,18		  ;hex values of the string
	puts strResult											  ;display unencrypted string
	puts strConvBack						                  ;"Converted back to ascii: "
	INVOKE putstring, iResult2								  ;display the string
	

	
	INVOKE ExitProcess,0					;terminate "normally" the program
	PUBLIC _start							;needed for proper assembly
main endp									;end of main procedure
	END										;The very LAST line in your program. 
	