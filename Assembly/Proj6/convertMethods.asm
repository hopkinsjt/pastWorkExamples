;******************************************************************************************
;Program Name: convertMethods.asm
;Programmer:   Justin Hopkins
;Class:        CSCI 2160-001
;Date:         DEC,08 2017 at 09:00 AM
;Purpose:	  encryption and displaying hex values see method documentation for description
;	
;******************************************************************************************
	.486
	.model flat
	.data
	hexToCharacter proto near32 stdCall, lpDestination:dword,lpSource:dWord, numBytes:dword
	charTo4HexDigits proTO near32 stdCall, lpSourceString:dword
	encrypt32Bit proto near32 stdCall,lpSourceString:dword,dMask:dword,numBytes:dword
	memoryallocBailey proto near32 stdCall, numBytes:dword
	
	.code
COMMENT%
************************************************************************************
* name:  hexToCharacter															   *
* date Created: 11/30/2017 														   *
*Date Last Modefied: 12/08/2017                                                    *
* purpose: sets the length of a box object                                         *
*Notes on specifications,special algorithms, and assumptions: if the len property  *
*		  is less than or equal to zero no action is taken.						   *
* @param  lpDestination :dword Where the converted string is going                 *
* @param  lpSource:dword The value you wish to convert to ascii                    *
* @param  numBytes:dword the number of bytes to convert
* @Return Void                                                                     *
*                                                                                  *
************************************************************************************%
hexToCharacter proc near32 stdCall	uses eax ebx ecx edi edx esi, lpDestination:dword,lpSource:dWord, numBytes:dword
	local bAddressShift:byte		;local storage
	mov ebx,lpDestination			;moved to register to manipulate
	mov edx,lpSource				;moved to register to manipulate
	mov bAddressShift,28			;will be used to move through address
	mov esi,0						;will be used to mark position in string
	mov edi,0						;will be used to mark position in the source
	
	.if numBytes >0					;if the number of bytes specified is greater than 4
		mov ecx, numBytes			;loop through numBytes times
stLoop:								;build a null terminated string
		mov eax,0					;clear contents for shifting operations
		mov	al, byte ptr [edx+edi]  ;grab the next byte to convert
		shl ax,8					;shift to left high order of byte alone in AH
		shr ax,4				;shift to right to get low order of byte alone in top of al
		shr al,4				;shift to right to get low order of byte to bottom of al
		.if AH==0					;if AH is zero
			mov byte ptr[ebx+esi],30h		;move an ascii 0 into the string
		.elseif AH==1						;if AH is 1
			mov byte ptr[ebx+esi],31h		;move an ascii 1 into the string
		.elseif AH==2						;if AH is 2
			mov byte ptr[ebx+esi],32h		;move an ascii 2 into the string
		.elseif AH==3						;if AH is 3
			mov byte ptr[ebx+esi],33h		;move an ascii 3 into the string
		.elseif AH==4						;if AH is 4
			mov byte ptr[ebx+esi],34h		;move an ascii 4 into the string
		.elseif AH==5						;if AH is 5
			mov byte ptr[ebx+esi],35h		;move an ascii 5 into the string
		.elseif AH==6						;if AH is 6
			mov byte ptr[ebx+esi],36h		;move an ascii 6 into the string
		.elseif AH==7						;if AH is 7
			mov byte ptr[ebx+esi],37h		;move an ascii 7 into the string
		.elseif AH==8						;if AH is 8
			mov byte ptr[ebx+esi],38h		;move an ascii 8 into the string
		.elseif AH==9						;if AH is 9
			mov byte ptr[ebx+esi],39h		;move an ascii 9 into the string
		.elseif AH==10						;if AH is 10
			mov byte ptr[ebx+esi],041h		;move an ascii A into the string
		.elseif AH==11						;if AH is 11
			mov byte ptr[ebx+esi],042h		;move an ascii B into the string
		.elseif AH==12						;if AH is 12
			mov byte ptr[ebx+esi],043h		;move an ascii C into the string
		.elseif AH==13						;if AH is 13
			mov byte ptr[ebx+esi],044h		;move an ascii D into the string
		.elseif AH==14						;if AH is 14
			mov byte ptr[ebx+esi],045h		;move an ascii E into the string
		.else		 						;else AH is 15
			mov byte ptr[ebx+esi],046h		;move an ascii F into the string
		.endif								;end if AH==0
		inc esi								;go to next byte in the string
		.if AL==0							;if AL is zero
			mov byte ptr[ebx+esi],30h		;move an ascii 0 into the string
		.elseif AL==1						;if AL is 1
			mov byte ptr[ebx+esi],31h		;move an ascii 1 into the string
		.elseif AL==2						;if AL is 2
			mov byte ptr[ebx+esi],32h		;move an ascii 2 into the string
		.elseif AL==3						;if AL is 3
			mov byte ptr[ebx+esi],33h		;move an ascii 3 into the string
		.elseif AL==4						;if AL is 4
			mov byte ptr[ebx+esi],34h		;move an ascii 4 into the string
		.elseif AL==5						;if AL is 5
			mov byte ptr[ebx+esi],35h		;move an ascii 5 into the string
		.elseif AL==6						;if AL is 6
			mov byte ptr[ebx+esi],36h		;move an ascii 6 into the string
		.elseif AL==7						;if AL is 7
			mov byte ptr[ebx+esi],37h		;move an ascii 7 into the string
		.elseif AL==8						;if AL is 8
			mov byte ptr[ebx+esi],38h		;move an ascii 8 into the string
		.elseif AL==9						;if AL is 9
			mov byte ptr[ebx+esi],39h		;move an ascii 9 into the string
		.elseif AL==10						;if AL is 10
			mov byte ptr[ebx+esi],041h		;move an ascii A into the string
		.elseif AL==11						;if AL is 11
			mov byte ptr[ebx+esi],042h		;move an ascii B into the string
		.elseif AL==12						;if AL is 12
			mov byte ptr[ebx+esi],043h		;move an ascii C into the string
		.elseif AL==13						;if AL is 13
			mov byte ptr[ebx+esi],044h		;move an ascii D into the string
		.elseif AL==14						;if AL is 14
			mov byte ptr[ebx+esi],045h		;move an ascii E into the string
		.else		 						;else AL is 15
			mov byte ptr[ebx+esi],046h		;move an ascii F into the string
		.endif								;end if AL ==0
		inc esi								;go to next byte in the string
		inc edi								;go to next byte in the source
		dec ecx								;loop too great do manual loop with jumps
		cmp ecx,0							;if ecx is less than zero
		je next								;go to the next step
		jmp stLoop							;else loop
		.else								;else if numBytes ==0
		mov cl,bAddressShift				;placed in cl to shift right
		SHR edx,cl							;get the first byte of the address
		.if DL==0							;if DL is zero
			mov byte ptr[ebx+esi],30h		;move an ascii 0 into the string
		.elseif DL==1						;if DL is 1
			mov byte ptr[ebx+esi],31h		;move an ascii 1 into the string
		.elseif DL==2						;if DL is 2
			mov byte ptr[ebx+esi],32h		;move an ascii 2 into the string
		.elseif DL==3						;if DL is 3
			mov byte ptr[ebx+esi],33h		;move an ascii 3 into the string
		.elseif DL==4						;if DL is 4
			mov byte ptr[ebx+esi],34h		;move an ascii 4 into the string
		.elseif DL==5						;if DL is 5
			mov byte ptr[ebx+esi],35h		;move an ascii 5 into the string
		.elseif DL==6						;if DL is 6
			mov byte ptr[ebx+esi],36h		;move an ascii 6 into the string
		.elseif DL==7						;if DL is 7
			mov byte ptr[ebx+esi],37h		;move an ascii 7 into the string
		.elseif DL==8						;if DL is 8
			mov byte ptr[ebx+esi],38h		;move an ascii 8 into the string
		.elseif DL==9						;if DL is 9
			mov byte ptr[ebx+esi],39h		;move an ascii 9 into the string
		.elseif DL==10						;if DL is 10
			mov byte ptr[ebx+esi],041h		;move an ascii A into the string
		.elseif DL==11						;if DL is 11
			mov byte ptr[ebx+esi],042h		;move an ascii B into the string
		.elseif DL==12						;if DL is 12
			mov byte ptr[ebx+esi],043h		;move an ascii C into the string
		.elseif DL==13						;if DL is 13
			mov byte ptr[ebx+esi],044h		;move an ascii D into the string
		.elseif DL==14						;if DL is 14
			mov byte ptr[ebx+esi],045h		;move an ascii E into the string
		.else		 						;else DL is 15
			mov byte ptr[ebx+esi],046h		;move an ascii F into the string
		.endif
		inc esi						;get the next byte in the string
		sub bAddressShift,4			;will shift to the next byte
		mov edx,lpSource			;restore the address
address:
		mov cl,bAddressShift		;store the shift count in cl to perform shifting
		SHR edx,cl					;get the first byte of the address
		SHl dx,4				;shift to right to get low order of byte alone in top of dl
		shr dl,4				;shift to right to get low order of byte to bottom of dl
		.if DL==0							;if DL is zero
			mov byte ptr[ebx+esi],30h		;move an ascii 0 into the string
		.elseif DL==1						;if DL is 1
			mov byte ptr[ebx+esi],31h		;move an ascii 1 into the string
		.elseif DL==2						;if DL is 2
			mov byte ptr[ebx+esi],32h		;move an ascii 2 into the string
		.elseif DL==3						;if DL is 3
			mov byte ptr[ebx+esi],33h		;move an ascii 3 into the string
		.elseif DL==4						;if DL is 4
			mov byte ptr[ebx+esi],34h		;move an ascii 4 into the string
		.elseif DL==5						;if DL is 5
			mov byte ptr[ebx+esi],35h		;move an ascii 5 into the string
		.elseif DL==6						;if DL is 6
			mov byte ptr[ebx+esi],36h		;move an ascii 6 into the string
		.elseif DL==7						;if DL is 7
			mov byte ptr[ebx+esi],37h		;move an ascii 7 into the string
		.elseif DL==8						;if DL is 8
			mov byte ptr[ebx+esi],38h		;move an ascii 8 into the string
		.elseif DL==9						;if DL is 9
			mov byte ptr[ebx+esi],39h		;move an ascii 9 into the string
		.elseif DL==10						;if DL is 10
			mov byte ptr[ebx+esi],041h		;move an ascii A into the string
		.elseif DL==11						;if DL is 11
			mov byte ptr[ebx+esi],042h		;move an ascii B into the string
		.elseif DL==12						;if DL is 12
			mov byte ptr[ebx+esi],043h		;move an ascii C into the string
		.elseif DL==13						;if DL is 13
			mov byte ptr[ebx+esi],044h		;move an ascii D into the string
		.elseif DL==14						;if DL is 14
			mov byte ptr[ebx+esi],045h		;move an ascii E into the string
		.else		 						;else DL is 15
			mov byte ptr[ebx+esi],046h		;move an ascii F into the string
		.endif								;end if Dl ==0 
		sub bAddressShift,4					;get the next digit in the address
		inc esi								;go to next byte in the  returned string
		mov edx, lpSource					;restore edx
		cmp bAddressShift,0					;if the address shift is less than 0
		jl next								;building the string is complete
		jmp address							;else perform the next shift
	.endif									;end if numBytes > 0
next:										;handle exiting the method
	mov byte ptr[ebx+esi],00h				;null terminate the string
	RET										;exit the method
hexToCharacter endp							;end hexToCharacter


COMMENT%
************************************************************************************
* name:  charTo4HexDigits														   *
* date Created: 11/30/2017 														   *
*Date Last Modefied: 12/08/2017                                                    *
* purpose: take a string and return the equivalent dword mask                      *
*Notes on specifications,special algorithms, and assumptions: this method takes    *
*	the first 8 bytes of a valid null terminated  string it then goes through each *
*	byte individually and places the equivalent numeric value in the proper 	   *
*	position in a registerto be returned. a valid string has only 0-9 and a/A-f/F  * 
*																				   *
* @param  lpSourceString :dword address of a null terminated string                *
*								that will be converted into a dword mask		   *
* @Return eax:dword return the 4 byte mask                                         *
*                                                                                  *
************************************************************************************%
charTo4HexDigits proc near32 stdCall uses ebx ecx edi edx esi, lpSourceString:dword
	mov ebx,lpSourceString				;mov string into register to manipulate
	mov esi,eax							;store tle allocated memory in less v register
	mov edi,0							;will increment to navigate through the string
	mov ecx,8							;will only grab first 8 bytes of string
	mov edx,0							;clear out edx makes easier to see shifting 
stLoop:									;handle each byte in the string
	cmp ecx,1							;if 0 then the end of the mask has been filled
	je next								;mask is filled exit the method
	mov al,byte ptr [ebx+edi]			;grab the first byte of the string
	.if al == '0'						; al contains ascii 0
		shl edx,8						;mov lowest order nibble out of dl
		mov dl ,00h						;mov zero into edx
		shl dl,4						;get previous nibble and current nibble together
		shr edx,4						;reset the byte to al
	.elseif al =='1'					;if al has ascii 1
		shl edx,8						;mov lowest order nibble out of dl
		mov dl,01h						;mov a 1 into d register
		shl dl,4						;get previous nibble and current nibble together
		shr edx,4                          ;reset the byte to al
	.elseif al =='2'					;if al has ascii 2
		shl edx,8						;mov lowest order nibble out of dl
		mov dl,02h						;mov a 2 into d register
		shl dl,4						;get previous nibble and current nibble together
		shr edx,4                          ;reset the byte to al
	.elseif al =='3'					;if al has ascii 3
		shl edx,8						;mov lowest order nibble out of dl
		mov dl,03h						;mov a 3 into d register
		shl dl,4						;get previous nibble and current nibble together
		shr edx,4                          ;reset the byte to al
	.elseif al =='4'					;if al has ascii 4
		shl edx,8 						;mov lowest order nibble out of dl
		mov dl,04h						;mov a 4 into d register
		shl dl,4						;shift to the next position
		shr edx,4						;shift to the next position
	.elseif al =='5'					;if al has ascii 5
		shl edx,8 						;mov lowest order nibble out of dl
		mov dl,05h						;mov a 5 into d register
		shl dl,4						;get previous nibble and current nibble together
		shr edx,4                          ;reset the byte to al
	.elseif al =='6'					;if al has ascii 1
		shl edx,8 						;mov lowest order nibble out of dl
		mov dl,06h						;mov a 3 into d register
		shl dl,4						;get previous nibble and current nibble together
		shr edx,4                          ;reset the byte to al
	.elseif al =='7'					;if al has ascii 1
		shl edx,8 						;mov lowest order nibble out of dl
		mov dl,07h						;mov a 3 into d register
		shl dl,4						;get previous nibble and current nibble together
		shr edx,4                          ;reset the byte to al
	.elseif al =='8'					;if al has ascii 1
		shl edx,8 						;mov lowest order nibble out of dl
		mov dl,08h						;mov a 3 into d register
		shl dl,4						;get previous nibble and current nibble together
		shr edx,4                          ;reset the byte to al
	.elseif al =='9'					;if al has ascii 1
		shl edx,8 						;mov lowest order nibble out of dl
		mov dl,09h						;mov a 3 into d register
		shl dl,4						;get previous nibble and current nibble together
		shr edx,4                          ;reset the byte to al
	.elseif al =='A'||al =='a'			;if al has ascii 1
		shl edx,8 						;mov lowest order nibble out of dl
		mov dl,0Ah						;mov a 3 into d register
		shl dl,4						;get previous nibble and current nibble together
		shr edx,4                          ;reset the byte to al
	.elseif al =='B'|| al =='b'			;if al has ascii 1
		shl edx,8 						;mov lowest order nibble out of dl
		mov dl,0Bh						;mov a 3 into d register
		shl dl,4						;get previous nibble and current nibble together
		shr edx,4                          ;reset the byte to al
	.elseif al =='C'|| al =='c'			;if al has ascii 1
		shl edx,8 						;mov lowest order nibble out of dl
		mov dl,0Ch						;mov a 3 into d register
		shl dl,4						;get previous nibble and current nibble together
		shr edx,4                          ;reset the byte to al
	.elseif al =='D'|| al =='d'			;if al has ascii 1
		shl edx,8 						;mov lowest order nibble out of dl
		mov dl,0Dh						;mov a 3 into d register
		shl dl,4						;get previous nibble and current nibble together
		shr edx,4                          ;reset the byte to al
	.elseif al =='E'|| al =='e'			;if al has ascii 1
		shl edx,8 						;mov lowest order nibble out of dl
		mov dl,0Eh						;mov a 3 into d register
		shl dl,4						;get previous nibble and current nibble together
		shr edx,4                          ;reset the byte to al
	.elseif al =='F'|| al =='f'			;if al has ascii 1
		shl edx,8 						;mov lowest order nibble out of dl
		mov dl,0Fh						;mov a 3 into d register
		shl dl,4						;get previous nibble and current nibble together
		shr edx,4                          ;reset the byte to al
	.else								;else there was an invalid character
		mov eax,-1						; return -1
		jmp finish						;exit the method
	.endif								;end if al == '0'
	dec ecx								;get the bext byte
	inc edi								;next byte in the string 
	jmp stLoop							;test the next value
next:									;avoid shifting leading nibble out of Register
	shl edx,4							;put leading bit in highest position
	mov ax,dx							;preserve low order of mask
	mov cl,byte ptr [ebx+edi]			;grab the first byte of the string
	.if cl == '0'						; al contains ascii 0
		shl ax,4						;mov lowest order nibble out of dl
		mov al,00h						;mov a 1 into d register
		shl al,4						;get previous nibble and current nibble together
		shr ax,4                           ;reset the byte to al
	.elseif cl =='1'					;if al has ascii 1
		shl ax,4						;mov lowest order nibble out of dl
		mov al,01h						;mov a 1 into d register
		shl al,4						;get previous nibble and current nibble together
		shr ax,4                           ;reset the byte to al
	.elseif cl =='2'					;if al has ascii 2
		shl ax,4						;mov lowest order nibble out of dl
		mov al,02h						;mov a 2 into d register
		shl al,4						;get previous nibble and current nibble together
		shr ax,4                           ;reset the byte to al
	.elseif cl =='3'					;if al has ascii 3
		shl ax,4						;mov lowest order nibble out of dl
		mov al,03h						;mov a 3 into d register
		shl al,4						;get previous nibble and current nibble together
		shr ax,4                           ;reset the byte to al
	.elseif cl =='4'					;if al has ascii 4
		shl ax,4 						;mov lowest order nibble out of dl
		mov al,04h						;mov a 4 into d register
		shl al,4						;shift to the next position
		shr ax,4						;shift to the next position
	.elseif cl =='5'					;if al has ascii 5
		shl ax,4 						;mov lowest order nibble out of dl
		mov al,05h						;mov a 3 into d register
		shl al,4						;get previous nibble and current nibble together
		shr ax,4                           ;reset the byte to al
	.elseif cl =='6'					;if al has ascii 1
		shl ax,4 						;mov lowest order nibble out of dl
		mov al,06h						;mov a 3 into d register
		shl al,4						;get previous nibble and current nibble together
		shr ax,4                           ;reset the byte to al
	.elseif cl =='7'					;if al has ascii 1
		shl ax,4 						;mov lowest order nibble out of dl
		mov al,07h						;mov a 3 into d register
		shl al,4						;get previous nibble and current nibble together
		shr ax,4                           ;reset the byte to al
	.elseif cl =='8'					;if al has ascii 1
		shl ax,4 						;mov lowest order nibble out of dl
		mov al,08h						;mov a 3 into d register
		shl al,4						;get previous nibble and current nibble together
		shr ax,4                           ;reset the byte to al
	.elseif cl =='9'					;if al has ascii 1
		shl ax,4 						;mov lowest order nibble out of dl
		mov al,09h						;mov a 3 into d register
		shl al,4						;get previous nibble and current nibble together
		shr ax,4                           ;reset the byte to al
	.elseif cl =='A'||cl =='a'			;if cl has ascii 1
		shl ax,4 						;mov lowest order nibble out of dl
		mov al,0Ah						;mov a 3 into d register
		shl al,4						;get previous nibble and current nibble together
		shr ax,4                           ;reset the byte to al
	.elseif cl =='B'|| cl =='b'			;if al has ascii 1
		shl ax,4 						;mov lowest order nibble out of dl
		mov al,0Bh						;mov a 3 into d register
		shl al,4						;get previous nibble and current nibble together
		shr ax,4                           ;reset the byte to al
	.elseif cl =='C'|| cl =='c'			;if al has ascii 1
		shl ax,4 						;mov lowest order nibble out of dl
		mov al,0Ch						;mov a 3 into d register
		shl al,4						;get previous nibble and current nibble together
		shr ax,4                           ;reset the byte to al
	.elseif cl =='D'|| cl =='d'			;if al has ascii 1
		shl ax,4 						;mov lowest order nibble out of dl
		mov al,0Dh						;mov a 3 into d register
		shl al,4						;get previous nibble and current nibble together
		shr ax,4                           ;reset the byte to al
	.elseif cl =='E'|| cl =='e'			;if al has ascii 1
		shl ax,4 						;mov lowest order nibble out of dl
		mov al,0Eh						;mov a 3 into d register
		shl al,4						;get previous nibble and current nibble together
		shr ax,4                           ;reset the byte to al
	.elseif cl =='F'|| cl =='f'			;if al has ascii 1
		shl ax,4 						;mov lowest order nibble out of dl
		mov al,0Fh						;mov a 3 into d register
		shl al,4						;get previous nibble and current nibble together
		shr ax,4                           ;reset the byte to al
	.else								;else there was an invalid character
		mov eax,-1						; return -1
		jmp finish						;exit the method
	.endif								;end if cl==0
	mov dl,al							;restore the full mask
	mov eax,edx							;return the resultant mask
finish:									;exit the method
	RET									;exit the method
charTo4HexDigits endp					;end of charTo4HexDigits

COMMENT%
************************************************************************************
* name:  encrypt32Bit															   *
* date Created: 11/30/2017 														   *
*Date Last Modefied: 12/08/2017                                                    *
* purpose: tae astring and encrypt it using a mask			                       *
*Notes on specifications,special algorithms, and assumptions:   				   * 
*																				   *
* @param  lpSourceString :dword address of a null terminated string                *
*								that will be encrypted							   *
* @param dMask:dword a 4 byte mask that will be used to encrypt the string		   *	
* @Return eax:dword return the address of the new string                           *
*                                                                                  *
************************************************************************************%
encrypt32Bit proc near32 stdCall uses ebx ecx edi edx esi, lpSourceString:dword, 
dMask:dword, numBytes:dword
	local count:word, remainder:word		;local storage
	mov EBX,lpSourceString					;put the original string in register to encrypt
	mov eax,numBytes						;move to eax to prime for division
	cwd										;spread across dx:ax pair
	mov ecx,4								;prepare to divide by 4
	idiv cx									;divide the number of bytes by 4
	mov ecx,dMask							;will be used to exor later
	mov count,ax							;store the product for looping
	mov remainder,dx						;store the remainder for comparisons
	mov esi,numBytes						;store the number of bytes to allocate storage
	inc esi									;include null character
	INVOKE memoryallocBailey, esi			;allocate a block of storage to store encrypt
	mov edi,eax								;returned memory block in less volatile registe
	mov esi,0								;navigate through string
	mov eax,numBytes						;compare to handle the remaining bytes
	cmp eax,4								;if for or less
	jle  carry								;handle the remainder
	mov eax,0								;wipe eax
stLoop:										;perform encryption 4 bytes at a time
	cmp count,0								;if the count is zero
	je carry								;jump and handle
	
	shl eax,8								;shift the first byte
	mov al, byte ptr [ebx+esi]				;grab the first byte of the orig string
	inc esi									;get to the next byte
	
	shl eax,8								;shift the second byte
	mov al,byte ptr [ebx+esi]				;second byte from original string
	inc esi									;get the next byte
	
	shl eax,8								;shift the third byte
	mov al,byte ptr [ebx+esi]				;third byte from the original string 
	inc esi									;get the next byte
	
	shl eax,8								;shift the fourth byte
	mov al,byte ptr [ebx+esi]				;get the fourth byte
	inc esi									;increment to the next byte
	xor eax,ecx								;exclusive or the first 4 bytes with the mask
	ROL ax,8								;roll left 8
	ROL eax,16								;roll left 16
	ROL ax,8								;roll left 8
	mov dword ptr [edi+esi-4],eax			;save in mem the first four encrypted bytes
	dec  count								;reduce the loop count
	jmp stLoop 								;grab the next 4 bytes		
carry:										;handle the remainder not divisiable by 4
	cmp remainder,0							;if there is no remainder
	je done									;exit the method else handle the remaining byte
	shl eax,8								;shift over the first byte
	mov al,byte ptr[ebx+esi]				;store the first byte
	inc esi									;go to next byte of the string
	rol ecx, 8								;roll the shell to encryp on the correct byte
	xor al,cl								;encrypt a single byte
	mov byte ptr [edi+esi-1],al				;store a single byte
	dec remainder							;decrement the remainder and
	jmp carry								;jump to grab the next byte
done: 										;handle exiting the method
	mov eax,edi							    ;return the encrypted string
	RET										;exit the method
encrypt32Bit endp


	END