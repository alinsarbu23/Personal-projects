import javax.swing.*; //timer si jpanel
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.util.List;

public class Panou extends JPanel {
    private Labirint labirint;
    private boolean animatie_live = false; // la inceput nu ruleaza, ruleaza dupa citire coordonate
    private List<List<Point>> lista_cai;
    private int index_cale_lista = 0;
    private int index_point_cale = 0;
    private Timer timer;
    private boolean bucla_animatie = true;

    public Panou(Labirint labirint, Parcurgere parcurgere) {
        this.labirint = labirint;
        this.lista_cai = parcurgere.get_lista_cai();
        initTimer();
    }

    private void initTimer() {
        int delay = 200; //0.2 s
        timer = new Timer(delay, new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                if (animatie_live) {
                    if (index_cale_lista < lista_cai.size()) {
                        if (index_point_cale < lista_cai.get(index_cale_lista).size()) {
                            repaint();
                            index_point_cale++;
                        } else {

                            index_point_cale = 0;
                            index_cale_lista++;
                        }
                    } else { //repornire lista afisare
                        if (bucla_animatie) {
                            index_point_cale = 0;
                            index_cale_lista = 0;
                        } else {
                            stop_animatie();
                        }
                    }
                }
            }
        });
    }

    public void start_animatie() {

        if (!animatie_live) {
            animatie_live = true;
            index_cale_lista = 0;
            index_point_cale = 0;
            timer.start();
        }
    }

    public void stop_animatie() {
        animatie_live = false;
        timer.stop();
    }

    @Override
    protected void paintComponent(Graphics g) {
        super.paintComponent(g);
        int linii = labirint.get_linii();
        int coloane = labirint.get_coloane();
        int dimensiune_celula = 40;

        for (int i = 0; i < linii; i++) {
            for (int j = 0; j < coloane; j++) {
                int valoare = labirint.get_valoare_pozitie(i, j);
                Color culoare = Color.WHITE;

                if (valoare == 0) {

                    if (i == 0 || i == linii - 1 || j == 0 || j == coloane - 1) {
                        culoare = Color.RED;
                    } else {
                        culoare = Color.BLACK;
                    }

                } else if (valoare == 1) {
                    culoare = Color.WHITE;
                }

                if (labirint.get_start().equals(new Point(i, j))) {
                    culoare = Color.GREEN;
                }

                g.setColor(culoare);
                g.fillRect(j * dimensiune_celula, i * dimensiune_celula, dimensiune_celula, dimensiune_celula);

                g.setColor(Color.BLACK);
                g.drawRect(j * dimensiune_celula, i * dimensiune_celula, dimensiune_celula, dimensiune_celula);
            }
        }

        if (animatie_live && !lista_cai.isEmpty()) {

            if (index_cale_lista < lista_cai.size()) {

                List<Point> cale_curenta = lista_cai.get(index_cale_lista);

                for (int i = 0; i < index_point_cale; i++) {

                    Point punct = cale_curenta.get(i);
                    int x = punct.y * dimensiune_celula;
                    int y = punct.x * dimensiune_celula;
                    g.setColor(Color.GREEN);
                    g.fillRect(x, y, dimensiune_celula, dimensiune_celula);

                }
            }
        }
    }
}