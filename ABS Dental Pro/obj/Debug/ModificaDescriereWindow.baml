   M S B A M L   `   `   `  ����I  EABS Dental Pro, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:ABS_Dental_ProABS_Dental_Pro  5   	   ,    &ABS_Dental_Pro.ModificaDescriereWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:ABS_Dental_Pro     5   	     � Title$+  %Modifică descriere programare pentru��5   	    � Icon$ Images/edit.png��66   $	��450��6M   $	��600��6Z    � ShowInTaskbar .   � 
ResizeMode$ NoResize=�6|    � WindowStartupLocation$ CenterScreen=�6�    � Topmost .  � 	Resources 5	         myTextBlocks �� 
TargetType �� Property	 �� Value  myComboBoxes  myLabels  myTextBoxes  myListBoxes��%3  &	        &	 M     &	 �     &	     &	 �    �� 5
   
   " ��6%   .�5      ��  � ��$	 16(�6)   66   5      �� 5   
   " ��6&   .�5      ��  � ��$	 14(�6)   66   5      �� 5   
   " ��6!   .�5      ��  � ��$	 16(�6)   66   �� 5       � ��$	 Bottom=�62   6C   5      �� 5   
   " ��6$   .�5      ��  � ��$	 16(�6)   66   �� 5       � ��
	 �����6;   �� 5       � ��$	 Bottom=�62   6C   5      �� 5   
   " ��6$   .�5      ��  � ��$	 16(�6)   66   5      5      .��5      �� 
 �� Orientation$
 Vertical=�6   .+�5   
   �� #��[  6   $��Vechea descriere��6:   $��	20,20,0,0q�6R   �� 5   
   -   5         ��Name$ tbDescriereVeche��5   7   #��[ 6    �� AcceptsReturn .  �� TextWrapping$
 Wrap=�6f   $��
20,5,20,10q�6z   $	��100��6�   {�.  6�   �� 5    
   #��[  6   $��Noua descriere��6:   $��	20,10,0,0q�6P   �� 5!   
   -   5        $ tbDescriereNoua��5!   7   #��[ 6    . $
 Wrap=�6e   $��
20,5,20,10q�6y   $	��100��6�   �� 5"   
   $
 
Horizontal=�6   .+�5#      �� -   5         ��Name$ btnModificaDescriere��5#      $
��Left=�64   $	��140��6O   $��40��6[   $��
130,50,0,0q�6g    � ToolTip$1 +Modifică descrierea programării efectuate��5$      .��5%      �� $
 
Horizontal=�6   .+�5&      �� $��/Images/edit.png��6   $��0,0,20,0q�66   $��35��6H   $��35��6S   5'      �� 5(      $��	Modifică��6    $��16(�60   $��Center=�6>    J� RenderTransformOrigin$ 0.5,0.5&�6Y   5)      5*      �� 5+      -   5        $ 
btnInchide��5+      $��Right=�6)   $	��140��6E   $��40��6Q   $��	50,50,0,0q�6]    �� IsCancel . .��5,      �� $
 
Horizontal=�6   .+�5-      �� $��/Images/close.png��6   $ 0.5,0.5&�67   $��0,0,20,0q�6W   $��35��6i   $��35��6t   5.      �� 5/      $��Închide��6    $��16(�6/   $��Center=�6=   6Z   50      51      52      53      54      