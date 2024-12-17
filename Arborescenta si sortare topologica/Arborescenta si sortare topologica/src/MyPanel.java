import java.awt.Color;
import java.awt.Graphics;
import java.awt.Point; //urmareste pozitia mouse-ului
import java.awt.event.MouseAdapter;
import java.awt.event.MouseEvent; //cele doua sunt legate de evenimentele cu privire la mouse
import java.awt.event.MouseMotionAdapter;
import java.util.Vector;
import javax.swing.JPanel; //creeaza pe panou obiectele grafice
import java.io.BufferedWriter; //buffer intern de colectare a datelor
import java.io.FileWriter; //scriere de date in fisier text
import java.io.IOException;
import javax.swing.JButton; //butoane grafice
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import javax.swing.JToggleButton; //double task
import javax.swing.ButtonGroup;
import java.util.Stack;

public class MyPanel extends JPanel {
    private int numar_noduri = 1; //numarul de noduri (primul va fi 1)
    private final int diametruNod = 50;
    private final Vector<Node> listaNoduri;
    private final Vector<Arc> listaArce;
    private Point pointStart = null;
    private Point pointEnd = null; //linie intermediara
    private boolean isDragging = false;
    private Node nod_selectat = null;
    private Node nod_temporar = null;
    private final JToggleButton buton_adaugare_nod;
    private final JToggleButton buton_adaugare_arc;
    private boolean adaugaNod = true; //operatie ce indica daca se adauga nod la operatiunea curenta

    private boolean este_orientat = true;
    private boolean noduriSuprapuse = false;
    private boolean aciclic = true;
    private boolean este_quasi_tare_conex = true;
    private Node radacina;
    private JButton buton_sortare_topologica;
    private final Node nod_tras = null; //coordonate nod tras

    public MyPanel() {
        listaNoduri = new Vector<Node>();
        listaArce = new Vector<Arc>();
        setBackground(Color.YELLOW);

        buton_adaugare_nod = new JToggleButton("Adaugă Nod");
        buton_adaugare_nod.setSelected(true);
        buton_adaugare_nod.addActionListener(new ActionListener() {
            public void actionPerformed(ActionEvent e) {
                adaugaNod = true;
                buton_adaugare_arc.setSelected(false); //switch intre butoane -arc-nod
            }
        });

        buton_adaugare_arc = new JToggleButton("Adaugă Arc");
        buton_adaugare_arc.addActionListener(new ActionListener() {
            public void actionPerformed(ActionEvent e) {
                adaugaNod = false;
                buton_adaugare_nod.setSelected(false);
                nod_temporar = null;
                pointStart = null;
            }
        });

        ButtonGroup buttonGroup = new ButtonGroup();
        buttonGroup.add(buton_adaugare_nod); //adaugare colectiva
        buttonGroup.add(buton_adaugare_arc);

        add(buton_adaugare_nod); //adaugare pe panou
        add(buton_adaugare_arc);

        addMouseListener(new MouseAdapter() {
            public void mousePressed(MouseEvent e) {
                if (adaugaNod) { //verificare buton apasat
                    for (Node node : listaNoduri) {
                        int nodeX = node.getCoordX();
                        int nodeY = node.getCoordY();
                        int raza = diametruNod / 2;

                        if (Math.abs(e.getX() - nodeX) <= raza && Math.abs(e.getY() - nodeY) <= raza) {
                            nod_selectat = node;
                            break;
                        }
                    }

                    if (nod_selectat == null) {
                        int centerX = e.getX() - diametruNod / 2;
                        int centerY = e.getY() - diametruNod / 2;


                        if (!adaugare_nod(centerX, centerY)) {
                            System.out.println("Nodurile se suprapun. Introduceți noi coordonate.");
                        }
                    }

                } else {
                    for (Node node : listaNoduri) {
                        int nodeX = node.getCoordX();
                        int nodeY = node.getCoordY();
                        int raza = diametruNod / 2;

                        if (Math.abs(e.getX() - nodeX) <= raza && Math.abs(e.getY() - nodeY) <= raza) {
                            nod_selectat = node;
                            break;
                        }
                    }

                    if (nod_selectat != null) {
                        nod_temporar = nod_selectat;
                        pointStart = e.getPoint();
                    }
                }
            }

            public void mouseReleased(MouseEvent e) {

                if (nod_tras != null) {
                    // Dacă avem un nod trăgător, actualizăm poziția acestuia la eliberarea mouse-ului
                    nod_tras.setCoordX(e.getX() - diametruNod / 2);
                    nod_tras.setCoordY(e.getY() - diametruNod / 2);
                }


                if (!isDragging) {
                    int centerX = e.getX() - diametruNod / 2;
                    int centerY = e.getY() - diametruNod / 2;
                    adaugare_nod(centerX, centerY);


                } else if (nod_selectat != null) { //verificare nod destinatie
                    Node endNode = null;
                    int endX = e.getX();
                    int endY = e.getY();

                    for (Node node : listaNoduri) {
                        int nodeX = node.getCoordX();
                        int nodeY = node.getCoordY();
                        int raza = diametruNod / 2;

                        if (Math.abs(endX - nodeX) <= raza && Math.abs(endY - nodeY) <= raza && node != nod_selectat) {
                            endNode = node;
                            break;
                        }
                    }

                    if (endNode != null) {
                        Arc arc = new Arc(nod_selectat, endNode, este_orientat);
                        listaArce.add(arc);
                    }
                    nod_selectat = null;
                }
                pointStart = null;
                isDragging = false;
                repaint();
            }
        });


        addMouseMotionListener(new MouseMotionAdapter() {
            public void mouseDragged(MouseEvent e) {
                pointEnd = e.getPoint();
                isDragging = true;
                repaint();
            }
        });


        addMouseMotionListener(new MouseMotionAdapter() {
            public void mouseDragged(MouseEvent e) {
                if (nod_selectat != null && adaugaNod) { //adaugarea nodului
                    nod_selectat.setCoordX(e.getX() - diametruNod / 2);
                    nod_selectat.setCoordY(e.getY() - diametruNod / 2);
                    repaint();
                } else if (!adaugaNod && nod_selectat != null && pointStart != null) { //adaugarea arcului
                    pointEnd = e.getPoint();
                    repaint();
                }
            }
        });


        JButton toggleButton = new JButton("Comută tipul de graf");
        toggleButton.addActionListener(new ActionListener() {
            public void actionPerformed(ActionEvent e) {
                este_orientat = !este_orientat;

                if (este_orientat) {
                    toggleButton.setText("Graf Orientat");
                } else {
                    toggleButton.setText("Graf Neorientat");
                }
                repaint(); //redesenare modificari
            }
        });
        add(toggleButton);


        JButton buton_sortare_topologica = new JButton("Sortare Topologică");
        buton_sortare_topologica.addActionListener(new ActionListener() {
            public void actionPerformed(ActionEvent e) {
                sortare_topologica(radacina);
            }
        });

        add(buton_sortare_topologica);

        JButton butonVerificare = new JButton("Verificare Arborescenta");
        butonVerificare.addActionListener(new ActionListener() {
            public void actionPerformed(ActionEvent e) {
                verificare_arborescenta();
                repaint();
            }
        });
        add(butonVerificare);
    }


    public void sortare_topologica(Node radacina) {

        if (numar_noduri == 0) {
            System.out.println("Graful nu contine niciun nod. Nu se poate efectua sortarea topologica.");
            return;
        }

        Vector<Boolean> visited = new Vector<>(numar_noduri);

        for (int i = 0; i < numar_noduri; i++) {
            visited.add(false);
        }

        Stack<Node> noduri_nevizitate = new Stack<>();
        Stack<Node> noduri_sortate = new Stack<>();

        noduri_nevizitate.push(radacina);

        while (!noduri_nevizitate.isEmpty()) {
            Node current = noduri_nevizitate.pop();
            int index_nod = listaNoduri.indexOf(current);

            if (!visited.get(index_nod)) {

                visited.set(index_nod, true);

                for (Arc arc : listaArce) {
                    if (arc.getStartNode().equals(current)) {
                        Node endNode = arc.getEndNode();
                        int endIndex = listaNoduri.indexOf(endNode);

                        if (!visited.get(endIndex)) {
                            noduri_nevizitate.push(endNode);
                        } else if (noduri_sortate.contains(endNode)) {
                            aciclic = false;
                            System.out.println("Graful contine cicluri. Sortarea topologica nu este posibila.");
                            return;
                        }
                    }
                }

                noduri_sortate.push(current);
            }
        }

        aciclic = true;
        System.out.print("Sortare Topologică: ");
        while (!noduri_sortate.isEmpty()) {
            Node node = noduri_sortate.pop();
            System.out.print(node.getNumber() + " ");
        }
        System.out.println();
    }


    public Node identifica_radacina() {

        for (Node node : listaNoduri) {

            boolean este_radacina = true;

            for (Arc arc : listaArce) {
                if (arc.getEndNode().equals(node)) {
                    este_radacina = false;
                    break;
                }
            }
            if (este_radacina) {
                radacina = node; 
                return node;
            }
        }
        return null;
    }


    public void verificare_quasi_tare_conex() {
        Node radacina = identifica_radacina();

        if (radacina == null) {
            este_quasi_tare_conex = false;
        }

        if (listaNoduri.size() < 2) {
            este_quasi_tare_conex = false;
            return;
        }

        for (Node nod : listaNoduri) {

            if (nod != radacina) {

                boolean areConexiune = false;
                boolean esteFrunza = true;

                for (Arc arc : listaArce) {

                    if (arc.getStartNode().equals(nod) || arc.getEndNode().equals(nod)) {

                        areConexiune = true;
                        if (arc.getEndNode().equals(nod)) {
                            esteFrunza = false; //
                        }
                    }
                }

                if (esteFrunza) {

                    boolean drumInversLaRadacina = false;

                    Node nodCurent = nod;

                    while (!nodCurent.equals(radacina)) {

                        boolean drumGasit = false;

                        for (Arc arc : listaArce) {
                            if (arc.getEndNode().equals(nodCurent) && arc.getStartNode().equals(radacina)) {
                                drumGasit = true;
                                nodCurent = arc.getStartNode(); // Ne deplasăm către nodul de start al arcului găsit
                                break;
                            }
                        }
                        if (!drumGasit) {
                            break;
                        }
                        if (nodCurent.equals(radacina)) {
                            drumInversLaRadacina = true;
                        }
                    }

                    if (!drumInversLaRadacina) {
                        este_quasi_tare_conex = false;
                        return;
                    }
                }

                if (!areConexiune) {
                    este_quasi_tare_conex = false;
                    return;
                }
            }
        }

        este_quasi_tare_conex = true;
    }

    public void verifica_ciclicitate() {
        boolean[] vizitat = new boolean[numar_noduri];

        for (Node nod : listaNoduri) {

            int index = listaNoduri.indexOf(nod);

            if (!vizitat[index]) {

                if (contine_ciclu(nod, vizitat)) {
                    aciclic = false;
                    return;
                }
            }
        }
        aciclic = true;
    }

    private boolean contine_ciclu(Node startNode, boolean[] vizitat) {

        Stack<Node> stiva= new Stack<>();
        Stack<Integer> index_stiva= new Stack<>();

        stiva.push(startNode);

        index_stiva.push(listaNoduri.indexOf(startNode));

        vizitat[listaNoduri.indexOf(startNode)] = true;

        while (!stiva.isEmpty()) {
            Node nodCurent = stiva.pop();

            for (Arc arc : listaArce) {
                if (arc.getStartNode().equals(nodCurent)) {
                    Node endNode = arc.getEndNode();
                    int indexEndNode = listaNoduri.indexOf(endNode);

                    if (!vizitat[indexEndNode]) {

                        stiva.push(endNode);
                        index_stiva.push(indexEndNode);
                        vizitat[indexEndNode] = true;

                    } else {
                        return true;
                    }
                }
            }
        }

        return false;
    }

    private void coloreazaRadacina(Graphics g) {
        Node radacina = identifica_radacina(); // Identifică rădăcina înainte de colorare

        if (radacina != null) {
            g.setColor(Color.GREEN);
            g.fillOval(radacina.getCoordX(), radacina.getCoordY(), diametruNod, diametruNod);
            g.setColor(Color.WHITE);
            g.drawOval(radacina.getCoordX(), radacina.getCoordY(), diametruNod, diametruNod);
            g.setColor(Color.BLACK);
            g.drawString(((Integer) radacina.getNumber()).toString(), radacina.getCoordX() + 13, radacina.getCoordY() + 20);
        }
    }

    public void verificare_arborescenta() {
        Node radacina = identifica_radacina();

        if (radacina != null) {
            System.out.println("Nodul radacina: " + radacina.getNumber());

            verifica_ciclicitate();
            verificare_quasi_tare_conex();

            if (aciclic && este_quasi_tare_conex) {
                System.out.println("Graful este arborescent.");
            } else {
                System.out.println("Graful nu este arborescent.");

                if (!aciclic) {
                    System.out.println("Motivul: Graful contine cicluri.");
                }

                if (!este_quasi_tare_conex) {
                    System.out.println("Motivul: Graful nu este quasi-tare conex.");
                }
            }
        } else {
            System.out.println("Nu s-a putut identifica un nod radacina.");
        }
    }


    protected void paintComponent(Graphics g) {
        super.paintComponent(g); //metoda a clasei parinte jpanel

        for (Arc arc : listaArce) {
            g.setColor(arc.isValid() ? Color.black : Color.RED); // Setez culoarea dupa validare
            Node startNode = arc.getStartNode();
            Node endNode = arc.getEndNode();
            int startX = startNode.getCoordX() + diametruNod/2; //sau metoda getRaza();
            int startY = startNode.getCoordY() + diametruNod / 2;
            int endX = endNode.getCoordX() + diametruNod / 2;
            int endY = endNode.getCoordY() + diametruNod / 2;

            desenare_arc(g, startX, startY, endX, endY);
        }

        if (pointStart != null) {
            g.setColor(Color.RED);
            g.drawLine(pointStart.x, pointStart.y, pointEnd.x, pointEnd.y);
        }

        for (int i = 0; i < listaNoduri.size(); i++) {
            listaNoduri.elementAt(i).desenare_nod(g, diametruNod);
        }

        coloreazaRadacina(g);
    }


    private void desenare_arc(Graphics g, int x1, int y1, int x2, int y2) {
        int dim_arc = 35;

        g.drawLine(x1, y1, x2, y2);

        if (este_orientat) {
            // Dacă graful este orientat, desenați săgeți
            double unghi_intre_noduri = Math.atan2(y2 - y1, x2 - x1);
            double lungime = Math.sqrt(Math.pow(x2 - x1, 2) + Math.pow(y2 - y1, 2));

            int arrowX = (int) (x1 + lungime * Math.cos(unghi_intre_noduri)); //coordonate varf sageata
            int arrowY = (int) (y1 + lungime * Math.sin(unghi_intre_noduri));

            double unghi_intre_noduri1 = unghi_intre_noduri - Math.toRadians(20);
            double unghi_intre_noduri2 = unghi_intre_noduri + Math.toRadians(20); //coordonate extremitate sageata

            int x3 = (int) (arrowX - dim_arc * Math.cos(unghi_intre_noduri1));
            int y3 = (int) (arrowY - dim_arc * Math.sin(unghi_intre_noduri1)); //lungime semi varf stanga

            int x4 = (int) (arrowX - dim_arc * Math.cos(unghi_intre_noduri2));
            int y4 = (int) (arrowY - dim_arc * Math.sin(unghi_intre_noduri2)); //lungime semi varf dreapta

            g.drawLine(arrowX, arrowY, x3, y3);
            g.drawLine(arrowX, arrowY, x4, y4);
        } else {
            // Dacă graful este neorientat
            g.drawLine(x1, y1, x2, y2);
        }
    }


    private boolean verificare_suprapunere_noduri(int x, int y) {
        int margine = diametruNod; // marginea cercului

        for (Node nod_lista : listaNoduri) {
            int nodeX = nod_lista.getCoordX();
            int nodeY = nod_lista.getCoordY();
            int raza = diametruNod / 2;

            if (Math.abs(x - nodeX) <= raza + margine && Math.abs(y - nodeY) <= raza + margine) { //margine + raza pentru  a se vedea si linia
                return true;
            }
        }
        return false;
    }


    private boolean adaugare_nod(int x, int y) {
        if (verificare_suprapunere_noduri(x, y)) {
            return false;
        } else {
            noduriSuprapuse = false;
            Node node = new Node(x, y, numar_noduri);
            listaNoduri.add(node);
            numar_noduri++;
            repaint();
            return true; // Returnăm true dacă nodul a fost adăugat cu succes
        }
    }

    public void matrice_adiacenta(String fisier) throws IOException {
        int numar_noduri = listaNoduri.size();
        int[][] matrice = new int[numar_noduri][numar_noduri];

        for (Arc arc : listaArce) {
            Node nod_start = arc.getStartNode();
            Node nod_end = arc.getEndNode();
            int start_index = listaNoduri.indexOf(nod_start);
            int end_index = listaNoduri.indexOf(nod_end);

            if (start_index != -1 && end_index != -1) { //nodurile exista in lista de noduri
                matrice[start_index][end_index] = 1;
                if (!este_orientat) {
                    matrice[end_index][start_index] = 1;
                }
            }
        }

        BufferedWriter writer = new BufferedWriter(new FileWriter(fisier));

        writer.write(String.valueOf(numar_noduri));
        writer.newLine();

        for (int i = 0; i < numar_noduri; i++) {
            for (int j = 0; j < numar_noduri; j++) {
                writer.write(String.valueOf(matrice[i][j]));
                if (j < numar_noduri - 1) {
                    writer.write(" ");
                }
            }
            writer.newLine();
        }

        writer.close();
    }

}