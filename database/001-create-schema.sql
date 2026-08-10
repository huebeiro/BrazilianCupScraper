CREATE TABLE teams (
	"id" SMALLSERIAL NOT NULL,
	"name" VARCHAR(100) NOT NULL UNIQUE,
	wins SMALLINT NOT NULL CHECK (wins >= 0),
	draws SMALLINT NOT NULL CHECK (draws >= 0), 
	losses SMALLINT NOT NULL CHECK (losses >= 0), 
	goals_for SMALLINT NOT NULL CHECK (goals_for >= 0), 
	goals_against SMALLINT NOT NULL CHECK (goals_against >= 0),
	CONSTRAINT pk_teams PRIMARY KEY ("id")
);

CREATE TABLE matches (
	"id" SMALLSERIAL NOT NULL,
	home_team SMALLINT NOT NULL,
	away_team SMALLINT NOT NULL,
	home_goals SMALLINT NOT NULL CHECK (home_goals >= 0), 
	away_goals SMALLINT NOT NULL CHECK (away_goals >= 0), 
	match_date TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
	CONSTRAINT pk_matches PRIMARY KEY ("id"),
	CONSTRAINT fk_matches_home FOREIGN KEY (home_team) REFERENCES teams("id"),
	CONSTRAINT fk_matches_away FOREIGN KEY (away_team) REFERENCES teams("id"),
	CONSTRAINT ck_matches_different_teams CHECK (home_team <> away_team)
);