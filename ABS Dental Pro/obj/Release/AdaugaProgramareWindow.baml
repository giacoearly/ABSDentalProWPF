   M S B A M L   `   `   `  ���� I  EABS Dental Pro, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:ABS_Dental_ProABS_Dental_Pro  +    %ABS_Dental_Pro.AdaugaProgramareWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     .localclr-namespace:ABS_Dental_Pro       �Language$  ro-RO� � Title$ Adaugă programare�� � Icon$ Images/add32.png��$	��570��$	��725�� � ShowInTaskbar .   � 
ResizeMode$ NoResize=� � WindowStartupLocation$ CenterScreen=� � 	Resources    myTextBlocks �� 
TargetType �� Property	 �� Value  myComboBoxes  myLabels  myTextBoxes  myListBoxes  styleCalendar&   System.Windows.Controls.Calendar
 �� 
TargetType5  /System.Windows.Controls.Primitives.CalendarItem  Name��%�  &	        &	 #     &	 F     &	 �     &	 �     &	 �     �� " ��.���  � ��$	 16(��� " ��.���  � ��$	 14(��� " ��.���  � ��$	 16(���  � ��$	 Bottom=��� " ��.���  � ��$	 16(���  � ��
	 �������  � ��$	 Bottom=��� " ��.���  � ��$	 16(��� "  .���  � ��	 �� "
  .R�(� $	��300��$	��300��.��  $ PART_CalendarItem��#��z��#��z��#��z��.���� .+���  �� Orientation$ 
Horizontal=�.+��� -    ��Name$ 	btnAdauga�� � ToolTip$ Adaugă programare��$��11��$��20,20,240,15q�$��45��$	��145��.���� $ 
Horizontal=�.+��� $��/Images/add.png��$
��Left=�$��Center=�$��0,0,20,0q� J� RenderTransformOrigin$ 0.5,0.5&�$��50��$��50���� $��Adaugă��$��16(�$��Center=�$��Right=�$��0q��� -   $ btnAdaugaPacient��$ Adaugă pacient nou��$��110,35,210,15q�$��30��$	��160��.���� $ 
Horizontal=�.+��� $��/Images/add.png��$
��Left=�$��Center=�$��0,0,5,0q�$ 0.5,0.5&�$��30��$��30���� $��Adaugă pacient��$��Center=�$��Right=�$��10,0,0,0q�$��14(��� $ 
Horizontal=�.+��� .+��� #��[  $��Medic��$��	20,20,0,0q��� -    ��Name$ lbMedici��#��[ $	��200��$	��160��$��20,5,0,0q����� *listaMediciProp+�� .+��� #��[  $��Pacient��$��	20,20,0,0q��� -   $ 
lbPacienti��#��[ $	��200��$	��300��$��20,5,0,0q����� *listaPacientiPropProgramare+�� .+��� #��[  $
��Data��$��	20,20,0,0q�(  "System.Windows.Controls.DatePicker  -   $��
datePicker��   CalendarStyle# [ $��25��$
��Left=�$	��120��$��20,5,0,0q��� $ 
Horizontal=�.+��� .+��� #��[  $	��Ora��$��	20,20,0,0q��� $ 
Horizontal=�$��0q�.+��� -    ��Name$ cbOra��#��[ $��25��$��45��$��20,5,0,0q�.@��� $��9���� $��10���� $��11���� $��12���� $��13���� $��14���� $��15���� $��16���� $��17���� $��18���� $��19���� -   $ cbMinute��#��[ $��25��$��45��$��0,5,0,0q�$ 0.5,0.5&�.@��� $��00���� $��15���� $��30���� $��45���� .+��� #��[  $��Durata��$��	40,20,0,0q��� $ 
Horizontal=�$��0q�.+��� -   $ cbDurata��#��[ $��25��$��90��$��40,5,0,0q�.@��� $��	30 minute���� $��1 oră���� $��1,5 ore���� $��2 ore���� $��2,5 ore���� $��3 ore���� -	    ��Name$ chkbMedicSelectat��$
��Left=�$��14(�$��
20,150,0,0q�$)��#Doar pacienții medicului selectat.�� d� 	IsChecked$
 TrueY��� #��[  $��	Descriere��$��	20,10,0,0q��� -
    ��Name$ tbDescriere��#��[  �� AcceptsReturn .  �� TextWrapping$
 Wrap=�$��
20,5,30,10q�$��50��