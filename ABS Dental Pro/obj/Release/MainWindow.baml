   M S B A M L   `   `   `  ���� I  EABS Dental Pro, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:ABS_Dental_ProABS_Dental_Pro      ABS_Dental_Pro.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    >d2http://schemas.microsoft.com/expression/blend/2008    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     .localclr-namespace:ABS_Dental_Pro       � Title$  ABS Dental Pro�� � Icon$ /Images/tooth.png��$��751.057��$��975.359�� � WindowStartupLocation$ CenterScreen=� � WindowState$ 	Maximized=� � 	Resources    myLabels �� 
TargetType �� Property �� Value  myTextBoxes  myComboBoxes  myTextBlocks  myTextBlocks2��%-  &	        &	 =     &	 �     &	 �     &	 �     �� " ��.���  � ��$ 16(���  � ��$ Bottom=��� " ��.���  � ��$ 16(���  � ��
 �������  � ��$ Bottom=��� " ��.���  � ��$ 16(��� " ��.���  � ��$ 18(��� " ��.���  � ��$ 20(� J� CommandBindings )  #System.Windows.Input.CommandBinding  -   	   Command$	 local:CustomCommands.Exit��.��� 
 � RowDefinitions
 �� $��* � � ColumnDefinitions �� $��* �.+�o� -    o�Name$ mnuMain��$��0q�$	��Top=�$
��Left=�.@�m� $��_Aplicație��$��12(�.@�m� $��_Ieșire��$
��Left=� m� Command$ local:CustomCommands.Exit�� m� Icon �� $��/Images/exit.png��$��17��$��17��m� $��_Ajutor��$��12(�.@�m� -   $��_Despre��$
��Left=� �� $��/Images/about.png��$��20��$��20���� -    ��Name$ 
tabControl��$��0,30,0,0q�.@��� $��	Pacienți��$��14(�.���� .+���  �� Orientation$ 
Horizontal=�.+��� -   $��btnAdaugaPacient�� � ToolTip$ Adaugă pacient��$	��Top=�$
��Left=�$��
20,20,0,20q�$	��140��$��40��.���� $ 
Horizontal=�.+��� $��/Images/add.png��$
��Left=�$��Center=�$��0,0,20,0q� J� RenderTransformOrigin$ 0.5,0.5&�$��50��$��50���� $��Adaugă��$��16(�$��Center=�$��Right=�$��0q��� -    ��Name$ btnCautaPacient��$ Caută pacient��$��Center=�$
��Left=�$��40,0,0,0q�$	��140��$��40��.���� $ 
Horizontal=�.+��� $��/Images/search.png��$
��Left=�$��Center=�$��0,0,10,0q�$ 0.5,0.5&�$��35���� $��Caută��$��16(�$��Center=�$��Right=�$��20,0,0,0q�$ 0.446,0.584&��� -   $ btnModifica��$ Modifică pacient��$
��Left=�$	��140��$��40��$��40,0,0,0q�{�.  .���� $ 
Horizontal=�.+��� $��/Images/edit.png��$��0,0,20,0q�$ 0.5,0.5&�$��35���� $��	Modifică��$��16(�$��Center=�$ 0.5,0.5&�$��0q��� -   $ btnIstoricPacient��$# Vezi istoricul programărilor��$
��Left=�$	��140��$��40��$��40,0,0,0q�{�.  .���� $ 
Horizontal=�.+��� $!��/Images/History-icon128.png��$��0,0,20,0q�$ 0.5,0.5&�$��35���� $��Istoric��$��16(�$��Center=�$ 0.5,0.5&�$��0q��� -	   $ 	btnSterge��$ Șterge pacient��$��Right=�$	��140��$��40��$��40,0,0,0q�{�.  .���� $ 
Horizontal=�.+��� $��/Images/remove.png��$ 0.5,0.5&�$��0,0,25,0q�$��35��$��35���� $��Șterge��$��16(�$��Center=��� $��Total număr pacienți:��$��16(�$��20��$��	40,20,0,0q��� -
    ��Name$ tbNrPacienti��$��16(�$��	10,20,0,0q�$��20��&   System.Windows.Controls.DataGrid  -   $��dataGridPacienti��   AutoGenerateColumns .     CanUserAddRows .     SelectionMode$ Single=�   
IsReadOnly . ��. $
��Auto=�$
��Auto=�$	��655������ *listaPacientiProp+ � DataContext ��  �� RelativeSource ��  �� AncestorType$ WindowL�   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Număr Fișă��1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *	NumarFisa+  $ Medic�� �� *Medic+  $
 Nume�� �� *Nume+  $ Prenume�� �� *	Prenume+  $	 CNP�� �� *Cnp+  $ Serie CI�� �� *	SerieCi+  $ 	Număr CI�� �� *	NumarCi+  $ Vârsta�� �� *Varsta+  $	 Sex�� �� *Sex+  $ Telefon�� �� *	Telefon+  $ Email�� �� *Email+  $ Observații�� �� *
Observatii+�� $��Programări��$��14(�.���� -   $ tabProgramari��$��0,16,0,4q�.@��� -    ��Name$ tabItemMedic1��$
��N.A.��$��14(�y���   �� ElementName$  detaliiMedic1��! �� Path$! 
Visibility�.���� -   " ��Name$" stackPanel11��.+��� -   $" stackPanel1��$ 
Horizontal=�.+��� -   $ btnAdaugaProgramareMedic1��$ Adaugă programare��$	��Top=�$
��Left=�$��
20,20,0,20q�$	��140��$��40��.���� $ 
Horizontal=�.+��� $��/Images/add.png��$
��Left=�$��Center=�$��0,0,20,0q�$ 0.5,0.5&�$��50��$��50���� $��Adaugă��$��16(�$��Center=�$��Right=�$��0q��� -   $! btnModificaProgramareMedic1��$ Modifică programare��$
��Left=�$	��140��$��40��$��40,0,0,0q�.���� $ 
Horizontal=�.+��� $��/Images/edit.png��$��0,0,10,0q�$ 0.5,0.5&�$��40���� $��	Modifică��$��16(�$��Center=�$ 0.5,0.5&��� $��Data curentă:��$��16(�$��20��$��	40,20,0,0q��� -   $ tbDataCurenta1��$��16(�$��	10,20,0,0q�$��20���� -   $ btnInapoiProgramareMedic1��$
��Left=�$��60��$��40��$��	510,0,0,0q�$4 .Dă înapoi programările pacienților cu o zi��.���� $ 
Horizontal=�.+��� $)��#/Images/Actions-arrow-left-icon.png��$��0q�$��30���� -   $  btnInainteProgramareMedic1��$
��Left=�$��60��$��40��$��40,0,0,0q�$5 /Dă înainte programările pacienților cu o zi��.���� $ 
Horizontal=�.+��� $*��$/Images/Actions-arrow-right-icon.png��$��0q�$��30��$��Center=�$��Center=��� -   $% btnRepedeInapoiProgramareMedic1��$
��Left=�$��60��$��40��$��40,0,0,0q�$B <Dă înapoi programările pacienților cu două săptămâni��.���� $ 
Horizontal=�.+��� $0��*/Images/Actions-arrow-left-double-icon.png��$��0q�$��30��$��Center=�$��Center=��� -   $&  btnRepedeInainteProgramareMedic1��$
��Left=�$��60��$��40��$��40,0,0,0q�$C =Dă înainte programările pacienților cu două săptămâni��.���� $ 
Horizontal=�.+��� $1��+/Images/Actions-arrow-right-double-icon.png��$��0q�$��30��$��Center=�$��Center=��� -   # ��Name$# programariPacientiCanvas1��$��	20,20,0,0q�.+�'   !ABS_Dental_Pro.ProgramariPacienti  -   $  Name$$ programariPacienti1���� -   $ tabItemMedic2��$
��N.A.��$��14(�y��� $  detaliiMedic2��$! 
Visibility�.���� .+��� $ 
Horizontal=�.+��� -   $ btnAdaugaProgramareMedic2��$	��Top=�$
��Left=�$��
20,20,0,20q�$	��140��$��40�����	#FFDDDDDD.���� $ 
Horizontal=�.+��� $��/Images/add.png��$
��Left=�$��Center=�$��0,0,20,0q�$ 0.5,0.5&�$��50��$��50���� $��Adaugă��$��16(�$��Center=�$��Right=�$��0q��� -   $! btnModificaProgramareMedic2��$
��Left=�$	��140��$��40��$��40,0,0,0q����	#FFDDDDDD.���� $ 
Horizontal=�.+��� $��/Images/edit.png��$��0,0,10,0q�$ 0.5,0.5&�$��40���� $��	Modifică��$��16(�$��Center=�$ 0.5,0.5&��� $��Data curentă:��$��16(�$��20��$��	40,20,0,0q��� -   $ tbDataCurenta2��$��16(�$��	10,20,0,0q�$��20���� -   $ btnInapoiProgramareMedic2��$
��Left=�$��60��$��40��$��	510,0,0,0q�$4 .Dă înapoi programările pacienților cu o zi��.���� $ 
Horizontal=�.+��� $)��#/Images/Actions-arrow-left-icon.png��$��0q�$��30���� -   $  btnInainteProgramareMedic2��$
��Left=�$��60��$��40��$��40,0,0,0q�$5 /Dă înainte programările pacienților cu o zi��.���� $ 
Horizontal=�.+��� $*��$/Images/Actions-arrow-right-icon.png��$��0q�$��30��$��Center=�$��Center=��� -   $% btnRepedeInapoiProgramareMedic2��$
��Left=�$��60��$��40��$��40,0,0,0q�$B <Dă înapoi programările pacienților cu două săptămâni��.���� $ 
Horizontal=�.+��� $0��*/Images/Actions-arrow-left-double-icon.png��$��0q�$��30��$��Center=�$��Center=��� -    $&  btnRepedeInainteProgramareMedic2��$
��Left=�$��60��$��40��$��40,0,0,0q�$C =Dă înainte programările pacienților cu două săptămâni��.���� $ 
Horizontal=�.+��� $1��+/Images/Actions-arrow-right-double-icon.png��$��0q�$��30��$��Center=�$��Center=��� -!   $# programariPacientiCanvas2��$��	20,20,0,0q�.+�  -"   $$ programariPacienti2���� -#   $ tabItemMedic3��$
��N.A.��$��14(�y��� $  detaliiMedic3��$! 
Visibility�.���� .+��� $ 
Horizontal=�.+��� -$   $ btnAdaugaProgramareMedic3��$	��Top=�$
��Left=�$��
20,20,0,20q�$	��140��$��40��.���� $ 
Horizontal=�.+��� $��/Images/add.png��$
��Left=�$��Center=�$��0,0,20,0q�$ 0.5,0.5&�$��50��$��50���� $��Adaugă��$��16(�$��Center=�$��Right=�$��0q��� -%   $! btnModificaProgramareMedic3��$
��Left=�$	��140��$��40��$��40,0,0,0q�.���� $ 
Horizontal=�.+��� $��/Images/edit.png��$��0,0,10,0q�$ 0.5,0.5&�$��40���� $��	Modifică��$��16(�$��Center=�$ 0.5,0.5&��� $��Data curentă:��$��16(�$��20��$��	40,20,0,0q��� -&   $ tbDataCurenta3��$��16(�$��	10,20,0,0q�$��20���� -'   $ btnInapoiProgramareMedic3��$
��Left=�$��60��$��40��$��	510,0,0,0q�$4 .Dă înapoi programările pacienților cu o zi��.���� $ 
Horizontal=�.+��� $)��#/Images/Actions-arrow-left-icon.png��$��0q�$��30���� -(   $  btnInainteProgramareMedic3��$
��Left=�$��60��$��40��$��40,0,0,0q�$5 /Dă înainte programările pacienților cu o zi��.���� $ 
Horizontal=�.+��� $*��$/Images/Actions-arrow-right-icon.png��$��0q�$��30��$��Center=�$��Center=��� -)   $% btnRepedeInapoiProgramareMedic3��$
��Left=�$��60��$��40��$��40,0,0,0q�$B <Dă înapoi programările pacienților cu două săptămâni��.���� $ 
Horizontal=�.+��� $0��*/Images/Actions-arrow-left-double-icon.png��$��0q�$��30��$��Center=�$��Center=��� -*   $&  btnRepedeInainteProgramareMedic3��$
��Left=�$��60��$��40��$��40,0,0,0q�$C =Dă înainte programările pacienților cu două săptămâni��.���� $ 
Horizontal=�.+��� $1��+/Images/Actions-arrow-right-double-icon.png��$��0q�$��30��$��Center=�$��Center=��� -+   $# programariPacientiCanvas3��$��	20,20,0,0q�.+�  -,   $$ programariPacienti3���� --   $ tabItemMedic4��$
��N.A.��$��14(�y��� $  detaliiMedic4��$! 
Visibility�.���� .+��� $ 
Horizontal=�.+��� -.   $ btnAdaugaProgramareMedic4��$	��Top=�$
��Left=�$��
20,20,0,20q�$	��140��$��40��.���� $ 
Horizontal=�.+��� $��/Images/add.png��$
��Left=�$��Center=�$��0,0,20,0q�$ 0.5,0.5&�$��50��$��50���� $��Adaugă��$��16(�$��Center=�$��Right=�$��0q��� -/   $! btnModificaProgramareMedic4��$
��Left=�$	��140��$��40��$��40,0,0,0q�.���� $ 
Horizontal=�.+��� $��/Images/edit.png��$��0,0,10,0q�$ 0.5,0.5&�$��40���� $��	Modifică��$��16(�$��Center=�$ 0.5,0.5&��� $��Data curentă:��$��16(�$��20��$��	40,20,0,0q��� -0   $ tbDataCurenta4��$��16(�$��	10,20,0,0q�$��20���� -1   $ btnInapoiProgramareMedic4��$
��Left=�$��60��$��40��$��	510,0,0,0q�$4 .Dă înapoi programările pacienților cu o zi��.���� $ 
Horizontal=�.+��� $)��#/Images/Actions-arrow-left-icon.png��$��0q�$��30���� -2   $  btnInainteProgramareMedic4��$
��Left=�$��60��$��40��$��40,0,0,0q�$5 /Dă înainte programările pacienților cu o zi��.���� $ 
Horizontal=�.+��� $*��$/Images/Actions-arrow-right-icon.png��$��0q�$��30��$��Center=�$��Center=��� -3   $% btnRepedeInapoiProgramareMedic4��$
��Left=�$��60��$��40��$��40,0,0,0q�$B <Dă înapoi programările pacienților cu două săptămâni��.���� $ 
Horizontal=�.+��� $0��*/Images/Actions-arrow-left-double-icon.png��$��0q�$��30��$��Center=�$��Center=��� -4   $&  btnRepedeInainteProgramareMedic4��$
��Left=�$��60��$��40��$��40,0,0,0q�$C =Dă înainte programările pacienților cu două săptămâni��.���� $ 
Horizontal=�.+��� $1��+/Images/Actions-arrow-right-double-icon.png��$��0q�$��30��$��Center=�$��Center=��� -5   $# programariPacientiCanvas4��$��	20,20,0,0q�.+�  -6   $$ programariPacienti4���� -7   $ tabItemMedic5��$
��N.A.��$��14(�y��� $  detaliiMedic5��$! 
Visibility�.���� .+��� $ 
Horizontal=�.+��� -8   $ btnAdaugaProgramareMedic5��$	��Top=�$
��Left=�$��
20,20,0,20q�$	��140��$��40��.���� $ 
Horizontal=�.+��� $��/Images/add.png��$
��Left=�$��Center=�$��0,0,20,0q�$ 0.5,0.5&�$��50��$��50���� $��Adaugă��$��16(�$��Center=�$��Right=�$��0q��� -9   $! btnModificaProgramareMedic5��$
��Left=�$	��140��$��40��$��40,0,0,0q�.���� $ 
Horizontal=�.+��� $��/Images/edit.png��$��0,0,10,0q�$ 0.5,0.5&�$��40���� $��	Modifică��$��16(�$��Center=�$ 0.5,0.5&��� $��Data curentă:��$��16(�$��20��$��	40,20,0,0q��� -:   $ tbDataCurenta5��$��16(�$��	10,20,0,0q�$��20���� -;   $ btnInapoiProgramareMedic5��$
��Left=�$��60��$��40��$��	510,0,0,0q�$4 .Dă înapoi programările pacienților cu o zi��.���� $ 
Horizontal=�.+��� $)��#/Images/Actions-arrow-left-icon.png��$��0q�$��30���� -<   $  btnInainteProgramareMedic5��$
��Left=�$��60��$��40��$��40,0,0,0q�$5 /Dă înainte programările pacienților cu o zi��.���� $ 
Horizontal=�.+��� $*��$/Images/Actions-arrow-right-icon.png��$��0q�$��30��$��Center=�$��Center=��� -=   $% btnRepedeInapoiProgramareMedic5��$
��Left=�$��60��$��40��$��40,0,0,0q�$B <Dă înapoi programările pacienților cu două săptămâni��.���� $ 
Horizontal=�.+��� $0��*/Images/Actions-arrow-left-double-icon.png��$��0q�$��30��$��Center=�$��Center=��� ->   $&  btnRepedeInainteProgramareMedic5��$
��Left=�$��60��$��40��$��40,0,0,0q�$C =Dă înainte programările pacienților cu două săptămâni��.���� $ 
Horizontal=�.+��� $1��+/Images/Actions-arrow-right-double-icon.png��$��0q�$��30��$��Center=�$��Center=��� -?   $# programariPacientiCanvas5��$��	20,20,0,0q�.+�  -@   $$ programariPacienti5���� -A   $ tabItemMedic6��$
��N.A.��$��14(�y��� $  detaliiMedic6��$! 
Visibility�.���� .+��� $ 
Horizontal=�.+��� -B   $ btnAdaugaProgramareMedic6��$	��Top=�$
��Left=�$��
20,20,0,20q�$	��140��$��40��.���� $ 
Horizontal=�.+��� $��/Images/add.png��$
��Left=�$��Center=�$��0,0,20,0q�$ 0.5,0.5&�$��50��$��50���� $��Adaugă��$��16(�$��Center=�$��Right=�$��0q��� -C   $! btnModificaProgramareMedic6��$
��Left=�$	��140��$��40��$��40,0,0,0q�.���� $ 
Horizontal=�.+��� $��/Images/edit.png��$��0,0,10,0q�$ 0.5,0.5&�$��40���� $��	Modifică��$��16(�$��Center=�$ 0.5,0.5&��� $��Data curentă:��$��16(�$��20��$��	40,20,0,0q��� -D   $ tbDataCurenta6��$��16(�$��	10,20,0,0q�$��20���� -E   $ btnInapoiProgramareMedic6��$
��Left=�$��60��$��40��$��	510,0,0,0q�$4 .Dă înapoi programările pacienților cu o zi��.���� $ 
Horizontal=�.+��� $)��#/Images/Actions-arrow-left-icon.png��$��0q�$��30���� -F   $  btnInainteProgramareMedic6��$
��Left=�$��60��$��40��$��40,0,0,0q�$5 /Dă înainte programările pacienților cu o zi��.���� $ 
Horizontal=�.+��� $*��$/Images/Actions-arrow-right-icon.png��$��0q�$��30��$��Center=�$��Center=��� -G   $% btnRepedeInapoiProgramareMedic6��$
��Left=�$��60��$��40��$��40,0,0,0q�$B <Dă înapoi programările pacienților cu două săptămâni��.���� $ 
Horizontal=�.+��� $0��*/Images/Actions-arrow-left-double-icon.png��$��0q�$��30��$��Center=�$��Center=��� -H   $&  btnRepedeInainteProgramareMedic6��$
��Left=�$��60��$��40��$��40,0,0,0q�$C =Dă înainte programările pacienților cu două săptămâni��.���� $ 
Horizontal=�.+��� $1��+/Images/Actions-arrow-right-double-icon.png��$��0q�$��30��$��Center=�$��Center=��� -I   $# programariPacientiCanvas6��$��	20,20,0,0q�.+�  -J   $$ programariPacienti6���� $��Plăți��$��14(�$��24.62��$��Bottom=�.���� $��0q�.+��� $ 
Horizontal=�$��0,10,0,0q�.+��� -K   $ btnCautaPacientPlata��$$ Caută pacient pentru o plată��$��Center=�$
��Left=�$��50,0,0,0q�$	��140��$��40��.���� $ 
Horizontal=�.+��� $��Images/search.png��$
��Left=�$��Center=�$��0,0,10,0q�$ 0.5,0.5&�$��35���� $��Caută��$��16(�$��Center=�$��Right=�$��20,0,0,0q�$ 0.446,0.584&��� -L   $ btnAdaugaPlata��$" Adaugă plată lucrare nouă��$	��Top=�$
��Left=�$��
50,20,0,20q�$	��140��$��40��{�.  .���� $ 
Horizontal=�.+��� $��Images/add.png��$
��Left=�$��Center=�$��0,0,20,0q�$ 0.5,0.5&�$��50��$��50���� $��Adaugă��$��16(�$��Center=�$��Right=�$��0q��� -M   $ btnAdaugaPlataDoi��$ Adaugă plată tranșă��$	��Top=�$
��Left=�$��
50,20,0,20q�$	��140��$��40��{�.  .���� $ 
Horizontal=�.+��� $��Images/math-add-icon.png��$
��Left=�$��Center=�$��0,0,20,0q�$ 0.5,0.5&�$��40��$��35���� $��Tranșă��$��16(�$��Center=�$��Right=�$��0q��� $ 
Horizontal=�$��0,10,0,0q�.+��� $ Vertical=�.+��� -N   $ tbInformatii��$��Informații pacient��$��24(�$��	30,0,0,10q�� $��470.8��$��10,0,0,0q� �� $	��120 ��� $	��700 �
 �� $	��31* ��� $	��31* ��� $	��31* ��� $	��31* ��� $	��31* ��� $	��31* ��� $	��31* ��� $	��31* ��� $	��31* ��� $	��31* ��� $	��31* ��� $	��31* ��� $	��95* �.+��� #��[ $��Număr Fișă��$��0��$��0��$��5,0,0,0q�$��2���� -O   $ tbNumarFisa��$��1��$��0��#��[ $��0,0,10,5q��� #��[ $��Medic��$��0��$��1��$��5,0,0,0q��� -P   $ tbMedic��$��1��$��1��#��[ $��0,0,10,0q�$	��Top=��� #��[ $
��Nume��$��0��$��2��$��5,0,0,0q��� -Q   $ tbNume��$��1��$��2��#��[ $��0,0,10,0q��� #��[ $��Prenume��$��0��$��3��$��5,0,0,0q��� -R   $ 	tbPrenume��$��1��$��3��#��[ $��0,0,10,0q��� #��[ $	��CNP��$��0��$��4��$��5,0,0,0q��� -S   $ tbCnp��$��1��$��4��#��[ $��0,0,10,0q��� #��[ $��Seria CI��$��0��$��5��$��5,0,0,0q��� -T   $ 	tbSeriaCi��$��1��$��5��#��[ $��0,0,10,0q��� #��[ $��	Număr CI��$��0��$��6��$��5,0,0,0q��� -U   $ 	tbNumarCi��$��1��$��6��#��[ $��0,0,10,0q��� #��[ $��Vârsta��$��0��$��7��$��5,0,0,0q��� -V   $ tbVarsta��$��1��$��7��#��[ $��0,0,10,0q��� #��[ $	��Sex��$��0��$��8��$��5,0,0,0q��� -W   $ tbSex��$��1��$��8��$��Center=�$
��Left=�$��16(�$��24.236���� #��[ $��Telefon��$��0��$��9��$��5,0,0,0q��� -X   $ 	tbTelefon��$��1��$��9��#��[ $��0,0,10,0q��� #��[ $��Email��$��0��$��10��$��5,0,0,0q��� -Y   $ tbEmail��$��1��$��10��#��[ $��0,0,10,0q��� #��[ $��Observații��$��0��$��11��$��5,0,0,0q��� -Z   $ tbObservatii��$
��Wrap=�$��1��$��11��#��[ $��0,0,10,0q��� -[   $ btnModificaPacient��$ Modifică pacient��$��12��$��0��$��2��$��	30,10,0,0q�$	��140��$��40��{�.  $
��Left=�$	��Top=�.���� $ 
Horizontal=�.+��� $��Images/edit.png��$
��Left=�$��Center=�$��0,0,20,0q�$ 0.5,0.5&�$��40��$��40���� $��	Modifică��$��16(�$��Center=�$��Right=�$��0q��� .+��� -\   $ 
tbSituatie��$��Situație plăți��$��24(�$��	20,0,0,10q��  �� $	��150 ��� 
 �� $��40 ��� $��40 ��� $��40 ��� $��40 ��� $��40 ��� $��40 ��� $��80 �.+��� $��Medic��$��0��#��[ $��0��$��2��$��
1,1,252,39q�$��2���� -]   $ tbMedic2��$��1��$��0��#��[ $��0q�$��28��$	��253��$
��Left=�$	��Top=��� $��Total de plătit��$��1��$��0��#��[ �� -^   $ tbTotal��$��1��$��1��#��[ �� $��Total plătit��$��2��$��0��#��[ �� -_   $ tbTransa��$��2��$��1��#��[ �� $��Rest de plătit��$��3��$��0��#��[ �� -`   $ tbRest��
���  ��$
��Bold"�$��3��$��1��#��[ �� $
��Data��$��4��$��0��#��[ �� -a   $ tbData��$��4��$��1��#��[ �� $��	Descriere��$��5��$��0��#��[ �� -b   $ tbDescriere��$��5��$��1��#��[ �� -c   $ btnIstoricPlati��$ Vezi istoricul plăților��$��6��$��0��$
��Left=�$	��140��$��	30,10,0,0q�{�.  $��2��$��40��$	��Top=�.���� $ 
Horizontal=�.+��� $!��Images/if_wallet_286672.png��$��0,0,20,0q�$ 0.5,0.5&�$��35���� $��Istoric��$��16(�$��Center=�$ 0.5,0.5&�$��0q��� -d   $ btnModificaPlata��$ Modifică ultima plată��$��6��$��0��$��2��$��
220,10,0,0q�$	��140��$��40��{�.  $
��Left=�$	��Top=�.���� $ 
Horizontal=�.+��� $��Images/edit.png��$
��Left=�$��Center=�$��0,0,20,0q�$ 0.5,0.5&�$��40��$��40���� $��	Modifică��$��16(�$��Center=�$��Right=�$��0q��� $��Medici��$��0q�$��14(�.���� ��. .��� $��0q�
 �� $	��100 ��� $��* ��� $��* � �� $	��17* ��� $	��18* ��� $	��35* ��� $	��35* �.+��� -e   $��btnAdaugaMedic��$��0��$��0��$ Adaugă medic��$	��Top=�$
��Left=�$��	30,30,0,0q�$	��140��$��40��$��2��.���� $ 
Horizontal=�.+��� $��/Images/add.png��$
��Left=�$��Center=�$��0,0,20,0q�$ 0.5,0.5&�$��50��$��50���� $��Adaugă��$��16(�$��Center=�$��Right=�$��0q�&    ABS_Dental_Pro.DetaliiMedicCombo  -f   %  Name$% detaliiMedic1��&    ABS_Dental_Pro.DetaliiMedicCombo&   IsVisibleChanged$$& detaliiMedic1_IsVisibleChanged��$��
0,15,460,0q�$��1��$��0��$��Center=�$	��Top=�{�.  $y�Hidden=�$��3��  -g   $% detaliiMedic2��$$& detaliiMedic2_IsVisibleChanged��$��
0,15,260,0q�$��1��$��1��$��Center=�$	��Top=�{�.  $y�Hidden=�$��3��  -h   $% detaliiMedic3��$$& detaliiMedic3_IsVisibleChanged��$��430,15,0,15q�$��1��$��2��$��Center=�$��Center=�{�.  $y�Hidden=�$��2��  -i   $% detaliiMedic4��$$& detaliiMedic4_IsVisibleChanged��$��0,15,460,15q�$��2��$��0��$��Center=�$��Center=�{�.  $y�Hidden=�$��3��  -j   $% detaliiMedic5��$$& detaliiMedic5_IsVisibleChanged��$��0,15,260,15q�$��2��$��1��$��Center=�$��Center=�{�.  $y�Hidden=�$��3��  -k   $% detaliiMedic6��$$& detaliiMedic6_IsVisibleChanged��$��430,15,0,15q�$��2��$��2��$��Center=�$��Center=�{�.  $y�Hidden=�$��2��