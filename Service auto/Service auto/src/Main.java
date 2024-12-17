import javax.swing.*;
import java.awt.*;
import java.util.ArrayList;
import java.util.List;
import java.awt.event.MouseAdapter;
import java.awt.event.MouseEvent;
import javax.swing.table.DefaultTableModel;
import javax.swing.table.TableModel;


public class Main {


    public static void main(String[] args) {

        JFrame f = new JFrame("SERVICE AUTO");
        f.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        f.setSize(600, 600);
        f.setLayout(null);
        f.setLocationRelativeTo(null);

        // Adaugă JLabel pentru imaginea de fundal
        JLabel backgroundLabel = new JLabel();
        backgroundLabel.setBounds(0, 0, 600, 600);
        ImageIcon backgroundImageIcon = new ImageIcon("C:\\Users\\Alin Sârbu\\OneDrive\\Desktop\\personal_project\\src\\service.png");
        Image backgroundImage = backgroundImageIcon.getImage().getScaledInstance(600, 600, Image.SCALE_SMOOTH);
        ImageIcon scaledBackgroundImageIcon = new ImageIcon(backgroundImage);
        backgroundLabel.setIcon(scaledBackgroundImageIcon);
        f.add(backgroundLabel);

        // Adaugă meniul
        JMenuBar menuBar = new JMenuBar();
        JMenu menuHome = new JMenu("ACASA");

        JMenu menuClients = new JMenu("Clienti");
        JMenuItem menuShowClients = new JMenuItem("LISTA_Clienti");
        JMenuItem menuAddClient = new JMenuItem("ADAUGA_Client");
        //JMenuItem menuDeleteClient = new JMenuItem("STERGE_Client");

        menuClients.add(menuShowClients);
        menuClients.add(menuAddClient);
        //menuClients.add(menuDeleteClient);

        JMenu menuCars = new JMenu("Masini");
        JMenuItem menuShowCars = new JMenuItem("Lista masini");
        JMenuItem menuAddCars = new JMenuItem("Adauga masini");
        JMenuItem menuDeleteCar = new JMenuItem("Sterge_masini");

        menuCars.add(menuShowCars);
        menuCars.add(menuAddCars);
        menuCars.add(menuDeleteCar);

        JMenu menuPersonal = new JMenu("Personal");
        JMenuItem menuShowPersonal = new JMenuItem("Lista personal");
        JMenuItem menuAddPersonal = new JMenuItem("Adauga personal");
        JMenuItem menuDeletePersonal = new JMenuItem("Concediaza personal");

        menuPersonal.add(menuShowPersonal);
        menuPersonal.add(menuAddPersonal);
        menuPersonal.add(menuDeletePersonal);

        menuBar.add(menuHome);
        menuBar.add(menuClients);
        menuBar.add(menuCars);
        menuBar.add(menuPersonal);

        f.setJMenuBar(menuBar);
        f.setVisible(true);

        menuShowClients.addActionListener(e -> {
            List<Client> clients;
            clients = new ArrayList<>();

            try {
                clients = DBUtils.getAllActiveClients();
            } catch (Exception ex) {
                System.out.println(ex.getMessage());
            }

            if (clients.isEmpty()) {
                System.out.println("Nu există clienți în lista activă.");
                return;
            }

            String[] columnNames = {"Nume", "Prenume", "Varsta", "Data_Nasterii", "Telefon"};
            Object[][] data = new Object[clients.size()][columnNames.length];

            for (int i = 0; i < clients.size(); i++) {
                Client client = clients.get(i);
                data[i][0] = client.getNume();
                data[i][1] = client.getPrenume();
                data[i][2] = client.getVarsta();
                data[i][3] = client.getData_nasterii();
                data[i][4] = client.getTelefon();
            }

            JTable table = new JTable(data, columnNames);

            JPanel backgroundPanel = new JPanel(new BorderLayout()) {
                @Override
                protected void paintComponent(Graphics g) {
                    super.paintComponent(g);
                    ImageIcon backgroundImageIcon = new ImageIcon("C:\\Users\\Alin Sârbu\\Downloads\\service.png");
                    Image backgroundImage = backgroundImageIcon.getImage().getScaledInstance(600, 600, Image.SCALE_SMOOTH);
                    g.drawImage(backgroundImage, 0, 0, this);
                }
            };

            backgroundPanel.add(new JScrollPane(table));



            JButton btnCancel = new JButton("Cancel");

            btnCancel.addActionListener(cancelEvent -> {
                // Închiderea ferestrei și revenirea în meniu
                f.setContentPane(backgroundLabel); // Revenim la imaginea de fundal
                f.revalidate();
                f.pack();
                f.setSize(600, 600);
            });
            backgroundPanel.add(btnCancel, BorderLayout.SOUTH);

            // Adăugăm JPanel personalizat în fereastra principală
            f.setContentPane(backgroundPanel);
            f.revalidate(); // Actualizăm conținutul fereastrei

            // Resetăm mărimea ferestrei pentru a se potrivi cu noul conținut
            f.pack();
            f.setSize(600, 600);
        });

        menuAddClient.addActionListener(e -> {
            // Eliminarea conținutului existent din fereastra principală
            f.getContentPane().removeAll();

            // Adăugarea conținutului specific pentru fereastra de adăugare
            JPanel addClientPanel = new JPanel(new GridLayout(6, 2));

            JLabel lblNume = new JLabel("Nume: ");
            JTextField txtNume = new JTextField();
            JLabel lblPrenume = new JLabel("Prenume: ");
            JTextField txtPrenume = new JTextField();
            JLabel lblVarsta = new JLabel("Varsta: ");
            JTextField txtVarsta = new JTextField();
            JLabel lblDataNasterii = new JLabel("Data Nasterii: ");
            JTextField txtDataNasterii = new JTextField();
            JLabel lblTelefon = new JLabel("Telefon: ");
            JTextField txtTelefon = new JTextField();

            JButton btnSaveClient = new JButton("Salveaza");
            JButton btnCancelClient = new JButton("Renunta");

            btnSaveClient.addActionListener(event -> {
                // Obținerea valorilor introduse în câmpuri
                String nume = txtNume.getText();
                String prenume = txtPrenume.getText();
                String varstaStr = txtVarsta.getText();
                String dataNasterii = txtDataNasterii.getText();
                String telefon = txtTelefon.getText();

                try {
                    int varsta = Integer.parseInt(varstaStr);

                    // Adăugarea clientului utilizând metoda din DBUtils
                    DBUtils.insertClient(nume, prenume, varsta, dataNasterii, telefon);

                    // Afisare mesaj in consola
                    System.out.println("Client adaugat: " + nume + " " + prenume);

                    // Restul codului pentru revenirea la meniul principal
                    f.setContentPane(backgroundLabel);
                    f.revalidate();
                    f.pack();
                    f.setSize(600, 600);
                } catch (NumberFormatException ex) {
                    System.out.println("Varsta trebuie sa fie un numar valid.");
                }
            });

            // Declarați backgroundPanel ca variabilă de instanță în clasa dvs.
            JPanel backgroundPanel = new JPanel(new BorderLayout());

            btnCancelClient.addActionListener(event -> {

                f.setContentPane(backgroundLabel);
                f.revalidate();
                f.pack();
                f.setSize(600, 600);
            });

            backgroundPanel.add(btnCancelClient, BorderLayout.SOUTH);

            f.setContentPane(backgroundPanel);
            f.revalidate();
            f.pack();
            f.setSize(600, 600);


            addClientPanel.add(lblNume);
            addClientPanel.add(txtNume);
            addClientPanel.add(lblPrenume);
            addClientPanel.add(txtPrenume);
            addClientPanel.add(lblVarsta);
            addClientPanel.add(txtVarsta);
            addClientPanel.add(lblDataNasterii);
            addClientPanel.add(txtDataNasterii);
            addClientPanel.add(lblTelefon);
            addClientPanel.add(txtTelefon);
            addClientPanel.add(btnSaveClient);
            addClientPanel.add(btnCancelClient);

            // Adăugarea panoului în fereastra principală
            f.setContentPane(addClientPanel);

            // Resetarea mărimii ferestrei pentru a se potrivi cu noul conținut
            f.pack();
            f.setSize(400, 300); // Modificați dimensiunea după necesitate
            f.setVisible(true);
        });

        menuShowCars.addActionListener(e -> {
            List<Masina> masini = new ArrayList<>();

            try {
                masini = DBUtils.getAllActiveMasini();
            } catch (Exception ex) {
                System.out.println(ex.getMessage());
            }

            if (masini.isEmpty()) {
                System.out.println("Nu există masini în lista activa.");
                return;
            }

            String[] columnNames = {"Marca", "Model", "An fabricatie", "Posesor"};

            Object[][] data = new Object[masini.size()][columnNames.length];

            for (int i = 0; i < masini.size(); i++) {
                Masina masina = masini.get(i);
                data[i][0] = masina.getMarca();
                data[i][1] = masina.getModel();
                data[i][2] = masina.getAn_fabricatie();
                data[i][3] = masina.getPosesor();
            }

            JTable table = new JTable(data, columnNames);

            JPanel backgroundPanel = new JPanel(new BorderLayout()) {
                @Override
                protected void paintComponent(Graphics g) {
                    super.paintComponent(g);
                    ImageIcon backgroundImageIcon = new ImageIcon("C:\\Users\\Alin Sârbu\\Downloads\\service.png");
                    Image backgroundImage = backgroundImageIcon.getImage().getScaledInstance(600, 600, Image.SCALE_SMOOTH);
                    g.drawImage(backgroundImage, 0, 0, this);
                }
            };

            backgroundPanel.add(new JScrollPane(table));

            JButton btnCancel = new JButton("Cancel");

            btnCancel.addActionListener(cancelEvent -> {
                // Închiderea ferestrei și revenirea în meniu
                f.setContentPane(backgroundLabel); // Revenim la imaginea de fundal
                f.revalidate();
                f.pack();
                f.setSize(600, 600);
            });
            backgroundPanel.add(btnCancel, BorderLayout.SOUTH);

            // Adăugăm JPanel personalizat în fereastra principală
            f.setContentPane(backgroundPanel);
            f.revalidate(); // Actualizăm conținutul fereastrei

            // Resetăm mărimea ferestrei pentru a se potrivi cu noul conținut
            f.pack();
            f.setSize(600, 600);
        });

        menuAddCars.addActionListener(e -> {
            // Eliminarea conținutului existent din fereastra principală
            f.getContentPane().removeAll();

            // Adăugarea conținutului specific pentru fereastra de adăugare
            JPanel addClientPanel = new JPanel(new GridLayout(6, 2));

            JLabel lblmarca = new JLabel("Marca: ");
            JTextField txtmarca = new JTextField();

            JLabel lblmodel = new JLabel("Model: ");
            JTextField txtmodel = new JTextField();

            JLabel lblan_fabricatie = new JLabel("Data fabricarii: ");
            JTextField txtan_fabricatie = new JTextField();

            JLabel lblposesor = new JLabel("ID Posesor: ");
            JTextField txtposesor = new JTextField();

            JButton btnSaveClient = new JButton("Salveaza");
            JButton btnCancelClient = new JButton("Renunta");

            btnSaveClient.addActionListener(event -> {
                // Obținerea valorilor introduse în câmpuri
                String marca = txtmarca.getText();
                String model = txtmodel.getText();
                String an_fabricatie = txtan_fabricatie.getText();
                String posesor = txtposesor.getText();

                try {
                    int pos = Integer.parseInt(posesor);

                    // Adăugarea clientului utilizând metoda din DBUtils
                    DBUtils.insertMasini(marca, model, an_fabricatie, Integer.parseInt(posesor));

                    // Afisare mesaj in consola
                    System.out.println("Masina adaugata: " + marca + " " + model);

                    // Restul codului pentru revenirea la meniul principal
                    f.setContentPane(backgroundLabel);
                    f.revalidate();
                    f.pack();
                    f.setSize(600, 600);
                } catch (NumberFormatException ex) {
                    System.out.println("Posesorul trebuie sa fie un numar valid.");
                }
            });

            // Declarați backgroundPanel ca variabilă de instanță în clasa dvs.
            JPanel backgroundPanel = new JPanel(new BorderLayout());

            btnCancelClient.addActionListener(event -> {

                f.setContentPane(backgroundLabel);
                f.revalidate();
                f.pack();
                f.setSize(600, 600);
            });

            backgroundPanel.add(btnCancelClient, BorderLayout.SOUTH);

            f.setContentPane(backgroundPanel);
            f.revalidate();
            f.pack();
            f.setSize(600, 600);


            addClientPanel.add(lblmarca);
            addClientPanel.add(txtmarca);
            addClientPanel.add(lblmodel);
            addClientPanel.add(txtmodel);
            addClientPanel.add(lblan_fabricatie);
            addClientPanel.add(txtan_fabricatie);
            addClientPanel.add(lblposesor);
            addClientPanel.add(txtposesor);
            addClientPanel.add(btnSaveClient);
            addClientPanel.add(btnCancelClient);

            // Adăugarea panoului în fereastra principală
            f.setContentPane(addClientPanel);

            // Resetarea mărimii ferestrei pentru a se potrivi cu noul conținut
            f.pack();
            f.setSize(400, 300); // Modificați dimensiunea după necesitate
            f.setVisible(true);
        });

        menuDeleteCar.addActionListener(e -> {
            List<Masina> masini = new ArrayList<>();

            try {
                masini = DBUtils.getAllActiveMasini();
            } catch (Exception ex) {
                System.out.println(ex.getMessage());
            }

            if (masini.isEmpty()) {
                System.out.println("Nu există mașini în lista activă.");
                return;
            }

            String[] columnNames = {"Marca", "Model", "An fabricatie", "Posesor"};

            Object[][] data = new Object[masini.size()][columnNames.length];

            for (int i = 0; i < masini.size(); i++) {
                Masina masina = masini.get(i);
                data[i][0] = masina.getMarca();
                data[i][1] = masina.getModel();
                data[i][2] = masina.getAn_fabricatie();
                data[i][3] = masina.getPosesor();
            }

            JTable table = new JTable(data, columnNames);

            JPanel backgroundPanel = new JPanel(new BorderLayout()) {
                @Override
                protected void paintComponent(Graphics g) {
                    super.paintComponent(g);
                    ImageIcon backgroundImageIcon = new ImageIcon("C:\\Users\\Alin Sârbu\\Downloads\\service.png");
                    Image backgroundImage = backgroundImageIcon.getImage().getScaledInstance(600, 600, Image.SCALE_SMOOTH);
                    g.drawImage(backgroundImage, 0, 0, this);
                }
            };

            backgroundPanel.add(new JScrollPane(table));

            JButton btnCancel = new JButton("Cancel");

            btnCancel.addActionListener(cancelEvent -> {
                f.setContentPane(backgroundLabel); // Revenim la imaginea de fundal
                f.revalidate();
                f.pack();
                f.setSize(600, 600);
            });

            backgroundPanel.add(btnCancel, BorderLayout.SOUTH);

            JPopupMenu popupMenu = new JPopupMenu();
            JMenuItem deleteMenuItem = new JMenuItem("Șterge");

            List<Masina> final_masini = masini;

            deleteMenuItem.addActionListener(deleteEvent -> {
                int selectedRow = table.getSelectedRow();

                if (selectedRow >= 0 && selectedRow < final_masini.size()) {
                    int masinaId = final_masini.get(selectedRow).getMasinaId();
                    String marca = final_masini.get(selectedRow).getMarca(); // Retine marca

                    try {
                        // Afiseaza un mesaj de confirmare cu marca
                        int dialogResult = JOptionPane.showConfirmDialog(null, "Sigur doriți să ștergeți mașina cu marca: " + marca + "?", "Atenție", JOptionPane.YES_NO_OPTION);

                        if (dialogResult == JOptionPane.YES_OPTION) {
                            System.out.println("Atenție, se șterge!...");
                            DBUtils.deleteMasinaByMarcaProcedure(marca); // Foloseste metoda de stergere dupa marca

                            // Afișează mesaj în consolă
                            System.out.println("Mașina cu marca " + marca + " au fost ștearsă.");

                            // Actualizează lista de mașini după ștergere
                            final_masini.remove(selectedRow);

                            // Obține modelul tabelului și actualizează rândul
                            TableModel model = table.getModel();
                            if (model instanceof DefaultTableModel) {
                                DefaultTableModel defaultModel = (DefaultTableModel) model;
                                defaultModel.removeRow(selectedRow);

                                // Redesenarea tabelului
                                defaultModel.fireTableDataChanged();
                            } else {
                                System.out.println("");
                            }
                        }
                    } catch (Exception ex) {
                        ex.printStackTrace();
                        System.out.println("Eroare la ștergerea mașinii.");
                    }
                } else {
                    System.out.println("Niciun rând selectat sau index invalid.");
                }
            });



            popupMenu.add(deleteMenuItem);

            table.addMouseListener(new MouseAdapter() {
                @Override
                public void mousePressed(MouseEvent e) {
                    if (SwingUtilities.isRightMouseButton(e)) {
                        // Obțineți indexul rândului pe care s-a făcut clic
                        int row = table.rowAtPoint(e.getPoint());

                        // Selectați rândul la care s-a făcut clic
                        table.setRowSelectionInterval(row, row);

                        // Afișați meniul de context la poziția cursorului
                        popupMenu.show(table, e.getX(), e.getY());
                    }
                }
            });

            f.setContentPane(backgroundPanel);
            f.revalidate();
            f.pack();
            f.setSize(600, 600);
        });

        menuShowPersonal.addActionListener(e -> {

            List<Personal> personal_lista = new ArrayList<>();

            try {
                personal_lista = DBUtils.getAllActivePersonal();
            } catch (Exception ex) {
                System.out.println(ex.getMessage());
            }

            if (personal_lista.isEmpty()) {
                System.out.println("Nu există personal angajat.");
                return;
            }

            String[] columnNames = {"Nume", "Prenume", "Functie"};
            Object[][] data = new Object[personal_lista.size()][columnNames.length];

            for (int i = 0; i < personal_lista.size(); i++) {
                Personal personal = personal_lista.get(i);
                data[i][0] = personal.getnume();
                data[i][1] = personal.getprenume();
                data[i][2] = personal.getfunctie();
            }

            JTable table = new JTable(data, columnNames);

            JPanel backgroundPanel = new JPanel(new BorderLayout()) {
                @Override
                protected void paintComponent(Graphics g) {
                    super.paintComponent(g);
                    ImageIcon backgroundImageIcon = new ImageIcon("C:\\Users\\Alin Sârbu\\Downloads\\service.png");
                    Image backgroundImage = backgroundImageIcon.getImage().getScaledInstance(600, 600, Image.SCALE_SMOOTH);
                    g.drawImage(backgroundImage, 0, 0, this);
                }
            };

            backgroundPanel.add(new JScrollPane(table));



            JButton btnCancel = new JButton("Cancel");

            btnCancel.addActionListener(cancelEvent -> {
                // Închiderea ferestrei și revenirea în meniu
                f.setContentPane(backgroundLabel); // Revenim la imaginea de fundal
                f.revalidate();
                f.pack();
                f.setSize(600, 600);
            });
            backgroundPanel.add(btnCancel, BorderLayout.SOUTH);

            // Adăugăm JPanel personalizat în fereastra principală
            f.setContentPane(backgroundPanel);
            f.revalidate(); // Actualizăm conținutul fereastrei

            // Resetăm mărimea ferestrei pentru a se potrivi cu noul conținut
            f.pack();
            f.setSize(600, 600);
        });

        menuAddPersonal.addActionListener(e -> {
            // Eliminarea conținutului existent din fereastra principală
            f.getContentPane().removeAll();

            // Adăugarea conținutului specific pentru fereastra de adăugare
            JPanel addPersonalPanel = new JPanel(new GridLayout(4, 2));

            JLabel lblNume = new JLabel("Nume: ");
            JTextField txtNume = new JTextField();
            JLabel lblPrenume = new JLabel("Prenume: ");
            JTextField txtPrenume = new JTextField();
            JLabel lblFunctie = new JLabel("Functie: ");
            JTextField txtFunctie = new JTextField();


            JButton btnSavePersonal = new JButton("Salveaza");
            JButton btnCancelPersonal = new JButton("Renunta");

            btnSavePersonal.addActionListener(event -> {
                // Obținerea valorilor introduse în câmpuri
                String nume = txtNume.getText();
                String prenume = txtPrenume.getText();
                String functie = txtFunctie.getText();


                // Adăugarea clientului utilizând metoda din DBUtils
                DBUtils.insertPersonal(nume, prenume, functie);

                // Afisare mesaj in consola
                System.out.println("Personal adaugat. Bine ai venit in echipa noastra,  " + nume + " " + prenume);

                // Restul codului pentru revenirea la meniul principal
                f.setContentPane(backgroundLabel);
                f.revalidate();
                f.pack();
                f.setSize(600, 600);

            });

            // Declarați backgroundPanel ca variabilă de instanță în clasa dvs.
            JPanel backgroundPanel = new JPanel(new BorderLayout());

            btnCancelPersonal.addActionListener(event -> {

                f.setContentPane(backgroundLabel);
                f.revalidate();
                f.pack();
                f.setSize(600, 600);
            });

            backgroundPanel.add(btnCancelPersonal, BorderLayout.SOUTH);

            f.setContentPane(backgroundPanel);
            f.revalidate();
            f.pack();
            f.setSize(600, 600);


            addPersonalPanel.add(lblNume);
            addPersonalPanel.add(txtNume);

            addPersonalPanel.add(lblPrenume);
            addPersonalPanel.add(txtPrenume);

            addPersonalPanel.add(lblFunctie);
            addPersonalPanel.add(txtFunctie);

            addPersonalPanel.add(btnSavePersonal);
            addPersonalPanel.add(btnCancelPersonal);

            // Adăugarea panoului în fereastra principală
            f.setContentPane(addPersonalPanel);

            // Resetarea mărimii ferestrei pentru a se potrivi cu noul conținut
            f.pack();
            f.setSize(400, 300); // Modificați dimensiunea după necesitate
            f.setVisible(true);
        });

        menuDeletePersonal.addActionListener(e -> {
            List<Personal> personalList = new ArrayList<>();

            try {
                personalList = DBUtils.getAllActivePersonal();
            } catch (Exception ex) {
                System.out.println(ex.getMessage());
            }

            if (personalList.isEmpty()) {
                System.out.println("Nu există angajați în lista activă.");
                return;
            }

            String[] columnNames = {"Nume", "Prenume", "Functie"};

            Object[][] data = new Object[personalList.size()][columnNames.length];

            for (int i = 0; i < personalList.size(); i++) {
                Personal personal = personalList.get(i);
                data[i][0] = personal.getnume();
                data[i][1] = personal.getprenume();
                data[i][2] = personal.getfunctie();
            }

            JTable table = new JTable(data, columnNames);

            JPanel backgroundPanel = new JPanel(new BorderLayout()) {
                @Override
                protected void paintComponent(Graphics g) {
                    super.paintComponent(g);
                    ImageIcon backgroundImageIcon = new ImageIcon("C:\\Users\\Alin Sârbu\\Downloads\\service.png");
                    Image backgroundImage = backgroundImageIcon.getImage().getScaledInstance(600, 600, Image.SCALE_SMOOTH);
                    g.drawImage(backgroundImage, 0, 0, this);
                }
            };

            backgroundPanel.add(new JScrollPane(table));

            JButton btnCancel = new JButton("Cancel");

            btnCancel.addActionListener(cancelEvent -> {
                f.setContentPane(backgroundLabel);
                f.revalidate();
                f.pack();
                f.setSize(600, 600);
            });

            backgroundPanel.add(btnCancel, BorderLayout.SOUTH);

            JPopupMenu popupMenu = new JPopupMenu();
            JMenuItem deleteMenuItem = new JMenuItem("Șterge");

            List<Personal> finalPersonalList = personalList;

            deleteMenuItem.addActionListener(deleteEvent -> {
                int selectedRow = table.getSelectedRow();

                if (selectedRow >= 0 && selectedRow < finalPersonalList.size()) {
                    String nume = finalPersonalList.get(selectedRow).getnume();
                    String prenume = finalPersonalList.get(selectedRow).getprenume();

                    try {
                        int dialogResult = JOptionPane.showConfirmDialog(
                                null,
                                "Sigur doriți să ștergeți angajatul cu numele: " + nume + " " + prenume + "?",
                                "Atenție",
                                JOptionPane.YES_NO_OPTION
                        );

                        if (dialogResult == JOptionPane.YES_OPTION) {
                            System.out.println("Atenție, se șterge!...");
                            DBUtils.deletePersonalbyName(nume);

                            System.out.println("");

                            finalPersonalList.remove(selectedRow);

                            TableModel model = table.getModel();
                            if (model instanceof DefaultTableModel) {
                                DefaultTableModel defaultModel = (DefaultTableModel) model;
                                defaultModel.removeRow(selectedRow);
                                defaultModel.fireTableDataChanged();
                            } else {
                                System.out.println("Modelul tabelului nu este de tip DefaultTableModel.");
                            }
                        }
                    } catch (Exception ex) {
                        ex.printStackTrace();
                        System.out.println("Eroare la ștergerea angajatului.");
                    }
                } else {
                    System.out.println("Niciun rând selectat sau index invalid.");
                }
            });

            popupMenu.add(deleteMenuItem);

            table.addMouseListener(new MouseAdapter() {
                @Override
                public void mousePressed(MouseEvent e) {
                    if (SwingUtilities.isRightMouseButton(e)) {
                        int row = table.rowAtPoint(e.getPoint());
                        table.setRowSelectionInterval(row, row);
                        popupMenu.show(table, e.getX(), e.getY());
                    }
                }
            });

            f.setContentPane(backgroundPanel);
            f.revalidate();
            f.pack();
            f.setSize(600, 600);
        });




    }
}


