#include "DynamixelAnalyzerSettings.h"
#include <AnalyzerHelpers.h>


DynamixelAnalyzerSettings::DynamixelAnalyzerSettings()
:	mInputChannel( UNDEFINED_CHANNEL ),
	mBitRate( 1000000 )
{
	mInputChannelInterface.reset( new AnalyzerSettingInterfaceChannel() );
	mInputChannelInterface->SetTitleAndTooltip( "Serial", "Standard Xevel's Dynamixel" );
	mInputChannelInterface->SetChannel( mInputChannel );

	mBitRateInterface.reset( new AnalyzerSettingInterfaceInteger() );
	mBitRateInterface->SetTitleAndTooltip( "Bit Rate (Bits/S)",  "Specify the bit rate in bits per second." );
	mBitRateInterface->SetMax( 3000000 );
	mBitRateInterface->SetMin( 4800 );
	mBitRateInterface->SetInteger( mBitRate );

	AddInterface( mInputChannelInterface.get() );
	AddInterface( mBitRateInterface.get() );

	AddExportOption( 0, "Export as text/csv file" );
	AddExportExtension( 0, "text", "txt" );
	AddExportExtension( 0, "csv", "csv" );

	ClearChannels();
	AddChannel( mInputChannel, "Serial", false );
}

DynamixelAnalyzerSettings::~DynamixelAnalyzerSettings()
{
}

bool DynamixelAnalyzerSettings::SetSettingsFromInterfaces()
{
	mInputChannel = mInputChannelInterface->GetChannel();
	mBitRate = mBitRateInterface->GetInteger();

	ClearChannels();
	AddChannel( mInputChannel, "Xevel's Dynamixel", true );

	return true;
}

void DynamixelAnalyzerSettings::UpdateInterfacesFromSettings()
{
	mInputChannelInterface->SetChannel( mInputChannel );
	mBitRateInterface->SetInteger( mBitRate );
}

void DynamixelAnalyzerSettings::LoadSettings( const char* settings )
{
	SimpleArchive text_archive;
	text_archive.SetString( settings );

	text_archive >> mInputChannel;
	text_archive >> mBitRate;

	ClearChannels();
	AddChannel( mInputChannel, "Xevel's Dynamixel", true );

	UpdateInterfacesFromSettings();
}

const char* DynamixelAnalyzerSettings::SaveSettings()
{
	SimpleArchive text_archive;

	text_archive << mInputChannel;
	text_archive << mBitRate;

	return SetReturnString( text_archive.GetString() );
}
