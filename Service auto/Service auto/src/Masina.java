public class Masina {
    private int MasinaId;
    private String marca;
    private String model;
    private String an_fabricatie;
    private String posesor;
    private String activ;
    public Masina(String marca, String model, String an_fabricatie, String posesor, String activ) {
        this.marca = marca;
        this.model = model;
        this.an_fabricatie = an_fabricatie;
        this.posesor = posesor;
        this.activ = activ;
    }
    public int getMasinaId() {
        return MasinaId;
    }
    public String getMarca() {
        return marca;
    }
    public String getModel() {
        return model;
    }
    public String getAn_fabricatie() {
        return an_fabricatie;
    }
    public String getPosesor() {
        return posesor;
    }
}
