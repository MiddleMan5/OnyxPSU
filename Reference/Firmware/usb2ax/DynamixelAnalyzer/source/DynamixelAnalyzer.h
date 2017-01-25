#ifndef DYNAMIXEL_ANALYZER_H
#define DYNAMIXEL_ANALYZER_H

#include <Analyzer.h>
#include "DynamixelAnalyzerResults.h"
#include "DynamixelSimulationDataGenerator.h"

class DynamixelAnalyzerSettings;
class ANALYZER_EXPORT DynamixelAnalyzer : public Analyzer
{
public:
	DynamixelAnalyzer();
	virtual ~DynamixelAnalyzer();
	virtual void WorkerThread();

	virtual U32 GenerateSimulationData( U64 newest_sample_requested, U32 sample_rate, SimulationChannelDescriptor** simulation_channels );
	virtual U32 GetMinimumSampleRateHz();

	virtual const char* GetAnalyzerName() const;
	virtual bool NeedsRerun();

protected: //vars
	std::auto_ptr< DynamixelAnalyzerSettings > mSettings;
	std::auto_ptr< DynamixelAnalyzerResults > mResults;
	AnalyzerChannelData* mSerial;

	DynamixelSimulationDataGenerator mSimulationDataGenerator;
	bool mSimulationInitilized;

	//Serial analysis vars:
	U32 mSampleRateHz;
	U32 mStartOfStopBitOffset;
	U32 mEndOfStopBitOffset;
};

extern "C" ANALYZER_EXPORT const char* __cdecl GetAnalyzerName();
extern "C" ANALYZER_EXPORT Analyzer* __cdecl CreateAnalyzer( );
extern "C" ANALYZER_EXPORT void __cdecl DestroyAnalyzer( Analyzer* analyzer );

#endif //DYNAMIXEL_ANALYZER_H
