namespace IBDatabase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContractQuotes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ConId = c.Int(nullable: false),
                        ContractName = c.String(maxLength: 512),
                        Strike = c.Double(),
                        Expiry = c.String(maxLength: 10),
                        Right = c.String(maxLength: 4),
                        localSymbol = c.String(maxLength: 128),
                        bidSize = c.Decimal(precision: 18, scale: 2),
                        bidPrice = c.Decimal(precision: 18, scale: 2),
                        askPrice = c.Decimal(precision: 18, scale: 2),
                        askSize = c.Decimal(precision: 18, scale: 2),
                        lastPrice = c.Decimal(precision: 18, scale: 2),
                        lastSize = c.Decimal(precision: 18, scale: 2),
                        high = c.Decimal(precision: 18, scale: 2),
                        low = c.Decimal(precision: 18, scale: 2),
                        volume = c.Int(nullable: false),
                        close = c.Decimal(precision: 18, scale: 2),
                        bidOptComp = c.Decimal(precision: 18, scale: 2),
                        askOptComp = c.Decimal(precision: 18, scale: 2),
                        lastOptComp = c.Decimal(precision: 18, scale: 2),
                        modelOptComp = c.Decimal(precision: 18, scale: 2),
                        open = c.Decimal(precision: 18, scale: 2),
                        WeekLow13 = c.Decimal(precision: 18, scale: 2),
                        WeekHigh13 = c.Decimal(precision: 18, scale: 2),
                        WeekLow26 = c.Decimal(precision: 18, scale: 2),
                        WeekHigh26 = c.Decimal(precision: 18, scale: 2),
                        WeekLow52 = c.Decimal(precision: 18, scale: 2),
                        WeekHigh52 = c.Decimal(precision: 18, scale: 2),
                        AvgVolume = c.Decimal(precision: 18, scale: 2),
                        OpenInterest = c.Decimal(precision: 18, scale: 2),
                        OptionHistoricalVolatility = c.Decimal(precision: 18, scale: 2),
                        OptionImpliedVolatility = c.Decimal(precision: 18, scale: 2),
                        OptionBidExchStr = c.Decimal(precision: 18, scale: 2),
                        OptionAskExchStr = c.Decimal(precision: 18, scale: 2),
                        OptionCallOpenInterest = c.Decimal(precision: 18, scale: 2),
                        OptionPutOpenInterest = c.Decimal(precision: 18, scale: 2),
                        OptionCallVolume = c.Decimal(precision: 18, scale: 2),
                        OptionPutVolume = c.Decimal(precision: 18, scale: 2),
                        IndexFuturePremium = c.Decimal(precision: 18, scale: 2),
                        bidExch = c.Decimal(precision: 18, scale: 2),
                        askExch = c.Decimal(precision: 18, scale: 2),
                        auctionVolume = c.Decimal(precision: 18, scale: 2),
                        auctionPrice = c.Decimal(precision: 18, scale: 2),
                        auctionImbalance = c.Decimal(precision: 18, scale: 2),
                        markPrice = c.Decimal(precision: 18, scale: 2),
                        bidEFP = c.Decimal(precision: 18, scale: 2),
                        askEFP = c.Decimal(precision: 18, scale: 2),
                        lastEFP = c.Decimal(precision: 18, scale: 2),
                        openEFP = c.Decimal(precision: 18, scale: 2),
                        highEFP = c.Decimal(precision: 18, scale: 2),
                        lowEFP = c.Decimal(precision: 18, scale: 2),
                        closeEFP = c.Decimal(precision: 18, scale: 2),
                        lastTimestamp = c.Long(nullable: false),
                        shortable = c.Decimal(precision: 18, scale: 2),
                        fundamentals = c.Decimal(precision: 18, scale: 2),
                        RTVolume = c.Decimal(precision: 18, scale: 2),
                        halted = c.Int(nullable: false),
                        bidYield = c.Decimal(precision: 18, scale: 2),
                        askYield = c.Decimal(precision: 18, scale: 2),
                        lastYield = c.Decimal(precision: 18, scale: 2),
                        custOptComp = c.Decimal(precision: 18, scale: 2),
                        trades = c.Decimal(precision: 18, scale: 2),
                        trades_min = c.Decimal(precision: 18, scale: 2),
                        volume_min = c.Decimal(precision: 18, scale: 2),
                        lastRTHTrade = c.Decimal(precision: 18, scale: 2),
                        regulatoryImbalance = c.Decimal(precision: 18, scale: 2),
                        LocalTransactionTime = c.DateTime(nullable: false),
                        DbContract_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DBContracts", t => t.DbContract_Id)
                .Index(t => t.ConId, unique: true)
                .Index(t => t.ConId, name: "IX_ContractQuoteConIdIndex")
                .Index(t => t.DbContract_Id);
            
            CreateTable(
                "dbo.DBContracts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ConId = c.Int(nullable: false),
                        Symbol = c.String(maxLength: 10),
                        SecType = c.String(maxLength: 10),
                        Expiry = c.String(maxLength: 10),
                        Strike = c.Double(nullable: false),
                        Right = c.String(maxLength: 4),
                        Multiplier = c.String(maxLength: 20),
                        Exchange = c.String(maxLength: 50),
                        Currency = c.String(maxLength: 3),
                        LocalSymbol = c.String(maxLength: 512),
                        PrimaryExch = c.String(maxLength: 50),
                        TradingClass = c.String(maxLength: 10),
                        IncludeExpired = c.Boolean(nullable: false),
                        SecIdType = c.String(maxLength: 128),
                        SecId = c.String(maxLength: 128),
                        ComboLegsDescription = c.String(maxLength: 512),
                        ComboLegs = c.String(),
                        UnderComp = c.String(),
                        EntityType = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.ConId, unique: true)
                .Index(t => t.ConId, name: "IX_ContractConIdIndex");
            
            CreateTable(
                "dbo.ContractTradingHours",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ConId = c.Int(nullable: false),
                        ShortHistoricalDate = c.String(nullable: false),
                        LiquidHours = c.String(),
                        TradingHours = c.String(),
                        TimeZoneId = c.String(),
                        GmtOffset = c.Int(nullable: false),
                        OpeningBell = c.DateTime(nullable: false),
                        ClosingBell = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DBContractDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ConId = c.Int(nullable: false),
                        MarketName = c.String(),
                        MinTick = c.Double(nullable: false),
                        PriceMagnifier = c.Int(nullable: false),
                        OrderTypes = c.String(),
                        ValidExchanges = c.String(),
                        UnderConId = c.Int(nullable: false),
                        LongName = c.String(),
                        ContractMonth = c.String(),
                        TimeZoneId = c.String(),
                        TradingHours = c.String(),
                        LiquidHours = c.String(),
                        EvRule = c.String(),
                        EvMultiplier = c.Double(nullable: false),
                        Cusip = c.String(),
                        Ratings = c.String(),
                        DescAppend = c.String(),
                        BondType = c.String(),
                        CouponType = c.String(),
                        Callable = c.Boolean(nullable: false),
                        Putable = c.Boolean(nullable: false),
                        Coupon = c.Double(nullable: false),
                        Convertible = c.Boolean(nullable: false),
                        Maturity = c.String(),
                        IssueDate = c.String(),
                        NextOptionDate = c.String(),
                        NextOptionType = c.String(),
                        NextOptionPartial = c.Boolean(nullable: false),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DBTagValues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ConId = c.Int(nullable: false),
                        Tag = c.String(),
                        Value = c.String(),
                        DBContractDetail_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DBContractDetails", t => t.DBContractDetail_Id)
                .Index(t => t.DBContractDetail_Id);
            
            CreateTable(
                "dbo.IncomingBars",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EntityType = c.String(),
                        ReqId = c.Int(nullable: false),
                        Time = c.Long(nullable: false),
                        BarStartTime = c.DateTime(nullable: false),
                        ConId = c.Int(nullable: false),
                        Open = c.Double(nullable: false),
                        High = c.Double(nullable: false),
                        Low = c.Double(nullable: false),
                        Close = c.Double(nullable: false),
                        Volume = c.Long(nullable: false),
                        Wap = c.Double(nullable: false),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.InstantaneousTrendSerializations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EpochTime = c.Int(nullable: false),
                        BinaryStartTime = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BinaryEndTime = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        Duration = c.Time(nullable: false, precision: 7),
                        ConId = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LocalSymbol = c.String(),
                        FiveSecondIT = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FifteenSecondIT = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MinuteIT = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FiveMinuteIT = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FifteenMinuteIT = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Price_5 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Price_15 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Price_60 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Price_300 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Price_900 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Open = c.Decimal(nullable: false, precision: 18, scale: 2),
                        High = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Low = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Close = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Volume = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DBTagValues", "DBContractDetail_Id", "dbo.DBContractDetails");
            DropForeignKey("dbo.ContractQuotes", "DbContract_Id", "dbo.DBContracts");
            DropIndex("dbo.DBTagValues", new[] { "DBContractDetail_Id" });
            DropIndex("dbo.DBContracts", "IX_ContractConIdIndex");
            DropIndex("dbo.DBContracts", new[] { "ConId" });
            DropIndex("dbo.ContractQuotes", new[] { "DbContract_Id" });
            DropIndex("dbo.ContractQuotes", "IX_ContractQuoteConIdIndex");
            DropIndex("dbo.ContractQuotes", new[] { "ConId" });
            DropTable("dbo.InstantaneousTrendSerializations");
            DropTable("dbo.IncomingBars");
            DropTable("dbo.DBTagValues");
            DropTable("dbo.DBContractDetails");
            DropTable("dbo.ContractTradingHours");
            DropTable("dbo.DBContracts");
            DropTable("dbo.ContractQuotes");
        }
    }
}
