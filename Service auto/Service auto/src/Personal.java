public class Personal {
    private int PersonalId;
    private String nume;
    private String prenume;
    private String functie;
    private String activ;
    public Personal(String nume, String prenume, String functie, String activ) {
        this.nume = nume;
        this.prenume = prenume;
        this.functie = functie;
        this.activ = activ;
    }
    public int getPersonalId() {
        return PersonalId;
    }
    public String getnume() {
        return nume;
    }
    public String getprenume() {
        return prenume;
    }
    public String getfunctie() {
        return functie;
    }
}
