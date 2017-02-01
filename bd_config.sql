create table Pokemon (
    idp     int not null, /* id pokemon */
    nom     varchar(32),  /* nom pokemon */
    vie     int,          /* vie du pokemon */
    force   int,          /* force du pokemon */
    defense int,          /* defense poke */
    /* un type a mettre pour le type de pokemon */
    constraint pk_pokemon primary key (idp)
);

create table Stade (
    ids int not null, /* id stade */
    nbp int,          /* nombre places stade */
    nom varchar(32),  /* nom du stade */
    /* un type a mettre pour le type de stade */
    constraint pk_stade primary key (ids)

);

create table Match (
    idm int not null, /* id match (c'est aussi un style de musique) */
    pk1 int,          /* id pokemon 1 */
    pk2 int,          /* id pokemon 2 */
    pkv int,          /* id pokemon vainqueur */
    std int,          /* id stade */
    /* un type a mettre pour les phases */
    constraint pk_match primary key (idm),
    constraint sk_poke1 foreign key (pk1) references Pokemon(idp),
    constraint sk_poke2 foreign key (pk2) references Pokemon(idp),
    constraint chk_poke check (pk1 != pk2)

    /* sk_poke1 et sk_poke2 traduisent la composition --> 1 stade
    *  contient deux pokemons. On le traduit en relationnel avec
    *  des clefs etrangeres
    * -------------
    *  chk_poke assure qu'on ne peut avoir un match d'un pokemon
    *  contre lui-meme
    * * * * * * * * * * * * * * * * * */


);

create table Utilisateur (
    idu      int not null, /* id user */
    nom      varchar(32),  /* nom user */
    prenom   varchar(32),  /* prenom user */
    login    varchar(32),  /* login user */
    password varchar(32),  /* mdp user */
    constraint pk_utilisateur primary key (idu)

);
