using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OWRichPresence
{
    public enum ImageKey
    {
        // Vanilla
        ashtwin,
        attlerock,
        brittlehollow,
        darkbramble,
        dreamworld,
        embertwin,
        eyeoftheuniverse,
        giantsdeep,
        hollowslantern,
        hourglasstwins,
        interloper,
        orbitalprobecannon,
        outerwilds,
        quantummoon,
        ship,
        skyshutter,
        stranger,
        sun,
        sunstation,
        timberhearth,
        whitehole,

        //Carson
        carson = 50,
        alpinia,
        dester,
        draeo,
        ekbur,
        glaze,
        graebur,
        loon,
        mirtha,
        platinumhue,
        pyer,
        scarletstorm,
        urath,
        volcus,

        //Astral Codec
        lingeringchime = 95,

        //New Horizons
        newhorizons = 96,
        defaultplanet,
        defaultplanetatmosphere,
        defaultstar,

        //New Horizons Examples
        darkgateway = 100,
        dauntingconfidant,
        devilsmaw,
        frigidpygmy,
        giantsdeepexamples,
        lavatwin,
        viridianflow = 106,
        lava1 = 106,
        jademaw = 107,
        lava2 = 107,
        lavatwins,
        lightgateway,
        lunalure,
        nightlight,
        ringedjewel,
        sequesteredluminary,
        snowball,
        terralure,
        wetrock,
        hinderingskies,
        hymn,
        waltz,
        elegy,
        requiem,
        rondo,
        puzzlestation,

        //TRAPPIST-1
        trappist1 = 190,
        boilingfire,
        cloudyskies,
        deepwaters,
        everlastingsunset,
        frigidseas,
        glacialsteam,
        hollowice,

        //RSS
        barnardsstar = 200,
        barnardsstarb,
        callisto,
        ceres,
        charon,
        deimos,
        earth,
        europa,
        ganymede,
        halleyscomet,
        hydra,
        io,
        jupiter,
        kerberos,
        luhman16a,
        luhman16b,
        mars,
        mercury,
        themoon,
        neptune,
        nix,
        perdition,
        phobos,
        pluto,
        proteus,
        proximab,
        proximac,
        proximacentauri,
        puck,
        rigilkentaurus,
        saturn,
        sol,
        styx,
        titan,
        titania,
        toliman,
        triton,
        uranus,
        venus,
        vesta,

        //Upsilon Andromedae
        titawin = 6624,
        upsilonandromedaeb,

        //Outsider
        darkbrambleoutsider = 7288,
        powerstation,

        //Hearth's Neighbor
        neighborsun = 9976,
        lonelyhermit,
        alpinecore,
        lakecore,
        lavacore,
        derelictship,

        //Tesseract's Secret
        tesseract = 2226,

        //Evacuation
        thecampground = 3822,
        layeredlagoon,
        smolderinggulch,
        twilightfrost,
        spark,
        datacorrupted,

        //Unnamed Mystery
        aetherion = 8669,
        zephyria,
        thalassia,
        ginth,
        helioplasmia,
        metallos,
        electrum,
        outerspacestation,
        ghostmoon,

        //Separate section just for Jam 3 mods
        //Mod Jam 3
        jam3sun = 3526,
        starshipcommunity,

        //Axiom's Refuge
        axiom,
        brokensatellite,
        aicale,

        //Callis's Thesis
        theboiledegg,

        //Echo Hike
        echohike,

        //Finis
        finisplateau,

        //Granite's Invitation
        gravelrock,

        //Hearth's Neighbor 2: Magistarium
        magistarium,

        //Mod Jam Hub
        modjamhub,

        //Band Together
        fracturedharmony,

        //Solar Rangers
        eggstar,
        
        //Reflections
        forlornproject,

        //Symbiosis
        symbiosis = 3540,
        altth = 3540,

        //Fret's Quest
        whitesun = 3738,
        themagicbanjo,
        loststrings,
        greenbase,
        bracketsrest,
        driedtears,

        // Jam 5
        centralstation = 5265,

        // Project ARC
        verdantbeacon,
        emeraldreverie,
        anomalystation,

        // Heliostudy
        heliocenter = 5270,
        walker_jam5_star = 5270,
        shatteredgeode = 5271,
        walker_jam5_planet1 = 5271,
        thebigone = 5272,
        walker_jam5_planet2 = 5272,
        daucus = 5273,
        walker_jam5_planet3 = 5273,
        glacialabyss = 5274,
        walker_jam5_planet4 = 5274,
        orrerystation = 5275,
        walker_jam5_station = 5275,

        // Powerfail
        bruisedbrother,
        familliarrefuse,
        refuseslightbulb,
        sunsetsister,
        bruisedbrotherasteroid10,

        // Diorama
        dioramainterface,

        // Cosmic Curators
        cosmiccurators_astralbody_silver_lining,
        cosmiccurators_astralbody_scaled_museum,
        cosmiccurators_astralbody_ominous_orbiter,

        // Core Collapse
        theastrophytum,
        hiddenlight,

        // Under a Radio Moon
        radiomoon = 5400,
        t0187 = 5400,

        // On A Rail
        stellarexpress = 5500,
        thestellarexpress = 5500,
        frostcar,
        locomocean,
        sprucecaboose,

        // Dreambound
        ringedplanet = 7248,

        // Guardian
        suncore = 8273,

        //The Stranger They Are
        anglerseye = 8782,
        nearestneighbor,
        sizzlingsands,
        ringedgiant,
        ringedlaboratory,
        strangershomeworld,
        velvetvortex,
        burningbombardier,
        distantenigma,
        strandedvessel,
        strangersprobe

        //And any more that add icons in future or never
    }
}
