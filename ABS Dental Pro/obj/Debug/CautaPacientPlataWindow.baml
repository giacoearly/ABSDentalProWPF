   M S B A M L   `   `   `  ����I  EABS Dental Pro, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:ABS_Dental_ProABS_Dental_Pro  5   	   ,    &ABS_Dental_Pro.CautaPacientPlataWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:ABS_Dental_Pro     5   	     � Title$  Caută pacient��5      $	��480��6   $
��1000��6(    � WindowStartupLocation$ CenterScreen=�65    � ShowInTaskbar .   � 
ResizeMode$ NoResize=�6p    � Icon$ /Images/search.png��6�    � 	Resources 5	         myLabels �� 
TargetType �� Property �� Value  myTextBoxes��%.  &	        &	 z     �� 5
   
   " ��6!   .�5      ��  � ��$ 16(�6)   66   �� 5       � ��$ Bottom=�62   6C   5      �� 5   
   " ��6$   .�5      ��  � ��$ 16(�6)   66   �� 5       � ��
 �����6;   �� 5       � ��$ Bottom=�62   6C   5      5      .��5      �� .+�5   
   �� 	 �� Orientation$	 
Horizontal=�6   $��2.124,0,-2.124,0q�6.   .+�5      �� -   5        
 ��Name$
 btnCauta��5       � ToolTip$$ Caută pacient pentru o plată��6'    �� 	IsDefault . $��	45,20,0,0q�6`   $
��Left=�6s   $	��140��6�   $��40��6�   .��5      �� $	 
Horizontal=�6   .+�5      �� $��/Images/search.png��6   $
��Left=�68   $��Center=�5      $��0,0,30,0q�68    J� RenderTransformOrigin$ 0.5,0.5&�6J   $��35��6j   $��35��6u   5      �� 5      $��Caută��6    $��16(�6-   $��Center=�6;   $��Right=�6V   $��0q�6r   5      5      �� 5      -   5        $
 
btnIncarca��5      $ Încarcă date pacient��6)    . $��	45,20,0,0q�6Y   $
��Left=�6l   $	��140��6�   $��40��6�   {�.  .��5      �� $	 
Horizontal=�6   .+�5       �� $#��/Images/load2-upload-icon.png��6   $
��Left=�6C   $	��Top=�5!      $��0,0,30,0q�65   $ 0.5,0.5&�6G   $��35��6g   $��35��6r   5"      �� 5#      $��	Încarcă��6    $��16(�6/   $��Center=�6=   $��Right=�6X   $��0q�6t   5$      5%      �� 5&      -   5        $
 
btnInchide��5&      $ Închide fereastra��6)   $��Right=�6E   $	��140��6a   $��40��6m   $��	50,20,0,0q�6y    �� IsCancel . .��5'      �� $	 
Horizontal=�6   .+�5(      �� $��/Images/close.png��6   $ 0.5,0.5&�67   $��0,0,20,0q�6W   $��35��6i   $��35��6t   5)      �� 5*      $��Închide��6    $��16(�6/   $��Center=�6=   6Z   5+      5,      5-      �� 5.   
   $��280,20,280,0q�6   $	 
Horizontal=�6+   .+�5/      �� $��Nume/Prenume:��6   $��16(�6-   �� 50      -   5         ��Name$ tbCauta��50      #��[ 6'   $��Center=�6L   $	��200��6g   $��10,0,0,0q�6s   51      �� 52   
   $	 
Horizontal=�6   &   System.Windows.Controls.DataGrid  53   
   -   5        $��dataGridPacienti��53         SelectionMode$ Single=�6U      AutoGenerateColumns .     CanUserAddRows .     
IsReadOnly . ��. $
��Auto=�56      $
��Auto=�57      $	��300��6Q   $��0,20,0,0q�58   j   $��14(�6|   ��53   +   �� *listaPacientiProp+ � DataContext 58      ��  �� RelativeSource ��  �� AncestorType$ WindowL�   Columns 59      0  *System.Windows.Controls.DataGridTextColumn  5:      ,  &System.Windows.Controls.DataGridColumn   Header$ Număr Fișă��6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 69   �� *	NumarFisa+  5;      $ Medic��6%    64   �� *Medic+  5<      $
 Nume��6%    63   �� *Nume+  5=      $ Prenume��6%    66   �� *	Prenume+  5>      $	 CNP��6%    62   �� *Cnp+  5?      $ Serie CI��6%    67   �� *	SerieCi+  5@      $ 	Număr CI��6%    67   �� *	NumarCi+  5A      $ Vârsta��6%    65   �� *Varsta+  5B      $	 Sex��6%    62   �� *Sex+  5C      $ Telefon��6%    66   �� *	Telefon+  5D      $ Email��6%    64   �� *Email+  5E      $ Observații��6%    69   �� *
Observatii+5G      5H      5K      