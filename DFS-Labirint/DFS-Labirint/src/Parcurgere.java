import java.awt.Point;
import java.util.ArrayList;
import java.util.List;

public class Parcurgere {
    private Labirint labirint;
    private List<List<Point>> listaCai;

    public Parcurgere(Labirint labirint) {
        this.labirint = labirint;
        this.listaCai = new ArrayList<>();
        gaseste_cai(labirint.get_start().x, labirint.get_start().y, new ArrayList<>());
    }

    public List<List<Point>> get_lista_cai() {
        return listaCai;
    }

    private void gaseste_cai(int x, int y, List<Point> cale_curenta) {

        if (x < 0 || x >= labirint.get_linii() || y < 0 || y >= labirint.get_coloane()) {

            // Am atins o margine, adăugați calea curentă la listă
            listaCai.add(new ArrayList<>(cale_curenta));
            return;
        }

        if (labirint.este_zid(x, y) || labirint.get_valoare_pozitie(x, y) == 2) {
            return;
        }

        //marcare
        labirint.marcheaza_punct(new Point(x, y));
        cale_curenta.add(new Point(x, y));


        int[] dx = {1, -1, 0, 0};
        int[] dy = {0, 0, 1, -1};

        for (int i = 0; i < 4; i++) {

            int x_vecin = x + dx[i];
            int y_vecin = y + dy[i];
            gaseste_cai(x_vecin, y_vecin, cale_curenta);
        }


        labirint.demarcheaza_punct(new Point(x, y));
        cale_curenta.remove(cale_curenta.size() - 1);
    }

}