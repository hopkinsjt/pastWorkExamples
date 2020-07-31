;******************************************************************************************
;Program Name: string.asm
;Programmer:   Justin Hopkins
;Class:        CSCI 2160-001
;Date:         11/9/2017
;Purpose:
;	a collection of methods to manipulate strings *See method documentation for 
;	a description of what each method does*
;******************************************************************************************
	.486
	.model flat
	String_length	proto Near32 stdcall, lpString:dword
	memoryallocBailey proto near32 stdCall, numBytes:dword
	String_copy 	proto near32 stdcall, lpString2:dword
	String_toUpperCase proto near32 stdCall, lpString3:dWord
	String_equals proto near32 stdCall  lpString4:dWord, lpString5:dWord
	String_equalsIgnoreCase proto near32 stdCall, lpString6:dWord, lpString7:dWord
	String_appends proto near32 stdCall, lpString8:dWord, lpString9:dWord
	.code

COMMENT%
************************************************************************************
 name:  String_length
 date: 11/9/2017
 purpose: counts the number of ascii characters in a given string
 @param  lpString:dword 	string you wish to count the length of
 @Return AX 	Returns the count in the AX register
************************************************************************************%	
String_length	proc Near32 stdcall uses ebx edi, lpString:dword
	mov ax,0							;intialize the register to 0
	mov edi,0							;intialize the register to 0
count:
	mov ebx, lpString					;address of string put in register for manipulation
	cmp byte ptr [ebx+edi],00h			;if null character hit exit method
	je finish							;exit method
	inc ax								;increase count of the size of the string
	inc edi								;grab the next byte 
	jmp count							;in the string
finish:
	Ret									;exit the method
String_length	endp


COMMENT%
************************************************************************************
 name:  String_copy
 date: 11/9/2017
 purpose: makes a copy of a string and returns the location of the copy
 @param  lpString2:dword 	address of the first byte of a string to copy
 @Return EAX 	Returns the address of the copy of the string
************************************************************************************%
String_copy	proc Near32 stdcall uses ebx edx ecx edi, lpString2:dword
	mov eax,0							;initialize the register to 0
	mov ebx,lpstring2					;address of string put in register for manipulation
	Invoke String_length, lpString2		;get the size of the string
	inc eax								;factor in the null character
	mov ecx,eax							;preserve in less volatile register
	Invoke memoryallocBailey, ecx		;get a new block of memory the size of the string 
	mov edx,eax							;preserve new string in less volatile register
	mov edi,0							;initialize for first byte in string
next:
	cmp edi,ecx							;if the end of string is hit exit the method
	je finish							;exit the method
	mov al,[ebx+edi]					;else grab a character
	mov byte ptr [edx+edi], AL			;and store it in the new string
	inc edi								;increment to the next character
	jmp next							;grab the next character
finish:
	mov eax, edx						;return the new string
	RET									;exit the method
String_copy 	endp

COMMENT%
************************************************************************************
 name:  String_toUpperCase
 date: 11/9/2017
 purpose: makes a copy of a given string and converts it to uppercase 
 @param  lpString3:dword 	addres of the first byte of a string to convert to uppercase
 @Return EAX 	Returns the location of the new uppercase string
************************************************************************************%
String_toUpperCase proc near32 stdCall uses ebx ecx edi, lpString3:dWord
	mov ebx, lpString3					;address of string put in register for manipulation
	Invoke String_copy, lpString3		;first make copy of the string
	mov ecx, eax						;store the copy volatile register
	mov edi,0							;initalize to zero to grab first character
next:
	cmp byte ptr [ecx+edi],0			;if null character hit 
	je finish							;exit method
	.if byte ptr [ecx+edi] >= 61h && byte ptr [ecx+edi] <= 7ah	;if a lowercase char
		mov al, byte ptr[ecx+edi]		;put the char in register
		sub al,20h						;sub 20 hex to make it a capital letter
		mov byte ptr [ecx+edi],al		;place the new capitalized character back in string
		inc edi							;increment to the next character
		jmp next						;test the next character
	.else								;otherwise already either capitalized or other
		inc edi							;increment to the next character
		jmp next						;test the next character
	.endif
finish:
	mov eax,ecx							;return the new capitalized string
	Ret									;exit the method
String_toUpperCase endp

COMMENT%
************************************************************************************
 name:  String_equals
 date: 11/9/2017
 purpose: compares two strings to see if they are equal
 @param  lpString4:dword 	addres of the first byte of the first String
 @param  lpString5:dWord	address of the first byte of the second string
 @Return al 	Returns 1 if they are equal and returns 0 if they are not 
************************************************************************************%
String_equals proc near32 stdCall uses ebx ecx edi edx, lpString4:dWord, lpString5:dWord

	Invoke String_length, lpString4		;get the size of the first string
	mov dx,ax							;preserve in less volatile register
	Invoke String_length, lpString5		;get size of second string
	.if ax != dx						;if not the same length we no they can't be equal
		mov al,0						;set al to false
		jmp finish						;exit the method
	.endif
	mov al,1							;initialize to true
	mov ebx,lpString4					;store first string for manipulation
	mov ecx, lpString5					;store second string for manipulation
	mov edi,0							;initialize to the first byte in the string
next:
	cmp byte ptr [ebx+edi],0			;if null character hit 
	je finish							;exit the method
	mov dl,byte ptr[ebx+edi]			;grab the character from string 1
	.if dl== byte ptr[ecx+edi]			;if equal to character in string 2	
		inc edi							;move to next character
		jmp next						;test the next character
	.else								;else if they weren't equal
		mov al,0						;return false
	.endif
finish:
	RET									;exit the method
String_equals endp

COMMENT%
************************************************************************************
 name:  String_equalsIgnoreCase
 date: 11/9/2017
 purpose: compares two strings to see if they are equal and ignores the case
 @param  lpString4:dword 	addres of the first byte of the first String
 @param  lpString5:dWord	address of the first byte of the second string
 @Return al 	Returns 1 if they are equal and returns 0 if they are not 
************************************************************************************%
String_equalsIgnoreCase proc near32 stdCall uses ebx ecx edi edx , lpString6:dWord, lpString7:dWord
	Invoke String_toUpperCase, lpString6	;first convert 
	mov ebx,eax								;both strings 
	Invoke String_toUpperCase, lpString7	;to
	mov ecx,eax								;uppercase
	Invoke String_equals, ebx,ecx			;then test and see if they are equal in uppercase
											;result returned in al
	RET										;exit the method	
String_equalsIgnoreCase endp
Comment%
************************************************************************************
 name:  String_appends
 date: 11/9/2017
 purpose: takes the first string and appends it to the second to make one string
 @param  lpString4:dword 	addres of the first byte of the first String
 @param  lpString5:dWord	address of the first byte of the second string
 @Return EAX 	Returns the address of the first byte of the new appended string 
************************************************************************************%
String_appends proc near32 stdCall uses ebx ecx edi edx esi, lpString8:dWord, lpString9:dWord
	Invoke String_length, lpString8	;GET THE size of the first string
	mov cx,ax						;preserve the size of the first string
	Invoke String_length, lpString9	;get the size of the 2nd string 
	add cx,ax						;add the size of the 2nd string to the size of the 1st 
	inc cx							;add the null character 
	Invoke memoryallocBailey,cx		;get a block of memory the size of lpString6 +lpString7
	mov ecx, eax					;store that size in a less fragile register
	mov ebx,lpString8				;put the first string in a register for manipulation
	mov edx,lpString9				;second string in register for manipulation
	mov esi,0						;initialize to access bytes of the string
	mov edi,0						;initialize to store bytes of the string
firstStr:
	cmp byte ptr [ebx+esi],0		;see if the null character has been hit 
	je secondStr					;if it has skip over it and start storing the secondstr
	mov al, byte ptr [ebx+esi]		;place the first character in register to srore in string
	mov byte ptr [ecx+edi],al		;store char from first string in the appended string
	inc esi							;grab the next char from the first string
	inc edi							;next position in the appended string
	jmp firstStr					;handle the next character
secondStr:			
	mov esi,0						;reset to the first byte of the second string
secondStr2:
	mov al,byte ptr [edx+esi]		;grab byte from second string 	
	mov byte ptr[ecx+edi],al		;place it in the appended string
	cmp byte ptr [edx+esi],0		;if the null character was hit 
	je Finish						;exit the method
	inc esi							;else 
	inc edi							;grab the
	jmp secondStr2					;next byte
finish: 
	mov eax , ecx					;return the location of the appended string
	RET								;exit the method	
String_appends endp

END									;The very LAST line in your program. Terminate assembly