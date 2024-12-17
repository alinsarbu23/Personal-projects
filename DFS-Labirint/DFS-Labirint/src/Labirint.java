import java.awt.Point;
import java.io.BufferedReader; //citire fisier
import java.io.IOException;
import java.util.Scanner;
import java.io.File;

public class Labirint {
    private int[][] matrice;
    private Point start;

    public Labirint(String cale_fisier) {
        citire_matrice(cale_fisier);
    }


    public int get_coloane() {
        return matrice[0].length;
    }

    public int get_linii() {
        return matrice.length;
    }

    public int get_valoare_pozitie(int x, int y) {
        return matrice[x][y];
    }

    boolean este_zid(int x, int y) {
        return matrice[x][y] == 0;
    }


    public void set_start(Point start) {
        this.start = start;
    }

    public Point get_start() {
        return start;
    }

    public void marcheaza_punct(Point pozitie) {
        matrice[pozitie.x][pozitie.y] = 2; // punct vizitat
    }

    public void demarcheaza_punct(Point pozitie) {
        matrice[pozitie.x][pozitie.y] = 1;
    }

    public void citire_matrice(String cale_fisier) {
        File fisier = new File(cale_fisier);

        try {
            Scanner read = new Scanner(fisier);

            String[] dimensiuni_matrice = read.nextLine().split(" ");

            int linii = Integer.parseInt(dimensiuni_matrice[0]);
            int coloane = Integer.parseInt(dimensiuni_matrice[1]);

            matrice = new int[linii][coloane];

            for (int i = 0; i < linii; i++) {
                String linie = read.nextLine();
                String[] valori_linie = linie.split(" ");

                for (int j = 0; j < coloane; j++) {
                    matrice[i][j] = Integer.parseInt(valori_linie[j]);
                }
            }

            read.close();
        }
        catch (IOException e) {
            e.printStackTrace();
        }
    }

}