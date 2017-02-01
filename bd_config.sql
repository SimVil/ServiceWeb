
/* -----------------------------------------------------------------------------
* Fichier : bd_config.sql
* Date    : 02/2017
*
* Desc    : def initiale d'une base de donnee pour l'app pokemon tournament.
*           fichier en 2 parties -- creation des tables, remplissage basique.
----------------------------------------------------------------------------- */

/* Creation des tables ------------------------------------------------------ */

create table Pokemon (
    idp     int not null, /* id pokemon */
    nom     varchar(32),  /* nom pokemon */
    vie     int,          /* vie du pokemon */
    force   int,          /* force du pokemon */
    defense int,          /* defense poke */
    type    int,          /* type pokemon (par son id) */
    constraint pk_poke primary key (idp),
    constraint sk_type foreign key (type) references Element(ide)
);


create table Stade (
    ids  int not null, /* id stade */
    nbp  int,          /* nombre places stade */
    nom  varchar(32),  /* nom du stade */
    type int,          /* type terrain (par son id) */
    constraint pk_stade primary key (ids),
    constraint sk_type foreign key (type) references Element(ide)
);


create table Match (
    idm int not null, /* id match (c'est aussi un style de musique) */
    pk1 int,          /* id pokemon 1 */
    pk2 int,          /* id pokemon 2 */
    pkv int,          /* id pokemon vainqueur */
    std int,          /* id stade */
    phs int,          /* id phase */
    constraint pk_match primary key (idm),
    constraint sk_poke1 foreign key (pk1) references Pokemon(idp),
    constraint sk_poke2 foreign key (pk2) references Pokemon(idp),
    constraint sk_phase foreign key (phs) references Phase(idp),
    constraint chk_poke check (pk1 != pk2)
    /* sk_poke1 et sk_poke2 traduisent la composition --> 1 stade
    *  contient 2 pokemons. On le traduit en relationnel avec des
    *  clefs etrangeres
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


create table Element (
    ide  int not null, /* id element */
    type char(20),     /* type element */
    constraint pk_elt primary key (ide, type)
    constraint tp_chk check (type IN {
        'Eau',
        'Feu',
        'Sol',
        'Plante',
        'Electrique',
        'Glace',
        'Vol'
    })
    /* tp_chk restreint le champ d'assignation du type, on
    * fonctionne sur le principe de la liste blanche.
    * * * * * * * * */
);


create table Phase (
    idp int not null, /* id phase */
    typp char(20),    /* type de phase */
    constraint pk_phs primary key (idp, typp)
    constraint ph_chk check (typp IN {
        'HuitiemeFinale',
        'QuartFinale',
        'DemiFinale',
        'Finale'
    })
    /* ph_chk restreint le champ d'assignation du typp, on
    * fonctionne sur le principe de la liste blanche.
    * * * * * * * * */
);


/* Remplissage de base ------------------------------------------------------ */


/* ajout de pokemons */

/* ajout de stades  */

/* ajout de matchs  */

/* ajout d'utilisateurs


















/* EOF ---------------------------------------------------------------------- */
